namespace LedController
{
    partial class SetupLedForm
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
            this.Step1GroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numLedsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.prevButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.Step2GroupBox = new System.Windows.Forms.GroupBox();
            this.CwRadioButton = new System.Windows.Forms.RadioButton();
            this.CcwRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.setupBrightnessTrackbar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.Step3GroupBox = new System.Windows.Forms.GroupBox();
            this.Step3Message = new System.Windows.Forms.Label();
            this.StartNud = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.HeightNud = new System.Windows.Forms.NumericUpDown();
            this.WidthNud = new System.Windows.Forms.NumericUpDown();
            this.Step4GroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.Step1GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLedsNumericUpDown)).BeginInit();
            this.Step2GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.setupBrightnessTrackbar)).BeginInit();
            this.Step3GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StartNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNud)).BeginInit();
            this.Step4GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Step1GroupBox
            // 
            this.Step1GroupBox.Controls.Add(this.button1);
            this.Step1GroupBox.Controls.Add(this.label4);
            this.Step1GroupBox.Controls.Add(this.numLedsNumericUpDown);
            this.Step1GroupBox.Controls.Add(this.label2);
            this.Step1GroupBox.Location = new System.Drawing.Point(15, 12);
            this.Step1GroupBox.Name = "Step1GroupBox";
            this.Step1GroupBox.Size = new System.Drawing.Size(327, 148);
            this.Step1GroupBox.TabIndex = 1;
            this.Step1GroupBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(228, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "How many LED units do you have?";
            // 
            // numLedsNumericUpDown
            // 
            this.numLedsNumericUpDown.Location = new System.Drawing.Point(192, 70);
            this.numLedsNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numLedsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLedsNumericUpDown.Name = "numLedsNumericUpDown";
            this.numLedsNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.numLedsNumericUpDown.TabIndex = 1;
            this.numLedsNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 30);
            this.label2.MaximumSize = new System.Drawing.Size(317, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(304, 52);
            this.label2.TabIndex = 0;
            this.label2.Text = "This setup-wizard is designed for LED-strips which are installed linearly around " +
    "one screen, like a normal Ambilight setup. \r\n\r\n\r\n";
            // 
            // prevButton
            // 
            this.prevButton.Enabled = false;
            this.prevButton.Location = new System.Drawing.Point(183, 211);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(75, 23);
            this.prevButton.TabIndex = 1;
            this.prevButton.Text = "<<Previous";
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 211);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(264, 211);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next>>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // Step2GroupBox
            // 
            this.Step2GroupBox.Controls.Add(this.CwRadioButton);
            this.Step2GroupBox.Controls.Add(this.CcwRadioButton);
            this.Step2GroupBox.Controls.Add(this.label3);
            this.Step2GroupBox.Location = new System.Drawing.Point(348, 12);
            this.Step2GroupBox.Name = "Step2GroupBox";
            this.Step2GroupBox.Size = new System.Drawing.Size(327, 148);
            this.Step2GroupBox.TabIndex = 4;
            this.Step2GroupBox.TabStop = false;
            // 
            // CwRadioButton
            // 
            this.CwRadioButton.AutoSize = true;
            this.CwRadioButton.Location = new System.Drawing.Point(40, 63);
            this.CwRadioButton.MinimumSize = new System.Drawing.Size(50, 30);
            this.CwRadioButton.Name = "CwRadioButton";
            this.CwRadioButton.Size = new System.Drawing.Size(73, 30);
            this.CwRadioButton.TabIndex = 7;
            this.CwRadioButton.Text = "Clockwise";
            this.CwRadioButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CwRadioButton.UseVisualStyleBackColor = true;
            // 
            // CcwRadioButton
            // 
            this.CcwRadioButton.AutoSize = true;
            this.CcwRadioButton.Location = new System.Drawing.Point(119, 63);
            this.CcwRadioButton.MinimumSize = new System.Drawing.Size(50, 30);
            this.CcwRadioButton.Name = "CcwRadioButton";
            this.CcwRadioButton.Size = new System.Drawing.Size(109, 30);
            this.CcwRadioButton.TabIndex = 6;
            this.CcwRadioButton.Text = "Counterclockwise";
            this.CcwRadioButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 45);
            this.label3.MaximumSize = new System.Drawing.Size(317, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(251, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Please indicate which way round the lit LED moves.";
            // 
            // setupBrightnessTrackbar
            // 
            this.setupBrightnessTrackbar.Location = new System.Drawing.Point(12, 179);
            this.setupBrightnessTrackbar.Maximum = 255;
            this.setupBrightnessTrackbar.Minimum = 1;
            this.setupBrightnessTrackbar.Name = "setupBrightnessTrackbar";
            this.setupBrightnessTrackbar.Size = new System.Drawing.Size(327, 45);
            this.setupBrightnessTrackbar.TabIndex = 9;
            this.setupBrightnessTrackbar.Value = 64;
            this.setupBrightnessTrackbar.Scroll += new System.EventHandler(this.SetupBrightnessTrackbar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Brightness";
            // 
            // Step3GroupBox
            // 
            this.Step3GroupBox.Controls.Add(this.Step3Message);
            this.Step3GroupBox.Controls.Add(this.StartNud);
            this.Step3GroupBox.Controls.Add(this.label8);
            this.Step3GroupBox.Controls.Add(this.label7);
            this.Step3GroupBox.Controls.Add(this.label5);
            this.Step3GroupBox.Controls.Add(this.HeightNud);
            this.Step3GroupBox.Controls.Add(this.WidthNud);
            this.Step3GroupBox.Location = new System.Drawing.Point(681, 12);
            this.Step3GroupBox.Name = "Step3GroupBox";
            this.Step3GroupBox.Size = new System.Drawing.Size(327, 148);
            this.Step3GroupBox.TabIndex = 21;
            this.Step3GroupBox.TabStop = false;
            this.Step3GroupBox.VisibleChanged += new System.EventHandler(this.Step3GroupBox_VisibleChanged);
            // 
            // Step3Message
            // 
            this.Step3Message.AutoSize = true;
            this.Step3Message.Location = new System.Drawing.Point(54, 126);
            this.Step3Message.Name = "Step3Message";
            this.Step3Message.Size = new System.Drawing.Size(0, 13);
            this.Step3Message.TabIndex = 6;
            // 
            // StartNud
            // 
            this.StartNud.Location = new System.Drawing.Point(153, 90);
            this.StartNud.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.StartNud.Name = "StartNud";
            this.StartNud.Size = new System.Drawing.Size(120, 20);
            this.StartNud.TabIndex = 5;
            this.StartNud.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StartNud.ValueChanged += new System.EventHandler(this.NudChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(54, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Starting point:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Height (LED units):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Width (LED units):";
            // 
            // HeightNud
            // 
            this.HeightNud.Location = new System.Drawing.Point(153, 64);
            this.HeightNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HeightNud.Name = "HeightNud";
            this.HeightNud.Size = new System.Drawing.Size(120, 20);
            this.HeightNud.TabIndex = 1;
            this.HeightNud.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HeightNud.ValueChanged += new System.EventHandler(this.NudChanged);
            // 
            // WidthNud
            // 
            this.WidthNud.Location = new System.Drawing.Point(153, 38);
            this.WidthNud.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.WidthNud.Name = "WidthNud";
            this.WidthNud.Size = new System.Drawing.Size(120, 20);
            this.WidthNud.TabIndex = 0;
            this.WidthNud.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.WidthNud.ValueChanged += new System.EventHandler(this.NudChanged);
            // 
            // Step4GroupBox
            // 
            this.Step4GroupBox.Controls.Add(this.label6);
            this.Step4GroupBox.Location = new System.Drawing.Point(682, 163);
            this.Step4GroupBox.Name = "Step4GroupBox";
            this.Step4GroupBox.Size = new System.Drawing.Size(327, 148);
            this.Step4GroupBox.TabIndex = 22;
            this.Step4GroupBox.TabStop = false;
            this.Step4GroupBox.VisibleChanged += new System.EventHandler(this.Step4GroupBox_VisibleChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(110, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Does this make sense?";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(264, 240);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Yes";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // SetupLedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1488, 738);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.Step4GroupBox);
            this.Controls.Add(this.Step3GroupBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Step2GroupBox);
            this.Controls.Add(this.prevButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.Step1GroupBox);
            this.Controls.Add(this.setupBrightnessTrackbar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SetupLedForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup LED strip";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetupLedForm_FormClosing);
            this.Load += new System.EventHandler(this.SetupLedForm_Load);
            this.Step1GroupBox.ResumeLayout(false);
            this.Step1GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLedsNumericUpDown)).EndInit();
            this.Step2GroupBox.ResumeLayout(false);
            this.Step2GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.setupBrightnessTrackbar)).EndInit();
            this.Step3GroupBox.ResumeLayout(false);
            this.Step3GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StartNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightNud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthNud)).EndInit();
            this.Step4GroupBox.ResumeLayout(false);
            this.Step4GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox Step1GroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.GroupBox Step2GroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton CwRadioButton;
        private System.Windows.Forms.RadioButton CcwRadioButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numLedsNumericUpDown;
        private System.Windows.Forms.TrackBar setupBrightnessTrackbar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox Step3GroupBox;
        private System.Windows.Forms.GroupBox Step4GroupBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown StartNud;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown HeightNud;
        private System.Windows.Forms.NumericUpDown WidthNud;
        private System.Windows.Forms.Label Step3Message;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button button1;
    }
}