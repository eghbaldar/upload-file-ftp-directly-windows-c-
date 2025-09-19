namespace UploadDirectly
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnUpload = new Button();
            progressBar1 = new ProgressBar();
            listViewFiles = new ListView();
            btnLoadFiles = new Button();
            listFiles = new ListBox();
            btnFindFile = new Button();
            chkGuidFilename = new CheckBox();
            label1 = new Label();
            txtFtphostname = new TextBox();
            txtUsername = new TextBox();
            label2 = new Label();
            txtpassword = new TextBox();
            label3 = new Label();
            btnConnect = new Button();
            pnlFtp = new Panel();
            label6 = new Label();
            listPinging = new ListBox();
            lblFileNumbers = new Label();
            txtFolder = new TextBox();
            label4 = new Label();
            txtvirtualPath = new TextBox();
            label5 = new Label();
            toolTip1 = new ToolTip(components);
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            configuesToolStripMenuItem = new ToolStripMenuItem();
            saveTheConfigueToolStripMenuItem = new ToolStripMenuItem();
            loadAConfigueToolStripMenuItem = new ToolStripMenuItem();
            chkHibernate = new CheckBox();
            pnlFtp.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(14, 13);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(740, 81);
            btnUpload.TabIndex = 0;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(14, 100);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(740, 29);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 1;
            // 
            // listViewFiles
            // 
            listViewFiles.GridLines = true;
            listViewFiles.Location = new Point(14, 129);
            listViewFiles.Name = "listViewFiles";
            listViewFiles.Size = new Size(740, 515);
            listViewFiles.TabIndex = 3;
            listViewFiles.UseCompatibleStateImageBehavior = false;
            listViewFiles.View = View.Details;
            // 
            // btnLoadFiles
            // 
            btnLoadFiles.Location = new Point(760, 13);
            btnLoadFiles.Name = "btnLoadFiles";
            btnLoadFiles.Size = new Size(252, 81);
            btnLoadFiles.TabIndex = 4;
            btnLoadFiles.Text = "LoadFiles";
            btnLoadFiles.UseVisualStyleBackColor = true;
            btnLoadFiles.Click += btnLoadFiles_Click;
            // 
            // listFiles
            // 
            listFiles.FormattingEnabled = true;
            listFiles.Location = new Point(760, 100);
            listFiles.Name = "listFiles";
            listFiles.Size = new Size(303, 544);
            listFiles.TabIndex = 5;
            // 
            // btnFindFile
            // 
            btnFindFile.Location = new Point(1018, 13);
            btnFindFile.Name = "btnFindFile";
            btnFindFile.Size = new Size(45, 81);
            btnFindFile.TabIndex = 6;
            btnFindFile.Text = "Find";
            btnFindFile.UseVisualStyleBackColor = true;
            btnFindFile.Click += btnFindFile_Click;
            // 
            // chkGuidFilename
            // 
            chkGuidFilename.AutoSize = true;
            chkGuidFilename.Location = new Point(26, 60);
            chkGuidFilename.Name = "chkGuidFilename";
            chkGuidFilename.Size = new Size(197, 24);
            chkGuidFilename.TabIndex = 7;
            chkGuidFilename.Text = "Using Guid as a Filename";
            chkGuidFilename.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(26, 43);
            label1.Name = "label1";
            label1.Size = new Size(163, 28);
            label1.TabIndex = 8;
            label1.Text = "FTP HOST NAME:";
            // 
            // txtFtphostname
            // 
            txtFtphostname.Font = new Font("Segoe UI", 12F);
            txtFtphostname.Location = new Point(195, 40);
            txtFtphostname.Name = "txtFtphostname";
            txtFtphostname.Size = new Size(246, 34);
            txtFtphostname.TabIndex = 9;
            txtFtphostname.Text = "ftp://";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.PeachPuff;
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(516, 40);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(318, 34);
            txtUsername.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(447, 43);
            label2.Name = "label2";
            label2.Size = new Size(63, 28);
            label2.TabIndex = 10;
            label2.Text = "USER:";
            // 
            // txtpassword
            // 
            txtpassword.BackColor = Color.Bisque;
            txtpassword.Font = new Font("Segoe UI", 12F);
            txtpassword.Location = new Point(516, 80);
            txtpassword.Name = "txtpassword";
            txtpassword.PasswordChar = '*';
            txtpassword.Size = new Size(318, 34);
            txtpassword.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(449, 83);
            label3.Name = "label3";
            label3.Size = new Size(61, 28);
            label3.TabIndex = 12;
            label3.Text = "PASS:";
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(1250, 40);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(179, 79);
            btnConnect.TabIndex = 14;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // pnlFtp
            // 
            pnlFtp.Controls.Add(label6);
            pnlFtp.Controls.Add(listPinging);
            pnlFtp.Controls.Add(lblFileNumbers);
            pnlFtp.Controls.Add(chkGuidFilename);
            pnlFtp.Controls.Add(btnUpload);
            pnlFtp.Controls.Add(progressBar1);
            pnlFtp.Controls.Add(listViewFiles);
            pnlFtp.Controls.Add(btnLoadFiles);
            pnlFtp.Controls.Add(listFiles);
            pnlFtp.Controls.Add(btnFindFile);
            pnlFtp.Enabled = false;
            pnlFtp.Location = new Point(12, 122);
            pnlFtp.Name = "pnlFtp";
            pnlFtp.Size = new Size(1420, 663);
            pnlFtp.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(1069, 14);
            label6.Name = "label6";
            label6.Size = new Size(73, 20);
            label6.TabIndex = 19;
            label6.Text = "pinging ...";
            // 
            // listPinging
            // 
            listPinging.BackColor = Color.Black;
            listPinging.ForeColor = Color.White;
            listPinging.FormattingEnabled = true;
            listPinging.Location = new Point(1069, 37);
            listPinging.Name = "listPinging";
            listPinging.Size = new Size(347, 604);
            listPinging.TabIndex = 18;
            // 
            // lblFileNumbers
            // 
            lblFileNumbers.BackColor = Color.Silver;
            lblFileNumbers.Font = new Font("Segoe UI", 12F);
            lblFileNumbers.ForeColor = Color.White;
            lblFileNumbers.Location = new Point(933, 60);
            lblFileNumbers.Name = "lblFileNumbers";
            lblFileNumbers.Size = new Size(72, 28);
            lblFileNumbers.TabIndex = 17;
            lblFileNumbers.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtFolder
            // 
            txtFolder.Font = new Font("Segoe UI", 12F);
            txtFolder.Location = new Point(925, 40);
            txtFolder.Name = "txtFolder";
            txtFolder.Size = new Size(319, 34);
            txtFolder.TabIndex = 17;
            txtFolder.TextChanged += txtFolder_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Cursor = Cursors.Help;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(847, 43);
            label4.Name = "label4";
            label4.Size = new Size(72, 28);
            label4.TabIndex = 16;
            label4.Text = "Folder:";
            toolTip1.SetToolTip(label4, "/public_html/{Folder}/");
            // 
            // txtvirtualPath
            // 
            txtvirtualPath.Font = new Font("Segoe UI", 12F);
            txtvirtualPath.Location = new Point(925, 85);
            txtvirtualPath.Name = "txtvirtualPath";
            txtvirtualPath.Size = new Size(319, 34);
            txtvirtualPath.TabIndex = 19;
            txtvirtualPath.TextChanged += txtvirtualPath_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Cursor = Cursors.Help;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(847, 88);
            label5.Name = "label5";
            label5.Size = new Size(70, 28);
            label5.TabIndex = 18;
            label5.Text = "V-Path";
            toolTip1.SetToolTip(label5, "e.g. : https://kingeto.ir");
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, configuesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1440, 28);
            menuStrip1.TabIndex = 20;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(116, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // configuesToolStripMenuItem
            // 
            configuesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveTheConfigueToolStripMenuItem, loadAConfigueToolStripMenuItem });
            configuesToolStripMenuItem.Name = "configuesToolStripMenuItem";
            configuesToolStripMenuItem.Size = new Size(87, 24);
            configuesToolStripMenuItem.Text = "configues";
            // 
            // saveTheConfigueToolStripMenuItem
            // 
            saveTheConfigueToolStripMenuItem.Name = "saveTheConfigueToolStripMenuItem";
            saveTheConfigueToolStripMenuItem.Size = new Size(212, 26);
            saveTheConfigueToolStripMenuItem.Text = "Save the Configue";
            saveTheConfigueToolStripMenuItem.Click += saveTheConfigueToolStripMenuItem_Click;
            // 
            // loadAConfigueToolStripMenuItem
            // 
            loadAConfigueToolStripMenuItem.Name = "loadAConfigueToolStripMenuItem";
            loadAConfigueToolStripMenuItem.Size = new Size(212, 26);
            loadAConfigueToolStripMenuItem.Text = "Load a Configue";
            loadAConfigueToolStripMenuItem.Click += loadAConfigueToolStripMenuItem_Click;
            // 
            // chkHibernate
            // 
            chkHibernate.AutoSize = true;
            chkHibernate.Location = new Point(26, 94);
            chkHibernate.Name = "chkHibernate";
            chkHibernate.Size = new Size(237, 24);
            chkHibernate.TabIndex = 21;
            chkHibernate.Text = "Hibernate after upload process";
            chkHibernate.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1440, 787);
            Controls.Add(chkHibernate);
            Controls.Add(txtvirtualPath);
            Controls.Add(label5);
            Controls.Add(txtFolder);
            Controls.Add(label4);
            Controls.Add(pnlFtp);
            Controls.Add(btnConnect);
            Controls.Add(txtpassword);
            Controls.Add(label3);
            Controls.Add(txtUsername);
            Controls.Add(label2);
            Controls.Add(txtFtphostname);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "UFFD - Upload File FTP Directly";
            Load += Form1_Load;
            pnlFtp.ResumeLayout(false);
            pnlFtp.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnUpload;
        private ProgressBar progressBar1;
        private ListView listViewFiles;
        private Button btnLoadFiles;
        private ListBox listFiles;
        private Button btnFindFile;
        private CheckBox chkGuidFilename;
        private Label label1;
        private TextBox txtFtphostname;
        private TextBox txtUsername;
        private Label label2;
        private TextBox txtpassword;
        private Label label3;
        private Button btnConnect;
        private Panel pnlFtp;
        private TextBox txtFolder;
        private Label label4;
        private TextBox txtvirtualPath;
        private Label label5;
        private Label lblFileNumbers;
        private Label label6;
        private ListBox listPinging;
        private ToolTip toolTip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem configuesToolStripMenuItem;
        private ToolStripMenuItem saveTheConfigueToolStripMenuItem;
        private ToolStripMenuItem loadAConfigueToolStripMenuItem;
        private CheckBox chkHibernate;
    }
}
