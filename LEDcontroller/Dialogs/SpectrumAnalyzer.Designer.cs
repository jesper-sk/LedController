namespace LedController
{
    partial class SpectrumAnalyzer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpectrumAnalyzer));
            this.spectrumPanel = new System.Windows.Forms.Panel();
            this.lrPanel = new System.Windows.Forms.Panel();
            this.numBandsNud = new System.Windows.Forms.NumericUpDown();
            this.numBandsTrackBar = new System.Windows.Forms.TrackBar();
            this.showTrackBarButton = new System.Windows.Forms.Button();
            this.resetDimensionsButton = new System.Windows.Forms.Button();
            this.audioDeviceLabel = new System.Windows.Forms.Label();
            this.numBandsPanel = new System.Windows.Forms.Panel();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numBandsNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBandsTrackBar)).BeginInit();
            this.numBandsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // spectrumPanel
            // 
            this.spectrumPanel.BackColor = System.Drawing.Color.Black;
            this.spectrumPanel.Location = new System.Drawing.Point(12, 12);
            this.spectrumPanel.Name = "spectrumPanel";
            this.spectrumPanel.Size = new System.Drawing.Size(776, 292);
            this.spectrumPanel.TabIndex = 0;
            // 
            // lrPanel
            // 
            this.lrPanel.BackColor = System.Drawing.Color.Black;
            this.lrPanel.Location = new System.Drawing.Point(12, 310);
            this.lrPanel.Name = "lrPanel";
            this.lrPanel.Size = new System.Drawing.Size(776, 45);
            this.lrPanel.TabIndex = 0;
            // 
            // numBandsNud
            // 
            this.numBandsNud.BackColor = System.Drawing.Color.Black;
            this.numBandsNud.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numBandsNud.ForeColor = System.Drawing.Color.White;
            this.numBandsNud.Location = new System.Drawing.Point(727, 12);
            this.numBandsNud.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.numBandsNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBandsNud.Name = "numBandsNud";
            this.numBandsNud.Size = new System.Drawing.Size(49, 20);
            this.numBandsNud.TabIndex = 1;
            this.numBandsNud.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numBandsNud.ValueChanged += new System.EventHandler(this.NumBandsNud_ValueChanged);
            // 
            // numBandsTrackBar
            // 
            this.numBandsTrackBar.Location = new System.Drawing.Point(0, 0);
            this.numBandsTrackBar.Maximum = 1023;
            this.numBandsTrackBar.Minimum = 1;
            this.numBandsTrackBar.Name = "numBandsTrackBar";
            this.numBandsTrackBar.Size = new System.Drawing.Size(713, 45);
            this.numBandsTrackBar.TabIndex = 0;
            this.numBandsTrackBar.TickFrequency = 64;
            this.numBandsTrackBar.Value = 30;
            this.numBandsTrackBar.Scroll += new System.EventHandler(this.NumBandsTrackBar_Scroll);
            // 
            // showTrackBarButton
            // 
            this.showTrackBarButton.BackColor = System.Drawing.Color.Black;
            this.showTrackBarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showTrackBarButton.ForeColor = System.Drawing.Color.White;
            this.showTrackBarButton.Location = new System.Drawing.Point(12, 451);
            this.showTrackBarButton.Name = "showTrackBarButton";
            this.showTrackBarButton.Size = new System.Drawing.Size(49, 23);
            this.showTrackBarButton.TabIndex = 1;
            this.showTrackBarButton.Text = "Show";
            this.showTrackBarButton.UseVisualStyleBackColor = false;
            this.showTrackBarButton.Click += new System.EventHandler(this.ShowTrackBarButton_Click);
            // 
            // resetDimensionsButton
            // 
            this.resetDimensionsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetDimensionsButton.ForeColor = System.Drawing.Color.White;
            this.resetDimensionsButton.Location = new System.Drawing.Point(739, 451);
            this.resetDimensionsButton.Name = "resetDimensionsButton";
            this.resetDimensionsButton.Size = new System.Drawing.Size(49, 23);
            this.resetDimensionsButton.TabIndex = 2;
            this.resetDimensionsButton.Text = "Reset";
            this.resetDimensionsButton.UseVisualStyleBackColor = true;
            this.resetDimensionsButton.Click += new System.EventHandler(this.ResetDimensionsButton_Click);
            // 
            // audioDeviceLabel
            // 
            this.audioDeviceLabel.AutoSize = true;
            this.audioDeviceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.audioDeviceLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.audioDeviceLabel.Location = new System.Drawing.Point(67, 456);
            this.audioDeviceLabel.Name = "audioDeviceLabel";
            this.audioDeviceLabel.Size = new System.Drawing.Size(41, 13);
            this.audioDeviceLabel.TabIndex = 3;
            this.audioDeviceLabel.Text = "label1";
            // 
            // numBandsPanel
            // 
            this.numBandsPanel.BackColor = System.Drawing.Color.Black;
            this.numBandsPanel.Controls.Add(this.numBandsTrackBar);
            this.numBandsPanel.Controls.Add(this.numBandsNud);
            this.numBandsPanel.Location = new System.Drawing.Point(12, 361);
            this.numBandsPanel.Name = "numBandsPanel";
            this.numBandsPanel.Size = new System.Drawing.Size(776, 45);
            this.numBandsPanel.TabIndex = 4;
            this.numBandsPanel.Visible = false;
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 16;
            this.updateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // nSpectrumAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(952, 637);
            this.Controls.Add(this.numBandsPanel);
            this.Controls.Add(this.audioDeviceLabel);
            this.Controls.Add(this.resetDimensionsButton);
            this.Controls.Add(this.showTrackBarButton);
            this.Controls.Add(this.lrPanel);
            this.Controls.Add(this.spectrumPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(348, 276);
            this.Name = "nSpectrumAnalyzer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Spectrum Analyzer";
            this.Resize += new System.EventHandler(this.NSpectrumAnalyzer_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.numBandsNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBandsTrackBar)).EndInit();
            this.numBandsPanel.ResumeLayout(false);
            this.numBandsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel spectrumPanel;
        private System.Windows.Forms.Panel lrPanel;
        private System.Windows.Forms.NumericUpDown numBandsNud;
        private System.Windows.Forms.TrackBar numBandsTrackBar;
        private System.Windows.Forms.Button showTrackBarButton;
        private System.Windows.Forms.Button resetDimensionsButton;
        private System.Windows.Forms.Label audioDeviceLabel;
        private System.Windows.Forms.Panel numBandsPanel;
        private System.Windows.Forms.Timer updateTimer;
    }
}