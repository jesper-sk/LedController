namespace LedController
{
    partial class AssignHotkeyForm
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
            this.keyCodeLabel = new System.Windows.Forms.Label();
            this.assignButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.KeyCodeComboBox = new System.Windows.Forms.ComboBox();
            this.shiftTB = new System.Windows.Forms.CheckBox();
            this.CtrlTB = new System.Windows.Forms.CheckBox();
            this.AltTB = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Keyboard Input:";
            // 
            // keyCodeLabel
            // 
            this.keyCodeLabel.AutoSize = true;
            this.keyCodeLabel.Location = new System.Drawing.Point(100, 46);
            this.keyCodeLabel.Name = "keyCodeLabel";
            this.keyCodeLabel.Size = new System.Drawing.Size(181, 13);
            this.keyCodeLabel.TabIndex = 1;
            this.keyCodeLabel.Text = "CTRL + ALT + SHIFT + CAPS LOCK";
            // 
            // assignButton
            // 
            this.assignButton.Location = new System.Drawing.Point(40, 79);
            this.assignButton.Name = "assignButton";
            this.assignButton.Size = new System.Drawing.Size(75, 23);
            this.assignButton.TabIndex = 2;
            this.assignButton.Text = "Assign";
            this.assignButton.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(121, 79);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 3;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(202, 79);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // KeyCodeComboBox
            // 
            this.KeyCodeComboBox.AllowDrop = true;
            this.KeyCodeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KeyCodeComboBox.FormattingEnabled = true;
            this.KeyCodeComboBox.Location = new System.Drawing.Point(156, 22);
            this.KeyCodeComboBox.Name = "KeyCodeComboBox";
            this.KeyCodeComboBox.Size = new System.Drawing.Size(121, 21);
            this.KeyCodeComboBox.TabIndex = 5;
            // 
            // shiftTB
            // 
            this.shiftTB.AutoSize = true;
            this.shiftTB.Location = new System.Drawing.Point(12, 26);
            this.shiftTB.Name = "shiftTB";
            this.shiftTB.Size = new System.Drawing.Size(47, 17);
            this.shiftTB.TabIndex = 6;
            this.shiftTB.Text = "Shift";
            this.shiftTB.UseVisualStyleBackColor = true;
            // 
            // CtrlTB
            // 
            this.CtrlTB.AutoSize = true;
            this.CtrlTB.Location = new System.Drawing.Point(65, 26);
            this.CtrlTB.Name = "CtrlTB";
            this.CtrlTB.Size = new System.Drawing.Size(41, 17);
            this.CtrlTB.TabIndex = 7;
            this.CtrlTB.Text = "Ctrl";
            this.CtrlTB.UseVisualStyleBackColor = true;
            // 
            // AltTB
            // 
            this.AltTB.AutoSize = true;
            this.AltTB.Location = new System.Drawing.Point(112, 26);
            this.AltTB.Name = "AltTB";
            this.AltTB.Size = new System.Drawing.Size(38, 17);
            this.AltTB.TabIndex = 8;
            this.AltTB.Text = "Alt";
            this.AltTB.UseVisualStyleBackColor = true;
            // 
            // AssignHotkeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 109);
            this.Controls.Add(this.AltTB);
            this.Controls.Add(this.CtrlTB);
            this.Controls.Add(this.shiftTB);
            this.Controls.Add(this.KeyCodeComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.assignButton);
            this.Controls.Add(this.keyCodeLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AssignHotkeyForm";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label keyCodeLabel;
        private System.Windows.Forms.Button assignButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox KeyCodeComboBox;
        private System.Windows.Forms.CheckBox shiftTB;
        private System.Windows.Forms.CheckBox CtrlTB;
        private System.Windows.Forms.CheckBox AltTB;
    }
}