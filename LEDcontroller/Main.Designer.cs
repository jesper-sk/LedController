﻿namespace LedController
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
            this.StaticBrightnessNUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.StaticBrightnessTrackbar = new System.Windows.Forms.TrackBar();
            this.staticProfileColorButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.AmbilightGroupBox = new System.Windows.Forms.GroupBox();
            this.AmbilightUpsGroupBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.AmbilightLimitUpsTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AmbilightNumHueSlicesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AmbilightMedianRadioButton = new System.Windows.Forms.RadioButton();
            this.AmbilightAverageRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AmbilightBrightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.AmbilightBrightnessNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.AmbilightPrecisionTrackBar = new System.Windows.Forms.TrackBar();
            this.AmbilightPrecisionNud = new System.Windows.Forms.NumericUpDown();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.AmbilightSmoothingTrackbar = new System.Windows.Forms.TrackBar();
            this.AmbilightSmoothingNud = new System.Windows.Forms.NumericUpDown();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.AmbilightCaptureProfileComboBox = new System.Windows.Forms.ComboBox();
            this.AmbilightCreateCaptureProfileButton = new System.Windows.Forms.Button();
            this.AmbilightCaptureScreenComboBox = new System.Windows.Forms.ComboBox();
            this.AmbilightShowCaptureAreasButton = new System.Windows.Forms.Button();
            this.RainbowGroupBox = new System.Windows.Forms.GroupBox();
            this.RainbowSpeedNud = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.RainbowSpeedTrackBar = new System.Windows.Forms.TrackBar();
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.BassTabPage = new System.Windows.Forms.TabPage();
            this.visStatusLabel = new System.Windows.Forms.Label();
            this.ShowAnalyzerButton = new System.Windows.Forms.Button();
            this.AudioLevelPanel = new System.Windows.Forms.Panel();
            this.AudioNumBandsLabel = new System.Windows.Forms.Label();
            this.numBandsTrackBar = new System.Windows.Forms.TrackBar();
            this.StartAudioVisButton = new System.Windows.Forms.Button();
            this.AudioVisualizerPanel = new System.Windows.Forms.Panel();
            this.audioUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.mainFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button3 = new System.Windows.Forms.Button();
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
            this.AmbilightUpsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightLimitUpsTrackBar)).BeginInit();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightNumHueSlicesNumericUpDown)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightBrightnessTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightBrightnessNumericUpDown)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightPrecisionTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightPrecisionNud)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightSmoothingTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightSmoothingNud)).BeginInit();
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
            this.TabControl.SuspendLayout();
            this.BassTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBandsTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).BeginInit();
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
            this.MainNotifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainNotifyIcon_MouseDown);
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
            this.ConfigTabPage.Size = new System.Drawing.Size(1590, 779);
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
            this.LogTabPage.Size = new System.Drawing.Size(1590, 779);
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
            this.VisualizerTabPage.Size = new System.Drawing.Size(1590, 779);
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
            this.ProfilesTabPage.Size = new System.Drawing.Size(1590, 779);
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
            this.MusicBrightnessTrackbar.TickFrequency = 8;
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
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 360);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Device:";
            // 
            // MusicDeviceLabel
            // 
            this.MusicDeviceLabel.AutoSize = true;
            this.MusicDeviceLabel.Location = new System.Drawing.Point(56, 360);
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
            this.ComPortComboBox.Size = new System.Drawing.Size(81, 21);
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
            this.connectComButton.Size = new System.Drawing.Size(81, 23);
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
            this.refreshComButton.Size = new System.Drawing.Size(82, 23);
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
            this.profileSetComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ProfileSetComboBox_MouseClick);
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
            this.StaticGroupBox.Controls.Add(this.StaticBrightnessNUD);
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
            this.StaticBrightnessTrackbar.TickFrequency = 8;
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
            this.AmbilightGroupBox.Controls.Add(this.AmbilightUpsGroupBox);
            this.AmbilightGroupBox.Controls.Add(this.groupBox9);
            this.AmbilightGroupBox.Controls.Add(this.groupBox3);
            this.AmbilightGroupBox.Controls.Add(this.groupBox8);
            this.AmbilightGroupBox.Controls.Add(this.groupBox6);
            this.AmbilightGroupBox.Controls.Add(this.groupBox7);
            this.AmbilightGroupBox.Location = new System.Drawing.Point(6, 394);
            this.AmbilightGroupBox.Name = "AmbilightGroupBox";
            this.AmbilightGroupBox.Size = new System.Drawing.Size(414, 380);
            this.AmbilightGroupBox.TabIndex = 29;
            this.AmbilightGroupBox.TabStop = false;
            this.AmbilightGroupBox.Text = "Selected LED Profile";
            // 
            // AmbilightUpsGroupBox
            // 
            this.AmbilightUpsGroupBox.Controls.Add(this.label5);
            this.AmbilightUpsGroupBox.Controls.Add(this.AmbilightLimitUpsTrackBar);
            this.AmbilightUpsGroupBox.Location = new System.Drawing.Point(246, 197);
            this.AmbilightUpsGroupBox.Name = "AmbilightUpsGroupBox";
            this.AmbilightUpsGroupBox.Size = new System.Drawing.Size(162, 70);
            this.AmbilightUpsGroupBox.TabIndex = 61;
            this.AmbilightUpsGroupBox.TabStop = false;
            this.AmbilightUpsGroupBox.Text = "Limit UPS";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 62;
            this.label5.Text = "(30 UPS)";
            // 
            // AmbilightLimitUpsTrackBar
            // 
            this.AmbilightLimitUpsTrackBar.Location = new System.Drawing.Point(6, 19);
            this.AmbilightLimitUpsTrackBar.Maximum = 60;
            this.AmbilightLimitUpsTrackBar.Minimum = 5;
            this.AmbilightLimitUpsTrackBar.Name = "AmbilightLimitUpsTrackBar";
            this.AmbilightLimitUpsTrackBar.Size = new System.Drawing.Size(150, 45);
            this.AmbilightLimitUpsTrackBar.SmallChange = 5;
            this.AmbilightLimitUpsTrackBar.TabIndex = 61;
            this.AmbilightLimitUpsTrackBar.TickFrequency = 5;
            this.AmbilightLimitUpsTrackBar.Value = 5;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label4);
            this.groupBox9.Controls.Add(this.AmbilightNumHueSlicesNumericUpDown);
            this.groupBox9.Controls.Add(this.AmbilightMedianRadioButton);
            this.groupBox9.Controls.Add(this.AmbilightAverageRadioButton);
            this.groupBox9.Location = new System.Drawing.Point(246, 101);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(162, 90);
            this.groupBox9.TabIndex = 42;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Measurement";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(6, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 60;
            this.label4.Text = "#Hue Slices:";
            // 
            // AmbilightNumHueSlicesNumericUpDown
            // 
            this.AmbilightNumHueSlicesNumericUpDown.Enabled = false;
            this.AmbilightNumHueSlicesNumericUpDown.Location = new System.Drawing.Point(80, 64);
            this.AmbilightNumHueSlicesNumericUpDown.Name = "AmbilightNumHueSlicesNumericUpDown";
            this.AmbilightNumHueSlicesNumericUpDown.Size = new System.Drawing.Size(76, 20);
            this.AmbilightNumHueSlicesNumericUpDown.TabIndex = 59;
            // 
            // AmbilightMedianRadioButton
            // 
            this.AmbilightMedianRadioButton.AutoSize = true;
            this.AmbilightMedianRadioButton.Location = new System.Drawing.Point(96, 19);
            this.AmbilightMedianRadioButton.Name = "AmbilightMedianRadioButton";
            this.AmbilightMedianRadioButton.Size = new System.Drawing.Size(60, 17);
            this.AmbilightMedianRadioButton.TabIndex = 1;
            this.AmbilightMedianRadioButton.Text = "Median";
            this.AmbilightMedianRadioButton.UseVisualStyleBackColor = true;
            // 
            // AmbilightAverageRadioButton
            // 
            this.AmbilightAverageRadioButton.AutoSize = true;
            this.AmbilightAverageRadioButton.Checked = true;
            this.AmbilightAverageRadioButton.Location = new System.Drawing.Point(6, 19);
            this.AmbilightAverageRadioButton.Name = "AmbilightAverageRadioButton";
            this.AmbilightAverageRadioButton.Size = new System.Drawing.Size(65, 17);
            this.AmbilightAverageRadioButton.TabIndex = 0;
            this.AmbilightAverageRadioButton.TabStop = true;
            this.AmbilightAverageRadioButton.Text = "Average";
            this.AmbilightAverageRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AmbilightBrightnessTrackBar);
            this.groupBox3.Controls.Add(this.AmbilightBrightnessNumericUpDown);
            this.groupBox3.Location = new System.Drawing.Point(6, 101);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(74, 272);
            this.groupBox3.TabIndex = 42;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Brightness";
            // 
            // AmbilightBrightnessTrackBar
            // 
            this.AmbilightBrightnessTrackBar.Location = new System.Drawing.Point(15, 19);
            this.AmbilightBrightnessTrackBar.Maximum = 255;
            this.AmbilightBrightnessTrackBar.Name = "AmbilightBrightnessTrackBar";
            this.AmbilightBrightnessTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.AmbilightBrightnessTrackBar.Size = new System.Drawing.Size(45, 218);
            this.AmbilightBrightnessTrackBar.TabIndex = 56;
            this.AmbilightBrightnessTrackBar.TickFrequency = 8;
            this.AmbilightBrightnessTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.AmbilightBrightnessTrackBar.Value = 1;
            // 
            // AmbilightBrightnessNumericUpDown
            // 
            this.AmbilightBrightnessNumericUpDown.Location = new System.Drawing.Point(6, 243);
            this.AmbilightBrightnessNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.AmbilightBrightnessNumericUpDown.Name = "AmbilightBrightnessNumericUpDown";
            this.AmbilightBrightnessNumericUpDown.Size = new System.Drawing.Size(62, 20);
            this.AmbilightBrightnessNumericUpDown.TabIndex = 57;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.AmbilightPrecisionTrackBar);
            this.groupBox8.Controls.Add(this.AmbilightPrecisionNud);
            this.groupBox8.Location = new System.Drawing.Point(86, 101);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(74, 272);
            this.groupBox8.TabIndex = 58;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Precision";
            // 
            // AmbilightPrecisionTrackBar
            // 
            this.AmbilightPrecisionTrackBar.Location = new System.Drawing.Point(15, 19);
            this.AmbilightPrecisionTrackBar.Maximum = 100;
            this.AmbilightPrecisionTrackBar.Minimum = 1;
            this.AmbilightPrecisionTrackBar.Name = "AmbilightPrecisionTrackBar";
            this.AmbilightPrecisionTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.AmbilightPrecisionTrackBar.Size = new System.Drawing.Size(45, 218);
            this.AmbilightPrecisionTrackBar.TabIndex = 56;
            this.AmbilightPrecisionTrackBar.TickFrequency = 10;
            this.AmbilightPrecisionTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.AmbilightPrecisionTrackBar.Value = 1;
            // 
            // AmbilightPrecisionNud
            // 
            this.AmbilightPrecisionNud.Location = new System.Drawing.Point(6, 243);
            this.AmbilightPrecisionNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AmbilightPrecisionNud.Name = "AmbilightPrecisionNud";
            this.AmbilightPrecisionNud.Size = new System.Drawing.Size(62, 20);
            this.AmbilightPrecisionNud.TabIndex = 57;
            this.AmbilightPrecisionNud.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.AmbilightSmoothingTrackbar);
            this.groupBox6.Controls.Add(this.AmbilightSmoothingNud);
            this.groupBox6.Location = new System.Drawing.Point(166, 101);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(74, 272);
            this.groupBox6.TabIndex = 58;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Smoothing";
            // 
            // AmbilightSmoothingTrackbar
            // 
            this.AmbilightSmoothingTrackbar.Location = new System.Drawing.Point(15, 19);
            this.AmbilightSmoothingTrackbar.Maximum = 100;
            this.AmbilightSmoothingTrackbar.Name = "AmbilightSmoothingTrackbar";
            this.AmbilightSmoothingTrackbar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.AmbilightSmoothingTrackbar.Size = new System.Drawing.Size(45, 218);
            this.AmbilightSmoothingTrackbar.TabIndex = 56;
            this.AmbilightSmoothingTrackbar.TickFrequency = 10;
            this.AmbilightSmoothingTrackbar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.AmbilightSmoothingTrackbar.Value = 1;
            // 
            // AmbilightSmoothingNud
            // 
            this.AmbilightSmoothingNud.Location = new System.Drawing.Point(6, 243);
            this.AmbilightSmoothingNud.Name = "AmbilightSmoothingNud";
            this.AmbilightSmoothingNud.Size = new System.Drawing.Size(62, 20);
            this.AmbilightSmoothingNud.TabIndex = 57;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.AmbilightCaptureProfileComboBox);
            this.groupBox7.Controls.Add(this.AmbilightCreateCaptureProfileButton);
            this.groupBox7.Controls.Add(this.AmbilightCaptureScreenComboBox);
            this.groupBox7.Controls.Add(this.AmbilightShowCaptureAreasButton);
            this.groupBox7.Location = new System.Drawing.Point(6, 19);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(402, 76);
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
            this.AmbilightCreateCaptureProfileButton.Location = new System.Drawing.Point(257, 17);
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
            this.AmbilightShowCaptureAreasButton.Location = new System.Drawing.Point(257, 44);
            this.AmbilightShowCaptureAreasButton.Name = "AmbilightShowCaptureAreasButton";
            this.AmbilightShowCaptureAreasButton.Size = new System.Drawing.Size(139, 23);
            this.AmbilightShowCaptureAreasButton.TabIndex = 39;
            this.AmbilightShowCaptureAreasButton.Text = "Show capture areas";
            this.AmbilightShowCaptureAreasButton.UseVisualStyleBackColor = true;
            this.AmbilightShowCaptureAreasButton.Click += new System.EventHandler(this.AmbilightShowCaptureAreasButton_Click);
            // 
            // RainbowGroupBox
            // 
            this.RainbowGroupBox.Controls.Add(this.RainbowSpeedNud);
            this.RainbowGroupBox.Controls.Add(this.label8);
            this.RainbowGroupBox.Controls.Add(this.RainbowSpeedTrackBar);
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
            this.RainbowSpeedTrackBar.TickFrequency = 5;
            this.RainbowSpeedTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.RainbowSpeedTrackBar.Value = 1;
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
            this.RainbowBrightnessTrackBar.TickFrequency = 8;
            this.RainbowBrightnessTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // NullGroupBox
            // 
            this.NullGroupBox.Controls.Add(this.button3);
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
            this.TabControl.Size = new System.Drawing.Size(1598, 805);
            this.TabControl.TabIndex = 41;
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // BassTabPage
            // 
            this.BassTabPage.Controls.Add(this.visStatusLabel);
            this.BassTabPage.Controls.Add(this.ShowAnalyzerButton);
            this.BassTabPage.Controls.Add(this.AudioLevelPanel);
            this.BassTabPage.Controls.Add(this.AudioNumBandsLabel);
            this.BassTabPage.Controls.Add(this.numBandsTrackBar);
            this.BassTabPage.Controls.Add(this.StartAudioVisButton);
            this.BassTabPage.Controls.Add(this.AudioVisualizerPanel);
            this.BassTabPage.Location = new System.Drawing.Point(4, 22);
            this.BassTabPage.Name = "BassTabPage";
            this.BassTabPage.Size = new System.Drawing.Size(1590, 779);
            this.BassTabPage.TabIndex = 4;
            this.BassTabPage.Text = "Audio";
            this.BassTabPage.UseVisualStyleBackColor = true;
            // 
            // visStatusLabel
            // 
            this.visStatusLabel.AutoSize = true;
            this.visStatusLabel.Location = new System.Drawing.Point(10, 371);
            this.visStatusLabel.Name = "visStatusLabel";
            this.visStatusLabel.Size = new System.Drawing.Size(148, 13);
            this.visStatusLabel.TabIndex = 0;
            this.visStatusLabel.Text = "Press \"start\" to start visualizin\'";
            // 
            // ShowAnalyzerButton
            // 
            this.ShowAnalyzerButton.Location = new System.Drawing.Point(727, 358);
            this.ShowAnalyzerButton.Name = "ShowAnalyzerButton";
            this.ShowAnalyzerButton.Size = new System.Drawing.Size(118, 23);
            this.ShowAnalyzerButton.TabIndex = 4;
            this.ShowAnalyzerButton.Text = "Open in new window";
            this.ShowAnalyzerButton.UseVisualStyleBackColor = true;
            this.ShowAnalyzerButton.Click += new System.EventHandler(this.ShowAnalyzerButton_Click);
            // 
            // AudioLevelPanel
            // 
            this.AudioLevelPanel.Location = new System.Drawing.Point(13, 358);
            this.AudioLevelPanel.Name = "AudioLevelPanel";
            this.AudioLevelPanel.Size = new System.Drawing.Size(663, 10);
            this.AudioLevelPanel.TabIndex = 3;
            // 
            // AudioNumBandsLabel
            // 
            this.AudioNumBandsLabel.AutoSize = true;
            this.AudioNumBandsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AudioNumBandsLabel.Location = new System.Drawing.Point(861, 363);
            this.AudioNumBandsLabel.Name = "AudioNumBandsLabel";
            this.AudioNumBandsLabel.Size = new System.Drawing.Size(21, 13);
            this.AudioNumBandsLabel.TabIndex = 2;
            this.AudioNumBandsLabel.Text = "30";
            // 
            // numBandsTrackBar
            // 
            this.numBandsTrackBar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.numBandsTrackBar.Location = new System.Drawing.Point(851, 3);
            this.numBandsTrackBar.Maximum = 400;
            this.numBandsTrackBar.Minimum = 1;
            this.numBandsTrackBar.Name = "numBandsTrackBar";
            this.numBandsTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.numBandsTrackBar.Size = new System.Drawing.Size(45, 349);
            this.numBandsTrackBar.TabIndex = 0;
            this.numBandsTrackBar.TickFrequency = 50;
            this.numBandsTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.numBandsTrackBar.Value = 30;
            this.numBandsTrackBar.Scroll += new System.EventHandler(this.NumBandsTrackBar_Scroll);
            // 
            // StartAudioVisButton
            // 
            this.StartAudioVisButton.Location = new System.Drawing.Point(682, 358);
            this.StartAudioVisButton.Name = "StartAudioVisButton";
            this.StartAudioVisButton.Size = new System.Drawing.Size(39, 23);
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
            // mainFormBindingSource
            // 
            this.mainFormBindingSource.DataSource = typeof(LedController.MainForm);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 42;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            this.AmbilightUpsGroupBox.ResumeLayout(false);
            this.AmbilightUpsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightLimitUpsTrackBar)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightNumHueSlicesNumericUpDown)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightBrightnessTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightBrightnessNumericUpDown)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightPrecisionTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightPrecisionNud)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightSmoothingTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmbilightSmoothingNud)).EndInit();
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
            this.TabControl.ResumeLayout(false);
            this.BassTabPage.ResumeLayout(false);
            this.BassTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBandsTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).EndInit();
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
        private System.Windows.Forms.NumericUpDown StaticBrightnessNUD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar StaticBrightnessTrackbar;
        private System.Windows.Forms.Button staticProfileColorButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox AmbilightGroupBox;
        private System.Windows.Forms.NumericUpDown AmbilightBrightnessNumericUpDown;
        private System.Windows.Forms.TrackBar AmbilightBrightnessTrackBar;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox AmbilightCaptureProfileComboBox;
        private System.Windows.Forms.Button AmbilightCreateCaptureProfileButton;
        private System.Windows.Forms.ComboBox AmbilightCaptureScreenComboBox;
        private System.Windows.Forms.Button AmbilightShowCaptureAreasButton;
        private System.Windows.Forms.GroupBox RainbowGroupBox;
        private System.Windows.Forms.NumericUpDown RainbowSpeedNud;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar RainbowSpeedTrackBar;
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
        private System.Windows.Forms.BindingSource mainFormBindingSource;
        private System.Windows.Forms.GroupBox AmbilightUpsGroupBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar AmbilightLimitUpsTrackBar;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown AmbilightNumHueSlicesNumericUpDown;
        private System.Windows.Forms.RadioButton AmbilightMedianRadioButton;
        private System.Windows.Forms.RadioButton AmbilightAverageRadioButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TrackBar AmbilightPrecisionTrackBar;
        private System.Windows.Forms.NumericUpDown AmbilightPrecisionNud;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TrackBar AmbilightSmoothingTrackbar;
        private System.Windows.Forms.NumericUpDown AmbilightSmoothingNud;
        private System.Windows.Forms.Button ShowAnalyzerButton;
        private System.Windows.Forms.Label visStatusLabel;
        private System.Windows.Forms.Button button3;
    }
}

