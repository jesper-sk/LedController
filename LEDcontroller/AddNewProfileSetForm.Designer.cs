namespace LedController
{
    partial class AddNewProfileSetForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.profileSetNameBox = new System.Windows.Forms.TextBox();
            this.newCancelButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Profileset Name:";
            // 
            // profileSetNameBox
            // 
            this.profileSetNameBox.Location = new System.Drawing.Point(15, 25);
            this.profileSetNameBox.Name = "profileSetNameBox";
            this.profileSetNameBox.Size = new System.Drawing.Size(257, 20);
            this.profileSetNameBox.TabIndex = 1;
            // 
            // CancelButton
            // 
            this.newCancelButton.Location = new System.Drawing.Point(116, 51);
            this.newCancelButton.Name = "CancelButton";
            this.newCancelButton.Size = new System.Drawing.Size(75, 23);
            this.newCancelButton.TabIndex = 2;
            this.newCancelButton.Text = "Cancel";
            this.newCancelButton.UseVisualStyleBackColor = true;
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(197, 51);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 3;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            // 
            // AddNewProfileSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 85);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.newCancelButton);
            this.Controls.Add(this.profileSetNameBox);
            this.Controls.Add(this.label1);
            this.Name = "AddNewProfileSetForm";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button newCancelButton;
        private System.Windows.Forms.Button CreateButton;
        public System.Windows.Forms.TextBox profileSetNameBox;
    }
}