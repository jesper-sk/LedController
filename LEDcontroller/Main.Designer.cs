namespace LedController
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            "",
            "",
            ""}, -1);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MainNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ActiveProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateLogTimer = new System.Windows.Forms.Timer(this.components);
            this.ConfigTabPage = new System.Windows.Forms.TabPage();
            this.OpenOnStartupCheckBox = new System.Windows.Forms.CheckBox();
            this.StartMinimizedCheckBox = new System.Windows.Forms.CheckBox();
            this.ConnectOnOpenCheckBox = new System.Windows.Forms.CheckBox();
            this.LogTabPage = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.LogUpdateIntervalComboBox = new System.Windows.Forms.ComboBox();
            this.CopyLogToClipboardButton = new System.Windows.Forms.Button();
            this.ArchiveLogButton = new System.Windows.Forms.Button();
            this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
            this.VisualizerTabPage = new System.Windows.Forms.TabPage();
            this.OpenVisualizerFormButton = new System.Windows.Forms.Button();
            this.VisualizerPanel = new System.Windows.Forms.Panel();
            this.StripInfoLabel = new System.Windows.Forms.Label();
            this.ProfilesTabPage = new System.Windows.Forms.TabPage();
            this.MusicGroupBox = new System.Windows.Forms.GroupBox();
            this.MusicBrightnessNud = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.MusicBrightnessTrackbar = new System.Windows.Forms.TrackBar();
            this.label15 = new System.Windows.Forms.Label();
            this.MusicEffectComboBox = new System.Windows.Forms.ComboBox();
            this.MusicHotkeyLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.MusicDeviceLabel = new System.Windows.Forms.Label();
            this.MusicSelectDeviceButton = new System.Windows.Forms.Button();
            this.ConnectionGB = new System.Windows.Forms.GroupBox();
            this.ComPortComboBox = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.connectComButton = new System.Windows.Forms.Button();
            this.refreshComButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.profileSetCreateButton = new System.Windows.Forms.Button();
            this.profileSetComboBox = new System.Windows.Forms.ComboBox();
            this.profileListView = new System.Windows.Forms.ListView();
            this.profileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.shorcutName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StaticGroupBox = new System.Windows.Forms.GroupBox();
            this.staticColortHotkeyLabel = new System.Windows.Forms.Label();
            this.StaticBrightnessNUD = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.StaticBrightnessTrackbar = new System.Windows.Forms.TrackBar();
            this.staticProfileColorButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.AmbilightGroupBox = new System.Windows.Forms.GroupBox();
            this.AmbilightPrecisionTrackBar = new System.Windows.Forms.TrackBar();
            this.label9 = new System.Windows.Forms.Label();
            this.AmbilightBrightnessNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AmbilightBrightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.AmbilightRecordStatusLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.AmbilightCaptureProfileComboBox = new System.Windows.Forms.ComboBox();
            this.AmbilightCreateCaptureProfileButton = new System.Windows.Forms.Button();
            this.AmbilightCaptureScreenComboBox = new System.Windows.Forms.ComboBox();
            this.AmbilightShowCaptureAreasButton = new System.Windows.Forms.Button();
            this.AmbilightHotkeyLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.RainbowGroupBox = new System.Windows.Forms.GroupBox();
            this.RainbowSpeedNud = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.RainbowSpeedTrackBar = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.RainbowBrightnessNud = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.RainbowBrightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.NullGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.newProfileButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.saveToXmlButton = new System.Windows.Forms.Button();
            this.deactivateLEDsButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SelectActiveProfileButton = new System.Windows.Forms.Button();
            this.activeProfileLabel = new System.Windows.Forms.Label();
            this.FpsLabel = new System.Windows.Forms.Label();
            this.mainFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TabControl = new System.Windows.Forms.TabControl();
            this.BassTabPage = new System.Windows.Forms.TabPage();
            this.AudioLevelPanel = new System.Windows.Forms.Panel();
            this.AudioNumBandsLabel = new System.Windows.Forms.Label();
            this.numBandsTrackBar = new System.Windows.Forms.TrackBar();
            this.StartAudioVisButton = new System.Windows.Forms.Button();
            this.AudioVisualizerPanel = new System.Windows.Forms.Panel();
            this.audioUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.TrayContextMenu.SuspendLayout();
            this.ConfigTabPage.SuspendLayout();
            this.LogTabPage.SuspendLayout();
            this.VisualizerTabPage.SuspendLayout();
            this.ProfilesTabPage.SuspendLayout();
            this.MusicGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MusicBrightnessNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MusicBrightnessTrackbar)).BeginInit();
            this.ConnectionGB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.StaticGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StaticBrightnessNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StaticBrightnessTrackbar)).BeginInit();
            this.AmbilightGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightPrecisionTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightBrightnessNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightBrightnessTrackBar)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.RainbowGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowSpeedNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowSpeedTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowBrightnessNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowBrightnessTrackBar)).BeginInit();
            this.NullGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).BeginInit();
            this.TabControl.SuspendLayout();
            this.BassTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBandsTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 849);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1877, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLabel1.Text = "Disconnected";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MainNotifyIcon
            // 
            this.MainNotifyIcon.ContextMenuStrip = this.TrayContextMenu;
            this.MainNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("MainNotifyIcon.Icon")));
            this.MainNotifyIcon.Text = "Led Controller - Click to open";
            this.MainNotifyIcon.Visible = true;
            this.MainNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MainNotifyIcon_MouseDoubleClick);
            this.MainNotifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainNotifyIcon_MouseMove);
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowHideToolStripMenuItem,
            this.ActiveProfileToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.TrayContextMenu.Name = "contextMenuStrip2";
            this.TrayContextMenu.Size = new System.Drawing.Size(177, 76);
            // 
            // ShowHideToolStripMenuItem
            // 
            this.ShowHideToolStripMenuItem.Name = "ShowHideToolStripMenuItem";
            this.ShowHideToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.ShowHideToolStripMenuItem.Text = "Show";
            this.ShowHideToolStripMenuItem.Click += new System.EventHandler(this.ShowHideToolStripMenuItem_Click);
            // 
            // ActiveProfileToolStripMenuItem
            // 
            this.ActiveProfileToolStripMenuItem.Name = "ActiveProfileToolStripMenuItem";
            this.ActiveProfileToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.ActiveProfileToolStripMenuItem.Text = "Select active profile";
            this.ActiveProfileToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ActiveProfileToolStripMenuItem_DropDownItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // UpdateLogTimer
            // 
            this.UpdateLogTimer.Tick += new System.EventHandler(this.UpdateLogTimer_Tick);
            // 
            // ConfigTabPage
            // 
            this.ConfigTabPage.Controls.Add(this.OpenOnStartupCheckBox);
            this.ConfigTabPage.Controls.Add(this.StartMinimizedCheckBox);
            this.ConfigTabPage.Controls.Add(this.ConnectOnOpenCheckBox);
            this.ConfigTabPage.Location = new System.Drawing.Point(4, 22);
            this.ConfigTabPage.Name = "ConfigTabPage";
            this.ConfigTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ConfigTabPage.Size = new System.Drawing.Size(899, 770);
            this.ConfigTabPage.TabIndex = 3;
            this.ConfigTabPage.Text = "Configuration";
            this.ConfigTabPage.UseVisualStyleBackColor = true;
            // 
            // OpenOnStartupCheckBox
            // 
            this.OpenOnStartupCheckBox.AutoSize = true;
            this.OpenOnStartupCheckBox.Location = new System.Drawing.Point(6, 29);
            this.OpenOnStartupCheckBox.Name = "OpenOnStartupCheckBox";
            this.OpenOnStartupCheckBox.Size = new System.Drawing.Size(100, 17);
            this.OpenOnStartupCheckBox.TabIndex = 2;
            this.OpenOnStartupCheckBox.Text = "Startup on login";
            this.OpenOnStartupCheckBox.UseVisualStyleBackColor = true;
            // 
            // StartMinimizedCheckBox
            // 
            this.StartMinimizedCheckBox.AutoSize = true;
            this.StartMinimizedCheckBox.Location = new System.Drawing.Point(6, 52);
            this.StartMinimizedCheckBox.Name = "StartMinimizedCheckBox";
            this.StartMinimizedCheckBox.Size = new System.Drawing.Size(96, 17);
            this.StartMinimizedCheckBox.TabIndex = 1;
            this.StartMinimizedCheckBox.Text = "Start minimized";
            this.StartMinimizedCheckBox.UseVisualStyleBackColor = true;
            // 
            // ConnectOnOpenCheckBox
            // 
            this.ConnectOnOpenCheckBox.AutoSize = true;
            this.ConnectOnOpenCheckBox.Location = new System.Drawing.Point(6, 6);
            this.ConnectOnOpenCheckBox.Name = "ConnectOnOpenCheckBox";
            this.ConnectOnOpenCheckBox.Size = new System.Drawing.Size(145, 17);
            this.ConnectOnOpenCheckBox.TabIndex = 0;
            this.ConnectOnOpenCheckBox.Text = "Try to connect on startup";
            this.ConnectOnOpenCheckBox.UseVisualStyleBackColor = true;
            // 
            // LogTabPage
            // 
            this.LogTabPage.Controls.Add(this.label10);
            this.LogTabPage.Controls.Add(this.LogUpdateIntervalComboBox);
            this.LogTabPage.Controls.Add(this.CopyLogToClipboardButton);
            this.LogTabPage.Controls.Add(this.ArchiveLogButton);
            this.LogTabPage.Controls.Add(this.LogRichTextBox);
            this.LogTabPage.Location = new System.Drawing.Point(4, 22);
            this.LogTabPage.Name = "LogTabPage";
            this.LogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LogTabPage.Size = new System.Drawing.Size(899, 770);
            this.LogTabPage.TabIndex = 2;
            this.LogTabPage.Text = "Logger";
            this.LogTabPage.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(735, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Update interval: (ms)";
            // 
            // LogUpdateIntervalComboBox
            // 
            this.LogUpdateIntervalComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LogUpdateIntervalComboBox.FormattingEnabled = true;
            this.LogUpdateIntervalComboBox.Items.AddRange(new object[] {
            "1 ms",
            "10 ms",
            "100 ms",
            "1000 ms"});
            this.LogUpdateIntervalComboBox.Location = new System.Drawing.Point(735, 25);
            this.LogUpdateIntervalComboBox.Name = "LogUpdateIntervalComboBox";
            this.LogUpdateIntervalComboBox.Size = new System.Drawing.Size(158, 21);
            this.LogUpdateIntervalComboBox.TabIndex = 3;
            this.LogUpdateIntervalComboBox.SelectedIndexChanged += new System.EventHandler(this.LogUpdateIntervalComboBox_SelectedIndexChanged);
            // 
            // CopyLogToClipboardButton
            // 
            this.CopyLogToClipboardButton.Location = new System.Drawing.Point(735, 335);
            this.CopyLogToClipboardButton.Name = "CopyLogToClipboardButton";
            this.CopyLogToClipboardButton.Size = new System.Drawing.Size(158, 23);
            this.CopyLogToClipboardButton.TabIndex = 2;
            this.CopyLogToClipboardButton.Text = "Copy to clipboard";
            this.CopyLogToClipboardButton.UseVisualStyleBackColor = true;
            this.CopyLogToClipboardButton.Click += new System.EventHandler(this.CopyLogToClipboardButton_Click);
            // 
            // ArchiveLogButton
            // 
            this.ArchiveLogButton.Location = new System.Drawing.Point(735, 364);
            this.ArchiveLogButton.Name = "ArchiveLogButton";
            this.ArchiveLogButton.Size = new System.Drawing.Size(158, 23);
            this.ArchiveLogButton.TabIndex = 1;
            this.ArchiveLogButton.Text = "Archive";
            this.ArchiveLogButton.UseVisualStyleBackColor = true;
            this.ArchiveLogButton.Click += new System.EventHandler(this.ArchiveLogButton_Click);
            // 
            // LogRichTextBox
            // 
            this.LogRichTextBox.Location = new System.Drawing.Point(6, 6);
            this.LogRichTextBox.Name = "LogRichTextBox";
            this.LogRichTextBox.ReadOnly = true;
            this.LogRichTextBox.ShortcutsEnabled = false;
            this.LogRichTextBox.Size = new System.Drawing.Size(723, 381);
            this.LogRichTextBox.TabIndex = 0;
            this.LogRichTextBox.Text = "";
            // 
            // VisualizerTabPage
            // 
            this.VisualizerTabPage.Controls.Add(this.OpenVisualizerFormButton);
            this.VisualizerTabPage.Controls.Add(this.VisualizerPanel);
            this.VisualizerTabPage.Controls.Add(this.StripInfoLabel);
            this.VisualizerTabPage.Location = new System.Drawing.Point(4, 22);
            this.VisualizerTabPage.Name = "VisualizerTabPage";
            this.VisualizerTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.VisualizerTabPage.Size = new System.Drawing.Size(899, 770);
            this.VisualizerTabPage.TabIndex = 1;
            this.VisualizerTabPage.Text = "Visualizer";
            this.VisualizerTabPage.UseVisualStyleBackColor = true;
            // 
            // OpenVisualizerFormButton
            // 
            this.OpenVisualizerFormButton.Location = new System.Drawing.Point(9, 364);
            this.OpenVisualizerFormButton.Name = "OpenVisualizerFormButton";
            this.OpenVisualizerFormButton.Size = new System.Drawing.Size(128, 23);
            this.OpenVisualizerFormButton.TabIndex = 3;
            this.OpenVisualizerFormButton.Text = "Open in new window";
            this.OpenVisualizerFormButton.UseVisualStyleBackColor = true;
            this.OpenVisualizerFormButton.Click += new System.EventHandler(this.OpenVisualizerFormButton_Click);
            // 
            // VisualizerPanel
            // 
            this.VisualizerPanel.Location = new System.Drawing.Point(143, 6);
            this.VisualizerPanel.Name = "VisualizerPanel";
            this.VisualizerPanel.Size = new System.Drawing.Size(750, 381);
            this.VisualizerPanel.TabIndex = 2;
            // 
            // StripInfoLabel
            // 
            this.StripInfoLabel.AutoSize = true;
            this.StripInfoLabel.Location = new System.Drawing.Point(6, 6);
            this.StripInfoLabel.Name = "StripInfoLabel";
            this.StripInfoLabel.Size = new System.Drawing.Size(48, 13);
            this.StripInfoLabel.TabIndex = 1;
            this.StripInfoLabel.Text = "Strip info";
            // 
            // ProfilesTabPage
            // 
            this.ProfilesTabPage.BackColor = System.Drawing.Color.White;
            this.ProfilesTabPage.Controls.Add(this.MusicGroupBox);
            this.ProfilesTabPage.Controls.Add(this.ConnectionGB);
            this.ProfilesTabPage.Controls.Add(this.groupBox1);
            this.ProfilesTabPage.Controls.Add(this.profileSetComboBox);
            this.ProfilesTabPage.Controls.Add(this.profileListView);
            this.ProfilesTabPage.Controls.Add(this.StaticGroupBox);
            this.ProfilesTabPage.Controls.Add(this.AmbilightGroupBox);
            this.ProfilesTabPage.Controls.Add(this.RainbowGroupBox);
            this.ProfilesTabPage.Controls.Add(this.NullGroupBox);
            this.ProfilesTabPage.Controls.Add(this.groupBox2);
            this.ProfilesTabPage.Controls.Add(this.groupBox4);
            this.ProfilesTabPage.Controls.Add(this.groupBox5);
            this.ProfilesTabPage.Location = new System.Drawing.Point(4, 22);
            this.ProfilesTabPage.Name = "ProfilesTabPage";
            this.ProfilesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ProfilesTabPage.Size = new System.Drawing.Size(899, 770);
            this.ProfilesTabPage.TabIndex = 0;
            this.ProfilesTabPage.Text = "Profile Selection";
            // 
            // MusicGroupBox
            // 
            this.MusicGroupBox.Controls.Add(this.MusicBrightnessNud);
            this.MusicGroupBox.Controls.Add(this.label16);
            this.MusicGroupBox.Controls.Add(this.MusicBrightnessTrackbar);
            this.MusicGroupBox.Controls.Add(this.label15);
            this.MusicGroupBox.Controls.Add(this.MusicEffectComboBox);
            this.MusicGroupBox.Controls.Add(this.MusicHotkeyLabel);
            this.MusicGroupBox.Controls.Add(this.label14);
            this.MusicGroupBox.Controls.Add(this.label13);
            this.MusicGroupBox.Controls.Add(this.MusicDeviceLabel);
            this.MusicGroupBox.Controls.Add(this.MusicSelectDeviceButton);
            this.MusicGroupBox.Location = new System.Drawing.Point(1266, 394);
            this.MusicGroupBox.Name = "MusicGroupBox";
            this.MusicGroupBox.Size = new System.Drawing.Size(414, 380);
            this.MusicGroupBox.TabIndex = 42;
            this.MusicGroupBox.TabStop = false;
            this.MusicGroupBox.Text = "Selected LED Profile";
            // 
            // MusicBrightnessNud
            // 
            this.MusicBrightnessNud.Location = new System.Drawing.Point(333, 136);
            this.MusicBrightnessNud.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.MusicBrightnessNud.Name = "MusicBrightnessNud";
            this.MusicBrightnessNud.Size = new System.Drawing.Size(75, 20);
            this.MusicBrightnessNud.TabIndex = 47;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 138);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 46;
            this.label16.Text = "Brightness:";
            // 
            // MusicBrightnessTrackbar
            // 
            this.MusicBrightnessTrackbar.Location = new System.Drawing.Point(9, 162);
            this.MusicBrightnessTrackbar.Maximum = 255;
            this.MusicBrightnessTrackbar.Name = "MusicBrightnessTrackbar";
            this.MusicBrightnessTrackbar.Size = new System.Drawing.Size(399, 45);
            this.MusicBrightnessTrackbar.TabIndex = 45;
            this.MusicBrightnessTrackbar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 44;
            this.label15.Text = "Select Effect:";
            // 
            // MusicEffectComboBox
            // 
            this.MusicEffectComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MusicEffectComboBox.FormattingEnabled = true;
            this.MusicEffectComboBox.Location = new System.Drawing.Point(9, 52);
            this.MusicEffectComboBox.Name = "MusicEffectComboBox";
            this.MusicEffectComboBox.Size = new System.Drawing.Size(399, 21);
            this.MusicEffectComboBox.TabIndex = 43;
            // 
            // MusicHotkeyLabel
            // 
            this.MusicHotkeyLabel.AutoSize = true;
            this.MusicHotkeyLabel.Location = new System.Drawing.Point(56, 350);
            this.MusicHotkeyLabel.Name = "MusicHotkeyLabel";
            this.MusicHotkeyLabel.Size = new System.Drawing.Size(69, 13);
            this.MusicHotkeyLabel.TabIndex = 30;
            this.MusicHotkeyLabel.Text = "Not assigned";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 350);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Hotkey:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 329);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Device:";
            // 
            // MusicDeviceLabel
            // 
            this.MusicDeviceLabel.AutoSize = true;
            this.MusicDeviceLabel.Location = new System.Drawing.Point(56, 329);
            this.MusicDeviceLabel.Name = "MusicDeviceLabel";
            this.MusicDeviceLabel.Size = new System.Drawing.Size(41, 13);
            this.MusicDeviceLabel.TabIndex = 1;
            this.MusicDeviceLabel.Text = "label13";
            // 
            // MusicSelectDeviceButton
            // 
            this.MusicSelectDeviceButton.Location = new System.Drawing.Point(328, 324);
            this.MusicSelectDeviceButton.Name = "MusicSelectDeviceButton";
            this.MusicSelectDeviceButton.Size = new System.Drawing.Size(80, 23);
            this.MusicSelectDeviceButton.TabIndex = 0;
            this.MusicSelectDeviceButton.Text = "Select device";
            this.MusicSelectDeviceButton.UseVisualStyleBackColor = true;
            this.MusicSelectDeviceButton.Click += new System.EventHandler(this.MusicSelectDeviceButton_Click);
            // 
            // ConnectionGB
            // 
            this.ConnectionGB.Controls.Add(this.ComPortComboBox);
            this.ConnectionGB.Controls.Add(this.checkBox1);
            this.ConnectionGB.Controls.Add(this.connectComButton);
            this.ConnectionGB.Controls.Add(this.refreshComButton);
            this.ConnectionGB.Location = new System.Drawing.Point(6, 6);
            this.ConnectionGB.Name = "ConnectionGB";
            this.ConnectionGB.Size = new System.Drawing.Size(93, 130);
            this.ConnectionGB.TabIndex = 13;
            this.ConnectionGB.TabStop = false;
            this.ConnectionGB.Text = "Connection";
            // 
            // ComPortComboBox
            // 
            this.ComPortComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComPortComboBox.FormattingEnabled = true;
            this.ComPortComboBox.Location = new System.Drawing.Point(6, 19);
            this.ComPortComboBox.Name = "ComPortComboBox";
            this.ComPortComboBox.Size = new System.Drawing.Size(75, 21);
            this.ComPortComboBox.TabIndex = 0;
            this.ComPortComboBox.SelectedIndexChanged += new System.EventHandler(this.ComPortComboBox_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 104);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "Override";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // connectComButton
            // 
            this.connectComButton.Location = new System.Drawing.Point(6, 75);
            this.connectComButton.Name = "connectComButton";
            this.connectComButton.Size = new System.Drawing.Size(75, 23);
            this.connectComButton.TabIndex = 1;
            this.connectComButton.Text = "Connect";
            this.connectComButton.UseVisualStyleBackColor = true;
            this.connectComButton.Click += new System.EventHandler(this.ConnectComButton_Click);
            // 
            // refreshComButton
            // 
            this.refreshComButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.refreshComButton.Location = new System.Drawing.Point(6, 46);
            this.refreshComButton.Name = "refreshComButton";
            this.refreshComButton.Size = new System.Drawing.Size(75, 23);
            this.refreshComButton.TabIndex = 2;
            this.refreshComButton.Text = "Refresh";
            this.refreshComButton.UseVisualStyleBackColor = true;
            this.refreshComButton.Click += new System.EventHandler(this.RefreshComButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.profileSetCreateButton);
            this.groupBox1.Location = new System.Drawing.Point(7, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(93, 75);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profilesets";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 44);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 23);
            this.button2.TabIndex = 21;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // profileSetCreateButton
            // 
            this.profileSetCreateButton.Location = new System.Drawing.Point(6, 15);
            this.profileSetCreateButton.Name = "profileSetCreateButton";
            this.profileSetCreateButton.Size = new System.Drawing.Size(81, 23);
            this.profileSetCreateButton.TabIndex = 20;
            this.profileSetCreateButton.Text = "Add ";
            this.profileSetCreateButton.UseVisualStyleBackColor = true;
            this.profileSetCreateButton.Click += new System.EventHandler(this.ProfileSetCreateButton_Click);
            // 
            // profileSetComboBox
            // 
            this.profileSetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileSetComboBox.FormattingEnabled = true;
            this.profileSetComboBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.profileSetComboBox.Location = new System.Drawing.Point(105, 59);
            this.profileSetComboBox.Name = "profileSetComboBox";
            this.profileSetComboBox.Size = new System.Drawing.Size(366, 21);
            this.profileSetComboBox.TabIndex = 7;
            this.profileSetComboBox.SelectedIndexChanged += new System.EventHandler(this.ProfileSetBox_SelectedIndexChanged);
            // 
            // profileListView
            // 
            this.profileListView.AllowColumnReorder = true;
            this.profileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.profileName,
            this.typeName,
            this.shorcutName});
            this.profileListView.FullRowSelect = true;
            this.profileListView.GridLines = true;
            this.profileListView.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.profileListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.profileListView.Location = new System.Drawing.Point(105, 86);
            this.profileListView.MultiSelect = false;
            this.profileListView.Name = "profileListView";
            this.profileListView.Size = new System.Drawing.Size(366, 300);
            this.profileListView.TabIndex = 25;
            this.profileListView.UseCompatibleStateImageBehavior = false;
            this.profileListView.View = System.Windows.Forms.View.Details;
            this.profileListView.SelectedIndexChanged += new System.EventHandler(this.ProfileListView_SelectedIndexChanged);
            // 
            // profileName
            // 
            this.profileName.Text = "Profile Name";
            this.profileName.Width = 113;
            // 
            // typeName
            // 
            this.typeName.Text = "Profile Type";
            this.typeName.Width = 172;
            // 
            // shorcutName
            // 
            this.shorcutName.Text = "Shortcut";
            this.shorcutName.Width = 77;
            // 
            // StaticGroupBox
            // 
            this.StaticGroupBox.Controls.Add(this.staticColortHotkeyLabel);
            this.StaticGroupBox.Controls.Add(this.StaticBrightnessNUD);
            this.StaticGroupBox.Controls.Add(this.label4);
            this.StaticGroupBox.Controls.Add(this.label1);
            this.StaticGroupBox.Controls.Add(this.StaticBrightnessTrackbar);
            this.StaticGroupBox.Controls.Add(this.staticProfileColorButton);
            this.StaticGroupBox.Controls.Add(this.label2);
            this.StaticGroupBox.Location = new System.Drawing.Point(846, 394);
            this.StaticGroupBox.Name = "StaticGroupBox";
            this.StaticGroupBox.Size = new System.Drawing.Size(414, 380);
            this.StaticGroupBox.TabIndex = 10;
            this.StaticGroupBox.TabStop = false;
            this.StaticGroupBox.Text = "Selected LED Profile";
            // 
            // staticColortHotkeyLabel
            // 
            this.staticColortHotkeyLabel.AutoSize = true;
            this.staticColortHotkeyLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.staticColortHotkeyLabel.Location = new System.Drawing.Point(57, 350);
            this.staticColortHotkeyLabel.Name = "staticColortHotkeyLabel";
            this.staticColortHotkeyLabel.Size = new System.Drawing.Size(69, 13);
            this.staticColortHotkeyLabel.TabIndex = 28;
            this.staticColortHotkeyLabel.Text = "Not assigned";
            // 
            // StaticBrightnessNUD
            // 
            this.StaticBrightnessNUD.Location = new System.Drawing.Point(333, 136);
            this.StaticBrightnessNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.StaticBrightnessNUD.Name = "StaticBrightnessNUD";
            this.StaticBrightnessNUD.Size = new System.Drawing.Size(75, 20);
            this.StaticBrightnessNUD.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 350);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Hotkey:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Brightness:";
            // 
            // StaticBrightnessTrackbar
            // 
            this.StaticBrightnessTrackbar.Location = new System.Drawing.Point(9, 162);
            this.StaticBrightnessTrackbar.Maximum = 255;
            this.StaticBrightnessTrackbar.Name = "StaticBrightnessTrackbar";
            this.StaticBrightnessTrackbar.Size = new System.Drawing.Size(399, 45);
            this.StaticBrightnessTrackbar.TabIndex = 2;
            this.StaticBrightnessTrackbar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // staticProfileColorButton
            // 
            this.staticProfileColorButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.staticProfileColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.staticProfileColorButton.Location = new System.Drawing.Point(9, 55);
            this.staticProfileColorButton.Name = "staticProfileColorButton";
            this.staticProfileColorButton.Size = new System.Drawing.Size(399, 42);
            this.staticProfileColorButton.TabIndex = 0;
            this.staticProfileColorButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Color:";
            // 
            // AmbilightGroupBox
            // 
            this.AmbilightGroupBox.Controls.Add(this.AmbilightPrecisionTrackBar);
            this.AmbilightGroupBox.Controls.Add(this.label9);
            this.AmbilightGroupBox.Controls.Add(this.AmbilightBrightnessNumericUpDown);
            this.AmbilightGroupBox.Controls.Add(this.AmbilightBrightnessTrackBar);
            this.AmbilightGroupBox.Controls.Add(this.AmbilightRecordStatusLabel);
            this.AmbilightGroupBox.Controls.Add(this.label12);
            this.AmbilightGroupBox.Controls.Add(this.groupBox7);
            this.AmbilightGroupBox.Controls.Add(this.AmbilightHotkeyLabel);
            this.AmbilightGroupBox.Controls.Add(this.label11);
            this.AmbilightGroupBox.Location = new System.Drawing.Point(6, 394);
            this.AmbilightGroupBox.Name = "AmbilightGroupBox";
            this.AmbilightGroupBox.Size = new System.Drawing.Size(414, 380);
            this.AmbilightGroupBox.TabIndex = 29;
            this.AmbilightGroupBox.TabStop = false;
            this.AmbilightGroupBox.Text = "Selected LED Profile";
            // 
            // AmbilightPrecisionTrackBar
            // 
            this.AmbilightPrecisionTrackBar.Location = new System.Drawing.Point(9, 178);
            this.AmbilightPrecisionTrackBar.Maximum = 11;
            this.AmbilightPrecisionTrackBar.Minimum = 1;
            this.AmbilightPrecisionTrackBar.Name = "AmbilightPrecisionTrackBar";
            this.AmbilightPrecisionTrackBar.Size = new System.Drawing.Size(392, 45);
            this.AmbilightPrecisionTrackBar.TabIndex = 43;
            this.AmbilightPrecisionTrackBar.TickFrequency = 2;
            this.AmbilightPrecisionTrackBar.Value = 11;
            this.AmbilightPrecisionTrackBar.ValueChanged += new System.EventHandler(this.AmbilightPrecisionTrackBar_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 58;
            this.label9.Text = "Brightness:";
            // 
            // AmbilightBrightnessNumericUpDown
            // 
            this.AmbilightBrightnessNumericUpDown.Location = new System.Drawing.Point(333, 101);
            this.AmbilightBrightnessNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.AmbilightBrightnessNumericUpDown.Name = "AmbilightBrightnessNumericUpDown";
            this.AmbilightBrightnessNumericUpDown.Size = new System.Drawing.Size(75, 20);
            this.AmbilightBrightnessNumericUpDown.TabIndex = 57;
            // 
            // AmbilightBrightnessTrackBar
            // 
            this.AmbilightBrightnessTrackBar.Location = new System.Drawing.Point(6, 127);
            this.AmbilightBrightnessTrackBar.Maximum = 255;
            this.AmbilightBrightnessTrackBar.Name = "AmbilightBrightnessTrackBar";
            this.AmbilightBrightnessTrackBar.Size = new System.Drawing.Size(402, 45);
            this.AmbilightBrightnessTrackBar.TabIndex = 56;
            this.AmbilightBrightnessTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.AmbilightBrightnessTrackBar.Value = 1;
            // 
            // AmbilightRecordStatusLabel
            // 
            this.AmbilightRecordStatusLabel.AutoSize = true;
            this.AmbilightRecordStatusLabel.Location = new System.Drawing.Point(302, 350);
            this.AmbilightRecordStatusLabel.Name = "AmbilightRecordStatusLabel";
            this.AmbilightRecordStatusLabel.Size = new System.Drawing.Size(99, 13);
            this.AmbilightRecordStatusLabel.TabIndex = 55;
            this.AmbilightRecordStatusLabel.Text = "Unloaded (inactive)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(227, 350);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 54;
            this.label12.Text = "Driver status:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.AmbilightCaptureProfileComboBox);
            this.groupBox7.Controls.Add(this.AmbilightCreateCaptureProfileButton);
            this.groupBox7.Controls.Add(this.AmbilightCaptureScreenComboBox);
            this.groupBox7.Controls.Add(this.AmbilightShowCaptureAreasButton);
            this.groupBox7.Location = new System.Drawing.Point(10, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(391, 76);
            this.groupBox7.TabIndex = 47;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Capture Screen";
            // 
            // AmbilightCaptureProfileComboBox
            // 
            this.AmbilightCaptureProfileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AmbilightCaptureProfileComboBox.FormattingEnabled = true;
            this.AmbilightCaptureProfileComboBox.Location = new System.Drawing.Point(6, 46);
            this.AmbilightCaptureProfileComboBox.Name = "AmbilightCaptureProfileComboBox";
            this.AmbilightCaptureProfileComboBox.Size = new System.Drawing.Size(203, 21);
            this.AmbilightCaptureProfileComboBox.TabIndex = 40;
            // 
            // AmbilightCreateCaptureProfileButton
            // 
            this.AmbilightCreateCaptureProfileButton.Enabled = false;
            this.AmbilightCreateCaptureProfileButton.Location = new System.Drawing.Point(246, 17);
            this.AmbilightCreateCaptureProfileButton.Name = "AmbilightCreateCaptureProfileButton";
            this.AmbilightCreateCaptureProfileButton.Size = new System.Drawing.Size(139, 23);
            this.AmbilightCreateCaptureProfileButton.TabIndex = 42;
            this.AmbilightCreateCaptureProfileButton.Text = "Custom...";
            this.AmbilightCreateCaptureProfileButton.UseVisualStyleBackColor = true;
            // 
            // AmbilightCaptureScreenComboBox
            // 
            this.AmbilightCaptureScreenComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AmbilightCaptureScreenComboBox.FormattingEnabled = true;
            this.AmbilightCaptureScreenComboBox.Location = new System.Drawing.Point(6, 19);
            this.AmbilightCaptureScreenComboBox.Name = "AmbilightCaptureScreenComboBox";
            this.AmbilightCaptureScreenComboBox.Size = new System.Drawing.Size(203, 21);
            this.AmbilightCaptureScreenComboBox.TabIndex = 46;
            // 
            // AmbilightShowCaptureAreasButton
            // 
            this.AmbilightShowCaptureAreasButton.Location = new System.Drawing.Point(246, 44);
            this.AmbilightShowCaptureAreasButton.Name = "AmbilightShowCaptureAreasButton";
            this.AmbilightShowCaptureAreasButton.Size = new System.Drawing.Size(139, 23);
            this.AmbilightShowCaptureAreasButton.TabIndex = 39;
            this.AmbilightShowCaptureAreasButton.Text = "Show capture areas";
            this.AmbilightShowCaptureAreasButton.UseVisualStyleBackColor = true;
            this.AmbilightShowCaptureAreasButton.Click += new System.EventHandler(this.AmbilightShowCaptureAreasButton_Click);
            // 
            // AmbilightHotkeyLabel
            // 
            this.AmbilightHotkeyLabel.AutoSize = true;
            this.AmbilightHotkeyLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AmbilightHotkeyLabel.Location = new System.Drawing.Point(56, 350);
            this.AmbilightHotkeyLabel.Name = "AmbilightHotkeyLabel";
            this.AmbilightHotkeyLabel.Size = new System.Drawing.Size(69, 13);
            this.AmbilightHotkeyLabel.TabIndex = 30;
            this.AmbilightHotkeyLabel.Text = "Not assigned";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 350);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "Hotkey:";
            // 
            // RainbowGroupBox
            // 
            this.RainbowGroupBox.Controls.Add(this.RainbowSpeedNud);
            this.RainbowGroupBox.Controls.Add(this.label8);
            this.RainbowGroupBox.Controls.Add(this.RainbowSpeedTrackBar);
            this.RainbowGroupBox.Controls.Add(this.label5);
            this.RainbowGroupBox.Controls.Add(this.label6);
            this.RainbowGroupBox.Controls.Add(this.RainbowBrightnessNud);
            this.RainbowGroupBox.Controls.Add(this.label7);
            this.RainbowGroupBox.Controls.Add(this.RainbowBrightnessTrackBar);
            this.RainbowGroupBox.Location = new System.Drawing.Point(426, 394);
            this.RainbowGroupBox.Name = "RainbowGroupBox";
            this.RainbowGroupBox.Size = new System.Drawing.Size(414, 380);
            this.RainbowGroupBox.TabIndex = 29;
            this.RainbowGroupBox.TabStop = false;
            this.RainbowGroupBox.Text = "Selected LED Profile";
            // 
            // RainbowSpeedNud
            // 
            this.RainbowSpeedNud.Location = new System.Drawing.Point(333, 26);
            this.RainbowSpeedNud.Name = "RainbowSpeedNud";
            this.RainbowSpeedNud.Size = new System.Drawing.Size(75, 20);
            this.RainbowSpeedNud.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Speed:";
            // 
            // RainbowSpeedTrackBar
            // 
            this.RainbowSpeedTrackBar.Location = new System.Drawing.Point(9, 52);
            this.RainbowSpeedTrackBar.Maximum = 100;
            this.RainbowSpeedTrackBar.Name = "RainbowSpeedTrackBar";
            this.RainbowSpeedTrackBar.Size = new System.Drawing.Size(399, 45);
            this.RainbowSpeedTrackBar.TabIndex = 29;
            this.RainbowSpeedTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.RainbowSpeedTrackBar.Value = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Location = new System.Drawing.Point(56, 350);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Not assigned";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 350);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Hotkey:";
            // 
            // RainbowBrightnessNud
            // 
            this.RainbowBrightnessNud.Location = new System.Drawing.Point(333, 138);
            this.RainbowBrightnessNud.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.RainbowBrightnessNud.Name = "RainbowBrightnessNud";
            this.RainbowBrightnessNud.Size = new System.Drawing.Size(75, 20);
            this.RainbowBrightnessNud.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Brightness:";
            // 
            // RainbowBrightnessTrackBar
            // 
            this.RainbowBrightnessTrackBar.Location = new System.Drawing.Point(9, 162);
            this.RainbowBrightnessTrackBar.Maximum = 255;
            this.RainbowBrightnessTrackBar.Name = "RainbowBrightnessTrackBar";
            this.RainbowBrightnessTrackBar.Size = new System.Drawing.Size(399, 45);
            this.RainbowBrightnessTrackBar.TabIndex = 2;
            this.RainbowBrightnessTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // NullGroupBox
            // 
            this.NullGroupBox.Controls.Add(this.label3);
            this.NullGroupBox.Location = new System.Drawing.Point(477, 6);
            this.NullGroupBox.Name = "NullGroupBox";
            this.NullGroupBox.Size = new System.Drawing.Size(414, 380);
            this.NullGroupBox.TabIndex = 26;
            this.NullGroupBox.TabStop = false;
            this.NullGroupBox.Text = "Selected LED Profile";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(120, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select a LED-profile to edit it here.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.newProfileButton);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(6, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(93, 80);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Profiles";
            // 
            // newProfileButton
            // 
            this.newProfileButton.Location = new System.Drawing.Point(6, 19);
            this.newProfileButton.Name = "newProfileButton";
            this.newProfileButton.Size = new System.Drawing.Size(81, 23);
            this.newProfileButton.TabIndex = 11;
            this.newProfileButton.Text = "Add";
            this.newProfileButton.UseVisualStyleBackColor = true;
            this.newProfileButton.Click += new System.EventHandler(this.NewProfileButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Remove";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.saveToXmlButton);
            this.groupBox4.Controls.Add(this.deactivateLEDsButton);
            this.groupBox4.Location = new System.Drawing.Point(6, 309);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(94, 77);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Control";
            // 
            // saveToXmlButton
            // 
            this.saveToXmlButton.Location = new System.Drawing.Point(7, 19);
            this.saveToXmlButton.Name = "saveToXmlButton";
            this.saveToXmlButton.Size = new System.Drawing.Size(81, 23);
            this.saveToXmlButton.TabIndex = 29;
            this.saveToXmlButton.Text = "Save all";
            this.saveToXmlButton.UseVisualStyleBackColor = true;
            this.saveToXmlButton.Click += new System.EventHandler(this.SaveToXmlButton_Click);
            // 
            // deactivateLEDsButton
            // 
            this.deactivateLEDsButton.Location = new System.Drawing.Point(7, 48);
            this.deactivateLEDsButton.Name = "deactivateLEDsButton";
            this.deactivateLEDsButton.Size = new System.Drawing.Size(81, 23);
            this.deactivateLEDsButton.TabIndex = 27;
            this.deactivateLEDsButton.Text = "Turn off";
            this.deactivateLEDsButton.UseVisualStyleBackColor = true;
            this.deactivateLEDsButton.Click += new System.EventHandler(this.DeactivateLEDsButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.groupBox5.Controls.Add(this.SelectActiveProfileButton);
            this.groupBox5.Controls.Add(this.activeProfileLabel);
            this.groupBox5.Controls.Add(this.FpsLabel);
            this.groupBox5.Location = new System.Drawing.Point(105, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(366, 48);
            this.groupBox5.TabIndex = 38;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Active profile";
            // 
            // SelectActiveProfileButton
            // 
            this.SelectActiveProfileButton.Location = new System.Drawing.Point(285, 17);
            this.SelectActiveProfileButton.Name = "SelectActiveProfileButton";
            this.SelectActiveProfileButton.Size = new System.Drawing.Size(75, 23);
            this.SelectActiveProfileButton.TabIndex = 32;
            this.SelectActiveProfileButton.Text = "Select";
            this.SelectActiveProfileButton.UseVisualStyleBackColor = true;
            this.SelectActiveProfileButton.Click += new System.EventHandler(this.SelectActiveProfileButton_Click);
            // 
            // activeProfileLabel
            // 
            this.activeProfileLabel.AutoSize = true;
            this.activeProfileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activeProfileLabel.Location = new System.Drawing.Point(6, 22);
            this.activeProfileLabel.Name = "activeProfileLabel";
            this.activeProfileLabel.Size = new System.Drawing.Size(37, 13);
            this.activeProfileLabel.TabIndex = 30;
            this.activeProfileLabel.Text = "None";
            // 
            // FpsLabel
            // 
            this.FpsLabel.AutoSize = true;
            this.FpsLabel.Location = new System.Drawing.Point(246, 22);
            this.FpsLabel.Name = "FpsLabel";
            this.FpsLabel.Size = new System.Drawing.Size(33, 13);
            this.FpsLabel.TabIndex = 39;
            this.FpsLabel.Text = "-- Fps";
            // 
            // mainFormBindingSource
            // 
            this.mainFormBindingSource.DataSource = typeof(LedController.MainForm);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.ProfilesTabPage);
            this.TabControl.Controls.Add(this.VisualizerTabPage);
            this.TabControl.Controls.Add(this.BassTabPage);
            this.TabControl.Controls.Add(this.LogTabPage);
            this.TabControl.Controls.Add(this.ConfigTabPage);
            this.TabControl.Location = new System.Drawing.Point(5, 5);
            this.TabControl.Multiline = true;
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(907, 796);
            this.TabControl.TabIndex = 41;
            // 
            // BassTabPage
            // 
            this.BassTabPage.Controls.Add(this.AudioLevelPanel);
            this.BassTabPage.Controls.Add(this.AudioNumBandsLabel);
            this.BassTabPage.Controls.Add(this.numBandsTrackBar);
            this.BassTabPage.Controls.Add(this.StartAudioVisButton);
            this.BassTabPage.Controls.Add(this.AudioVisualizerPanel);
            this.BassTabPage.Location = new System.Drawing.Point(4, 22);
            this.BassTabPage.Name = "BassTabPage";
            this.BassTabPage.Size = new System.Drawing.Size(899, 770);
            this.BassTabPage.TabIndex = 4;
            this.BassTabPage.Text = "Audio";
            this.BassTabPage.UseVisualStyleBackColor = true;
            // 
            // AudioLevelPanel
            // 
            this.AudioLevelPanel.Location = new System.Drawing.Point(13, 358);
            this.AudioLevelPanel.Name = "AudioLevelPanel";
            this.AudioLevelPanel.Size = new System.Drawing.Size(751, 23);
            this.AudioLevelPanel.TabIndex = 3;
            // 
            // AudioNumBandsLabel
            // 
            this.AudioNumBandsLabel.AutoSize = true;
            this.AudioNumBandsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioNumBandsLabel.Location = new System.Drawing.Point(857, 363);
            this.AudioNumBandsLabel.Name = "AudioNumBandsLabel";
            this.AudioNumBandsLabel.Size = new System.Drawing.Size(28, 13);
            this.AudioNumBandsLabel.TabIndex = 2;
            this.AudioNumBandsLabel.Text = "250";
            // 
            // numBandsTrackBar
            // 
            this.numBandsTrackBar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.numBandsTrackBar.Location = new System.Drawing.Point(851, 3);
            this.numBandsTrackBar.Maximum = 250;
            this.numBandsTrackBar.Name = "numBandsTrackBar";
            this.numBandsTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.numBandsTrackBar.Size = new System.Drawing.Size(45, 349);
            this.numBandsTrackBar.TabIndex = 0;
            this.numBandsTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.numBandsTrackBar.Scroll += new System.EventHandler(this.NumBandsTrackBar_Scroll);
            // 
            // StartAudioVisButton
            // 
            this.StartAudioVisButton.Location = new System.Drawing.Point(770, 358);
            this.StartAudioVisButton.Name = "StartAudioVisButton";
            this.StartAudioVisButton.Size = new System.Drawing.Size(75, 23);
            this.StartAudioVisButton.TabIndex = 0;
            this.StartAudioVisButton.Text = "Start";
            this.StartAudioVisButton.UseVisualStyleBackColor = true;
            this.StartAudioVisButton.Click += new System.EventHandler(this.StartAudioVisButton_Click);
            // 
            // AudioVisualizerPanel
            // 
            this.AudioVisualizerPanel.Location = new System.Drawing.Point(13, 13);
            this.AudioVisualizerPanel.Name = "AudioVisualizerPanel";
            this.AudioVisualizerPanel.Size = new System.Drawing.Size(832, 339);
            this.AudioVisualizerPanel.TabIndex = 0;
            // 
            // audioUpdateTimer
            // 
            this.audioUpdateTimer.Interval = 16;
            this.audioUpdateTimer.Tick += new System.EventHandler(this.AudioUpdateTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1877, 871);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Led Controller";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.TrayContextMenu.ResumeLayout(false);
            this.ConfigTabPage.ResumeLayout(false);
            this.ConfigTabPage.PerformLayout();
            this.LogTabPage.ResumeLayout(false);
            this.LogTabPage.PerformLayout();
            this.VisualizerTabPage.ResumeLayout(false);
            this.VisualizerTabPage.PerformLayout();
            this.ProfilesTabPage.ResumeLayout(false);
            this.MusicGroupBox.ResumeLayout(false);
            this.MusicGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MusicBrightnessNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MusicBrightnessTrackbar)).EndInit();
            this.ConnectionGB.ResumeLayout(false);
            this.ConnectionGB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.StaticGroupBox.ResumeLayout(false);
            this.StaticGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StaticBrightnessNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StaticBrightnessTrackbar)).EndInit();
            this.AmbilightGroupBox.ResumeLayout(false);
            this.AmbilightGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightPrecisionTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightBrightnessNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightBrightnessTrackBar)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.RainbowGroupBox.ResumeLayout(false);
            this.RainbowGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowSpeedNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowSpeedTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowBrightnessNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowBrightnessTrackBar)).EndInit();
            this.NullGroupBox.ResumeLayout(false);
            this.NullGroupBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.BassTabPage.ResumeLayout(false);
            this.BassTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBandsTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.NotifyIcon MainNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer UpdateLogTimer;
        private System.Windows.Forms.TabPage ConfigTabPage;
        private System.Windows.Forms.CheckBox OpenOnStartupCheckBox;
        private System.Windows.Forms.CheckBox StartMinimizedCheckBox;
        private System.Windows.Forms.CheckBox ConnectOnOpenCheckBox;
        private System.Windows.Forms.TabPage LogTabPage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox LogUpdateIntervalComboBox;
        private System.Windows.Forms.Button CopyLogToClipboardButton;
        private System.Windows.Forms.Button ArchiveLogButton;
        private System.Windows.Forms.RichTextBox LogRichTextBox;
        private System.Windows.Forms.TabPage VisualizerTabPage;
        private System.Windows.Forms.Button OpenVisualizerFormButton;
        private System.Windows.Forms.Panel VisualizerPanel;
        private System.Windows.Forms.Label StripInfoLabel;
        private System.Windows.Forms.TabPage ProfilesTabPage;
        private System.Windows.Forms.GroupBox ConnectionGB;
        private System.Windows.Forms.ComboBox ComPortComboBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button connectComButton;
        private System.Windows.Forms.Button refreshComButton;
        private System.Windows.Forms.ComboBox profileSetComboBox;
        private System.Windows.Forms.ListView profileListView;
        private System.Windows.Forms.ColumnHeader profileName;
        private System.Windows.Forms.ColumnHeader typeName;
        private System.Windows.Forms.ColumnHeader shorcutName;
        private System.Windows.Forms.GroupBox StaticGroupBox;
        private System.Windows.Forms.Label staticColortHotkeyLabel;
        private System.Windows.Forms.NumericUpDown StaticBrightnessNUD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar StaticBrightnessTrackbar;
        private System.Windows.Forms.Button staticProfileColorButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox AmbilightGroupBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown AmbilightBrightnessNumericUpDown;
        private System.Windows.Forms.TrackBar AmbilightBrightnessTrackBar;
        private System.Windows.Forms.Label AmbilightRecordStatusLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox AmbilightCaptureProfileComboBox;
        private System.Windows.Forms.Button AmbilightCreateCaptureProfileButton;
        private System.Windows.Forms.ComboBox AmbilightCaptureScreenComboBox;
        private System.Windows.Forms.Button AmbilightShowCaptureAreasButton;
        private System.Windows.Forms.Label AmbilightHotkeyLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox RainbowGroupBox;
        private System.Windows.Forms.NumericUpDown RainbowSpeedNud;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar RainbowSpeedTrackBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown RainbowBrightnessNud;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar RainbowBrightnessTrackBar;
        private System.Windows.Forms.GroupBox NullGroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button profileSetCreateButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button newProfileButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button saveToXmlButton;
        private System.Windows.Forms.Button deactivateLEDsButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button SelectActiveProfileButton;
        private System.Windows.Forms.Label activeProfileLabel;
        private System.Windows.Forms.Label FpsLabel;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage BassTabPage;
        private System.Windows.Forms.GroupBox MusicGroupBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label MusicDeviceLabel;
        private System.Windows.Forms.Button MusicSelectDeviceButton;
        private System.Windows.Forms.Label MusicHotkeyLabel;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown MusicBrightnessNud;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TrackBar MusicBrightnessTrackbar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox MusicEffectComboBox;
        private System.Windows.Forms.Button StartAudioVisButton;
        private System.Windows.Forms.Panel AudioVisualizerPanel;
        private System.Windows.Forms.Timer audioUpdateTimer;
        private System.Windows.Forms.Label AudioNumBandsLabel;
        private System.Windows.Forms.TrackBar numBandsTrackBar;
        private System.Windows.Forms.Panel AudioLevelPanel;
        private System.Windows.Forms.ToolStripMenuItem ActiveProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ShowHideToolStripMenuItem;
        private System.Windows.Forms.TrackBar AmbilightPrecisionTrackBar;
        private System.Windows.Forms.BindingSource mainFormBindingSource;
    }
}

