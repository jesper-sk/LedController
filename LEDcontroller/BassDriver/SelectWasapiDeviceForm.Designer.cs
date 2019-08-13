namespace LedController.Bass
{
    partial class SelectWasapiDeviceForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Input Devices", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Output devices (LoopBack)", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Speakers",
            "Realtek High Defenition Audio",
            "Output device ( loopback)"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Microphone",
            "NVidia Digitial Audio Out",
            "Input device"}, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectWasapiDeviceForm));
            this.CancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.DevicesListView = new System.Windows.Forms.ListView();
            this.deviceHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.driverHandler = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.detailHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(530, 470);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 1;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(449, 470);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(12, 470);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 3;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // DevicesListView
            // 
            this.DevicesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.deviceHeader,
            this.driverHandler,
            this.detailHeader});
            this.DevicesListView.FullRowSelect = true;
            this.DevicesListView.GridLines = true;
            listViewGroup1.Header = "Input Devices";
            listViewGroup1.Name = "OutputListViewGroup";
            listViewGroup2.Header = "Output devices (LoopBack)";
            listViewGroup2.Name = "OutputListViewGroup";
            this.DevicesListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this.DevicesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.DevicesListView.HideSelection = false;
            listViewItem1.Group = listViewGroup2;
            listViewItem1.UseItemStyleForSubItems = false;
            listViewItem2.Group = listViewGroup1;
            this.DevicesListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.DevicesListView.Location = new System.Drawing.Point(12, 12);
            this.DevicesListView.MultiSelect = false;
            this.DevicesListView.Name = "DevicesListView";
            this.DevicesListView.Size = new System.Drawing.Size(593, 452);
            this.DevicesListView.TabIndex = 4;
            this.DevicesListView.UseCompatibleStateImageBehavior = false;
            this.DevicesListView.View = System.Windows.Forms.View.Details;
            // 
            // deviceHeader
            // 
            this.deviceHeader.Text = "Device";
            this.deviceHeader.Width = 115;
            // 
            // driverHandler
            // 
            this.driverHandler.Text = "Driver";
            this.driverHandler.Width = 260;
            // 
            // detailHeader
            // 
            this.detailHeader.Text = "Details";
            this.detailHeader.Width = 214;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 470);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Deselect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // SelectWasapiDeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 499);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DevicesListView);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.CancelButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectWasapiDeviceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Audio Device";
            this.ResumeLayout(false);

        }

        #endregion
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ColumnHeader deviceHeader;
        private System.Windows.Forms.ColumnHeader driverHandler;
        private System.Windows.Forms.ColumnHeader detailHeader;
        private System.Windows.Forms.ListView DevicesListView;
        private System.Windows.Forms.Button button1;
    }
}