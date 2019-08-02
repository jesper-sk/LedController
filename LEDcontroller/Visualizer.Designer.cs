namespace LedController
{
    partial class Visualizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visualizer));
            this.statLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // statLabel
            // 
            this.statLabel.AutoSize = true;
            this.statLabel.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.statLabel.Location = new System.Drawing.Point(23, 20);
            this.statLabel.Name = "statLabel";
            this.statLabel.Size = new System.Drawing.Size(28, 13);
            this.statLabel.TabIndex = 0;
            this.statLabel.Text = "Text\r\n";
            // 
            // Visualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Visualizer";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Visualizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Visualizer_FormClosing);
            this.Load += new System.EventHandler(this.Visualizer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label statLabel;
    }
}