using System.Diagnostics;
using System.Net;
using System.Net.Cache;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Forms;

namespace UploadDirectly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeContextMenu();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadMultipleFilesAsync();
        }
        private static string IP;
        private static string USER;
        private static string PASS;
        private static string Folder;
        private static string FtpPath = $"ftp://{IP}/public_html/{Folder}/";
        private static string VirtualPath;

        private ContextMenuStrip contextMenu;
        public async Task UploadMultipleFilesAsync()
        {
            OpenFileDialog opf = new OpenFileDialog
            {
                Multiselect = true
            };

            if (opf.ShowDialog() == DialogResult.OK)
            {
                // List all selected files in the ListView
                listViewFiles.Invoke(new Action(() =>
                {
                    listViewFiles.Items.Clear();
                    foreach (string file in opf.FileNames)
                    {
                        ListViewItem item = new ListViewItem(Path.GetFileName(file));
                        item.SubItems.Add("Pending...");
                        item.SubItems.Add("0%"); // Progress column
                        listViewFiles.Items.Add(item);
                    }
                }));

                // Process each file sequentially
                foreach (ListViewItem item in listViewFiles.Items)
                {
                    string filePath = opf.FileNames[item.Index];
                    await UploadFileAsync(filePath, item);
                }

                MessageBox.Show("Upload process completed!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (chkHibernate.Checked)
                {
                    Application.SetSuspendState(PowerState.Hibernate, true, true);
                }
            }
        }

        private async Task UploadFileAsync(string filePath, ListViewItem item)
        {
            FileInfo f = new FileInfo(filePath);
            string remoteFilePath = "";
            if (chkGuidFilename.Checked) remoteFilePath = $"{FtpPath}{Guid.NewGuid()}{f.Extension}";
            else remoteFilePath = $"{FtpPath}{Path.GetFileNameWithoutExtension(f.Name)}{f.Extension}";

            long totalBytes = f.Length;
            bool uploadSuccess = false;

            while (!uploadSuccess)
            {
                try
                {
                    // Check if the file already exists
                    if (await FileExistsOnFtp(remoteFilePath))
                    {
                        UpdateListView(item, "Duplicated", "Duplicated");
                        break;
                        //continue; // Skip this file and continue to the next one
                    }
                    UpdateListView(item, "Uploading...", "0%");

                    await Task.Run(async () =>
                    {
                        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteFilePath);
                        request.Credentials = new NetworkCredential(USER, PASS);
                        request.Method = WebRequestMethods.Ftp.UploadFile;
                        request.UsePassive = true;
                        request.Timeout = 60000;
                        request.ReadWriteTimeout = 60000;
                        request.KeepAlive = false;

                        using (Stream fileStream = File.OpenRead(filePath))
                        using (Stream ftpStream = request.GetRequestStream())
                        {
                            byte[] buffer = new byte[10240];
                            int read;
                            long uploadedBytes = 0;

                            do
                            {
                                while (!CheckNetworkConnection())
                                {
                                    UpdateListView(item, "Waiting for connection...", null);
                                    Task.Delay(2000).Wait();
                                }

                                read = fileStream.Read(buffer, 0, buffer.Length);
                                if (read > 0)
                                {
                                    try
                                    {
                                        ftpStream.Write(buffer, 0, read);
                                    }
                                    catch (Exception ex)
                                    {
                                        UpdateListView(item, "Error: " + ex.Message, null);
                                        await WaitForNetworkRecovery(item);
                                    }
                                    uploadedBytes += read;

                                    double progressPercentage = ((double)uploadedBytes / totalBytes) * 100;
                                    UpdateListView(item, "Uploading...", $"{progressPercentage:0.00}%");
                                }
                            } while (read > 0);
                        }
                    });

                    uploadSuccess = true;
                    UpdateListView(item, "Completed", "100%");
                }
                catch (IOException ioEx)
                {
                    UpdateListView(item, "Error: " + ioEx.Message, null);
                    await WaitForNetworkRecovery(item);
                }
                catch (WebException webEx)
                {
                    UpdateListView(item, "Failed: " + webEx.Message, null);
                    break;
                }
            }
        }
        private async Task<bool> FileExistsOnFtp(string remoteFilePath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(remoteFilePath);
                request.Credentials = new NetworkCredential(USER, PASS);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.UsePassive = true;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return true; // File exists
                }
            }
            catch (WebException ex)
            {
                if (((FtpWebResponse)ex.Response).StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false; // File does not exist
                }
                throw;
            }
        }

        private bool CheckNetworkConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task WaitForNetworkRecovery(ListViewItem item)
        {
            while (!CheckNetworkConnection())
            {
                UpdateListView(item, "Waiting for reconnection...", null);
                await Task.Delay(2000);
            }
            UpdateListView(item, "Reconnected. Retrying...", null);
        }

        private void UpdateListView(ListViewItem item, string status, string progress)
        {
            if (listViewFiles.InvokeRequired)
            {
                listViewFiles.Invoke(new Action(() =>
                {
                    if (status != null) item.SubItems[1].Text = status;
                    switch (progress)
                    {
                        case null:
                            item.SubItems[2].Text = "failed!";
                            break;
                        case "Duplicated":
                            item.SubItems[2].Text = "Duplicated!";
                            break;
                        default:
                            item.SubItems[2].Text = progress;

                            int newValue = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(progress.Replace("%", ""))));
                            if (newValue < 100)
                                progressBar1.Value = newValue;
                            else progressBar1.Value = 0;
                            break;
                    }
                }));
            }
            else
            {
                if (status != null) item.SubItems[1].Text = status;
                switch (progress)
                {
                    case null:
                        item.SubItems[2].Text = "failed!";
                        break;
                    case "Duplicated":
                        item.SubItems[2].Text = "Duplicated";
                        break;
                    default:
                        item.SubItems[2].Text = progress;

                        int newValue = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(progress.Replace("%", ""))));
                        if (newValue < 100)
                            progressBar1.Value = newValue;
                        else progressBar1.Value = 0;
                        break;
                }
            }
        }
        private void ListViewFiles_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            // If it's the progress column (column 2) and the status is "Failed!"
            if (e.ColumnIndex == 2 && e.SubItem.Text == "failed!")
            {
                // Set background color to red for the "Failed!" cell
                e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
                // Set the text color to white to ensure it's readable
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, listViewFiles.Font, e.Bounds, Color.White);
            }
            else if (e.ColumnIndex == 2 && e.SubItem.Text == "Duplicated")
            {
                // Set background color to red for the "Failed!" cell
                e.Graphics.FillRectangle(Brushes.Yellow, e.Bounds);
                // Set the text color to white to ensure it's readable
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, listViewFiles.Font, e.Bounds, Color.Black);
            }
            else if (e.ColumnIndex == 2 && e.SubItem.Text == "100%")
            {
                // Set background color to red for the "Failed!" cell
                e.Graphics.FillRectangle(Brushes.Green, e.Bounds);
                // Set the text color to white to ensure it's readable
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, listViewFiles.Font, e.Bounds, Color.White);
            }
            else
            {
                // Default drawing for other cells
                e.DrawDefault = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listViewFiles.View = View.Details;
            listViewFiles.FullRowSelect = true;
            listViewFiles.GridLines = true;

            // Ensure columns are set
            listViewFiles.Columns.Clear();
            listViewFiles.Columns.Add("File Name", 200);
            listViewFiles.Columns.Add("Status", 150);
            listViewFiles.Columns.Add("Progress", 100);

            listViewFiles.OwnerDraw = true;
            listViewFiles.DrawSubItem += ListViewFiles_DrawSubItem;

        }

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            if (txtvirtualPath.Text.Trim().Length == 0 || txtFolder.Text.Trim().Length == 0)
            {
                MessageBox.Show("fill the folder and virtual path (v-path) fields up at first!");
                return;
            }
            listFiles.Items.Clear();
            ListFiles();
            lblFileNumbers.Text = listFiles.Items.Count.ToString();
        }
        public void ListFiles()
        {
            //MessageBox.Show(IP);
            //MessageBox.Show(USER);
            //MessageBox.Show(PASS);
            //MessageBox.Show(Folder);
            //MessageBox.Show(FtpPath);
            //MessageBox.Show(VirtualPath);
            try
            {
                string ftpUrl = FtpPath;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(USER, PASS);
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrEmpty(line) && line != "." && line != "..") listFiles.Items.Add(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        public void DeleteFile(string fileName)
        {
            try
            {
                string ftpUrl = $"{FtpPath}{fileName}";
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(USER, PASS);

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    MessageBox.Show("File deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void OpenFileInBrowser(string fileName)
        {
            try
            {
                string url = $"{VirtualPath}{fileName}"; // Replace with your file's public URL
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Delete File");
            ToolStripMenuItem openMenuItem = new ToolStripMenuItem("Open in Browser");

            deleteMenuItem.Click += (sender, e) =>
            {
                if (listFiles.SelectedItem != null)
                {
                    string fileName = listFiles.SelectedItem.ToString();
                    DialogResult result = MessageBox.Show($"Are you sure you want to delete '{fileName}'?", "Confirm Delete", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        DeleteFile(fileName);
                        listFiles.Items.Remove(fileName);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a file first.");
                }
            };

            openMenuItem.Click += (sender, e) =>
            {
                if (listFiles.SelectedItem != null)
                {
                    string fileName = listFiles.SelectedItem.ToString();
                    OpenFileInBrowser(fileName);
                }
                else
                {
                    MessageBox.Show("Please select a file first.");
                }
            };

            contextMenu.Items.Add(deleteMenuItem);
            contextMenu.Items.Add(openMenuItem);

            listFiles.ContextMenuStrip = contextMenu;
        }
        public void FilterItems()
        {
            if (listFiles.Items.Count == 0)
            {
                MessageBox.Show("The list is empty. Nothing to search!");
                return;
            }

            // Prompt user for the search term
            string searchTerm = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the search term:",
                "Search Items",
                ""
            );

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Search term cannot be empty!");
                return;
            }

            // Use LINQ to filter items that contain the search term (case-insensitive)
            var filteredItems = listFiles.Items.Cast<string>()
                                   .Where(item => item.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                                   .ToList();

            if (filteredItems.Count == 0)
            {
                MessageBox.Show("No matching items found!");
            }
            else
            {
                // Repopulate ListBox with filtered items
                listFiles.Items.Clear();
                foreach (var item in filteredItems)
                {
                    listFiles.Items.Add(item);
                }
            }
        }

        private void btnFindFile_Click(object sender, EventArgs e)
        {
            FilterItems();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string ftpHost = txtFtphostname.Text;
            string username = txtUsername.Text;
            string password = txtpassword.Text;

            var check = CheckFtpCredentials(ftpHost, username, password);
            if (check.IsSuccess)
            {
                IP = txtFtphostname.Text;
                USER = txtUsername.Text;
                PASS = txtpassword.Text;
                Folder = txtFolder.Text;
                FtpPath = $"ftp://{IP.Replace("ftp://", "")}/public_html/{Folder}/";
                VirtualPath = $"{txtvirtualPath.Text}/{Folder}/";

                pnlFtp.Enabled = true;

                txtFtphostname.Enabled = false;
                txtUsername.Enabled = false;
                txtpassword.Enabled = false;
                txtpassword.Enabled = false;

                StartPing();
            }
            else
            {
                MessageBox.Show(check.Message);
                pnlFtp.Enabled = false;
            }
        }
        static RersultDto CheckFtpCredentials(string ftpHost, string username, string password)
        {
            try
            {
                // Create FTP request
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpHost);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                // Provide credentials
                request.Credentials = new NetworkCredential(username, password);

                // Get FTP response
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return new RersultDto { IsSuccess = response.StatusCode == FtpStatusCode.OpeningData, Message = "" };
                }
            }
            catch (Exception ex)
            {
                return new RersultDto { IsSuccess = false, Message = ex.Message };
            }
        }
        protected class RersultDto
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            if (pnlFtp.Enabled)
            {
                Folder = txtFolder.Text;
                FtpPath = $"ftp://{IP.Replace("ftp://", "")}/public_html/{Folder}/";
            }
        }

        private void txtvirtualPath_TextChanged(object sender, EventArgs e)
        {
            if (pnlFtp.Enabled)
            {
                VirtualPath = txtvirtualPath.Text;
                VirtualPath = $"{txtvirtualPath.Text}/{Folder}/";
            }
        }
        private void StartPing()
        {
            // Configure the process to execute the ping command
            Process pingProcess = new Process();
            pingProcess.StartInfo.FileName = "ping";
            pingProcess.StartInfo.Arguments = "8.8.8.8 -t";
            pingProcess.StartInfo.RedirectStandardOutput = true;
            pingProcess.StartInfo.UseShellExecute = false;
            pingProcess.StartInfo.CreateNoWindow = true;

            // Handle the output asynchronously
            pingProcess.OutputDataReceived += (s, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    AddOutputToListBox(e.Data);
                }
            };

            // Start the process
            pingProcess.Start();
            pingProcess.BeginOutputReadLine();
        }

        private void AddOutputToListBox(string output)
        {
            if (listPinging.InvokeRequired)
            {
                listPinging.Invoke(new Action(() => listPinging.Items.Add(output)));
                listPinging.Invoke(new Action(() => listPinging.TopIndex = listPinging.Items.Count - 1));
            }
            else
            {
                listPinging.Items.Add(output);
                listPinging.TopIndex = listPinging.Items.Count - 1;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        // configuration section
        public class AppConfig
        {
            public string IP { get; set; }
            public string USER { get; set; }
            public string PASS { get; set; }
            public string Folder { get; set; }
            public string VirtualPath { get; set; }
        }

        private void saveTheConfigueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppConfig config = new()
            {
                IP = txtFtphostname.Text.Trim(),
                USER = txtUsername.Text.Trim(),
                PASS = txtpassword.Text.Trim(),
                Folder = txtFolder.Text.Trim(),
                VirtualPath = txtvirtualPath.Text.Trim(),
            };
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save Configuration";
            saveFileDialog.Filter = "JSON files (*.json)|*.json";
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            SaveConfig(config, saveFileDialog.FileName);
        }
        public static void SaveConfig(AppConfig config, string path)
        {
            // Serialize config first
            string json = JsonSerializer.Serialize(config);

            // Encrypt the JSON string
            string encryptedJson = EncryptionHelper.Encrypt(json);

            // Save encrypted content
            File.WriteAllText(path, encryptedJson);
        }
        public static AppConfig LoadConfig(string path)
        {
            if (!File.Exists(path)) return null;

            string encryptedJson = File.ReadAllText(path);
            string decryptedJson = EncryptionHelper.Decrypt(encryptedJson);

            return JsonSerializer.Deserialize<AppConfig>(decryptedJson);
        }

        private void loadAConfigueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open Configuration";
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.DefaultExt = "json";
            openFileDialog.ShowDialog();
            if (string.IsNullOrEmpty(openFileDialog.FileName)) return;

            AppConfig config = LoadConfig(openFileDialog.FileName);

            if (config != null)
            {
                txtFtphostname.Text = config.IP;
                txtUsername.Text = config.USER;
                txtpassword.Text = config.PASS;
                txtFolder.Text = config.Folder;
                txtvirtualPath.Text = config.VirtualPath;
            }
        }
    }
}
