namespace PanelDueSplashMaker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblImageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSerialPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblImageFileSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dropDithering = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFlashMe = new System.Windows.Forms.Button();
            this.btnBrowseFinalFirmware = new System.Windows.Forms.Button();
            this.txtFinalFirmwarePath = new System.Windows.Forms.TextBox();
            this.btnSaveResultingFirmware = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDownloadLatest = new System.Windows.Forms.Button();
            this.btnBrowseFirmwareBinary = new System.Windows.Forms.Button();
            this.txtFirmwareBinaryPath = new System.Windows.Forms.TextBox();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dropWebVersion = new System.Windows.Forms.ComboBox();
            this.btnFlashMeFromWeb = new System.Windows.Forms.Button();
            this.bkgndFirmwareListReader = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // picBox
            // 
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.Location = new System.Drawing.Point(12, 12);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(800, 480);
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblImageSize,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.lblSerialPort,
            this.toolStripStatusLabel4,
            this.lblImageFileSize});
            this.statusStrip1.Location = new System.Drawing.Point(0, 739);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(825, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabel1.Text = "Image Size:";
            // 
            // lblImageSize
            // 
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(12, 17);
            this.lblImageSize.Text = "?";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel3.Text = "Serial Port:";
            // 
            // lblSerialPort
            // 
            this.lblSerialPort.Name = "lblSerialPort";
            this.lblSerialPort.Size = new System.Drawing.Size(36, 17);
            this.lblSerialPort.Text = "None";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // lblImageFileSize
            // 
            this.lblImageFileSize.Name = "lblImageFileSize";
            this.lblImageFileSize.Size = new System.Drawing.Size(51, 17);
            this.lblImageFileSize.Text = "File Size:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.btnSaveResultingFirmware);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(817, 208);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Local Files";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dropDithering
            // 
            this.dropDithering.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dropDithering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropDithering.FormattingEnabled = true;
            this.dropDithering.Items.AddRange(new object[] {
            "No dithering",
            "--[ Ordered ]--",
            "Bayer dithering (4x4)",
            "Bayer dithering (8x8)",
            "Clustered dot (4x4)",
            "Dot halftoning (8x8)",
            "--[ Error diffusion ]--",
            "Fan dithering (7x3)",
            "Shiau dithering (5x3)",
            "Sierra dithering (5x3)",
            "Stucki dithering (5x5)",
            "Burkes dithering (5x3)",
            "Atkinson dithering (5x5)",
            "Two-row Sierra dithering (5x3)",
            "Floyd–Steinberg dithering (3x3)",
            "Jarvis-Judice-Ninke dithering (5x5)"});
            this.dropDithering.Location = new System.Drawing.Point(433, 500);
            this.dropDithering.Name = "dropDithering";
            this.dropDithering.Size = new System.Drawing.Size(379, 21);
            this.dropDithering.TabIndex = 3;
            this.dropDithering.SelectedIndexChanged += new System.EventHandler(this.dropDithering_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(375, 503);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Dithering:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnFlashMe);
            this.groupBox2.Controls.Add(this.btnBrowseFinalFirmware);
            this.groupBox2.Controls.Add(this.txtFinalFirmwarePath);
            this.groupBox2.Location = new System.Drawing.Point(8, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(800, 46);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Final Firmware Binary File Path";
            // 
            // btnFlashMe
            // 
            this.btnFlashMe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlashMe.Location = new System.Drawing.Point(672, 17);
            this.btnFlashMe.Name = "btnFlashMe";
            this.btnFlashMe.Size = new System.Drawing.Size(122, 23);
            this.btnFlashMe.TabIndex = 11;
            this.btnFlashMe.Text = "FLASH ME!";
            this.btnFlashMe.UseVisualStyleBackColor = true;
            this.btnFlashMe.Click += new System.EventHandler(this.btnFlashMe_Click);
            // 
            // btnBrowseFinalFirmware
            // 
            this.btnBrowseFinalFirmware.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFinalFirmware.Location = new System.Drawing.Point(591, 17);
            this.btnBrowseFinalFirmware.Name = "btnBrowseFinalFirmware";
            this.btnBrowseFinalFirmware.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFinalFirmware.TabIndex = 10;
            this.btnBrowseFinalFirmware.Text = "Browse";
            this.btnBrowseFinalFirmware.UseVisualStyleBackColor = true;
            this.btnBrowseFinalFirmware.Click += new System.EventHandler(this.btnBrowseFinalFirmware_Click);
            // 
            // txtFinalFirmwarePath
            // 
            this.txtFinalFirmwarePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFinalFirmwarePath.Location = new System.Drawing.Point(6, 19);
            this.txtFinalFirmwarePath.Name = "txtFinalFirmwarePath";
            this.txtFinalFirmwarePath.Size = new System.Drawing.Size(579, 20);
            this.txtFinalFirmwarePath.TabIndex = 9;
            // 
            // btnSaveResultingFirmware
            // 
            this.btnSaveResultingFirmware.Location = new System.Drawing.Point(6, 58);
            this.btnSaveResultingFirmware.Name = "btnSaveResultingFirmware";
            this.btnSaveResultingFirmware.Size = new System.Drawing.Size(151, 23);
            this.btnSaveResultingFirmware.TabIndex = 7;
            this.btnSaveResultingFirmware.Text = "Save Resulting Fimware";
            this.btnSaveResultingFirmware.UseVisualStyleBackColor = true;
            this.btnSaveResultingFirmware.Click += new System.EventHandler(this.btnSaveResultingFirmware_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnDownloadLatest);
            this.groupBox1.Controls.Add(this.btnBrowseFirmwareBinary);
            this.groupBox1.Controls.Add(this.txtFirmwareBinaryPath);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 46);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Firmware Binary (nologo) File Path";
            // 
            // btnDownloadLatest
            // 
            this.btnDownloadLatest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadLatest.Location = new System.Drawing.Point(672, 17);
            this.btnDownloadLatest.Name = "btnDownloadLatest";
            this.btnDownloadLatest.Size = new System.Drawing.Size(122, 23);
            this.btnDownloadLatest.TabIndex = 6;
            this.btnDownloadLatest.Text = "Download Latest";
            this.btnDownloadLatest.UseVisualStyleBackColor = true;
            this.btnDownloadLatest.Click += new System.EventHandler(this.btnDownloadLatest_Click);
            // 
            // btnBrowseFirmwareBinary
            // 
            this.btnBrowseFirmwareBinary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFirmwareBinary.Location = new System.Drawing.Point(591, 17);
            this.btnBrowseFirmwareBinary.Name = "btnBrowseFirmwareBinary";
            this.btnBrowseFirmwareBinary.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFirmwareBinary.TabIndex = 5;
            this.btnBrowseFirmwareBinary.Text = "Browse";
            this.btnBrowseFirmwareBinary.UseVisualStyleBackColor = true;
            this.btnBrowseFirmwareBinary.Click += new System.EventHandler(this.btnBrowseFirmwareBinary_Click);
            // 
            // txtFirmwareBinaryPath
            // 
            this.txtFirmwareBinaryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirmwareBinaryPath.Location = new System.Drawing.Point(6, 19);
            this.txtFirmwareBinaryPath.Name = "txtFirmwareBinaryPath";
            this.txtFirmwareBinaryPath.Size = new System.Drawing.Size(579, 20);
            this.txtFirmwareBinaryPath.TabIndex = 4;
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(12, 498);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(75, 23);
            this.btnLoadImage.TabIndex = 2;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(0, 527);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(825, 234);
            this.tabControl1.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnFlashMeFromWeb);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(817, 208);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "From Web";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dropWebVersion);
            this.groupBox3.Location = new System.Drawing.Point(8, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(406, 44);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Version";
            // 
            // dropWebVersion
            // 
            this.dropWebVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.dropWebVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropWebVersion.FormattingEnabled = true;
            this.dropWebVersion.Location = new System.Drawing.Point(3, 16);
            this.dropWebVersion.Name = "dropWebVersion";
            this.dropWebVersion.Size = new System.Drawing.Size(400, 21);
            this.dropWebVersion.TabIndex = 0;
            // 
            // btnFlashMeFromWeb
            // 
            this.btnFlashMeFromWeb.Enabled = false;
            this.btnFlashMeFromWeb.Location = new System.Drawing.Point(420, 6);
            this.btnFlashMeFromWeb.Name = "btnFlashMeFromWeb";
            this.btnFlashMeFromWeb.Size = new System.Drawing.Size(147, 44);
            this.btnFlashMeFromWeb.TabIndex = 2;
            this.btnFlashMeFromWeb.Text = "FLASH ME!";
            this.btnFlashMeFromWeb.UseVisualStyleBackColor = true;
            this.btnFlashMeFromWeb.Click += new System.EventHandler(this.btnFlashMeFromWeb_Click);
            // 
            // bkgndFirmwareListReader
            // 
            this.bkgndFirmwareListReader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgndFirmwareListReader_DoWork);
            this.bkgndFirmwareListReader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkgndFirmwareListReader_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 761);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dropDithering);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.btnLoadImage);
            this.MinimumSize = new System.Drawing.Size(841, 800);
            this.Name = "Form1";
            this.Text = "PanelDueSplashMaker";
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblImageSize;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblSerialPort;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnFlashMe;
        private System.Windows.Forms.Button btnBrowseFinalFirmware;
        private System.Windows.Forms.TextBox txtFinalFirmwarePath;
        private System.Windows.Forms.Button btnSaveResultingFirmware;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDownloadLatest;
        private System.Windows.Forms.Button btnBrowseFirmwareBinary;
        private System.Windows.Forms.TextBox txtFirmwareBinaryPath;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ComboBox dropDithering;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel lblImageFileSize;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnFlashMeFromWeb;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox dropWebVersion;
        private System.ComponentModel.BackgroundWorker bkgndFirmwareListReader;
    }
}

