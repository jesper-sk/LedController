using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace LedController
{
    public partial class AssignHotkeyForm : Form
    {
        private bool ctrl;
        private bool alt;
        private bool shift;
        private Keys keyCode;

        public AssignHotkeyForm()
        {
            InitializeComponent();
            assignButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
            foreach (Key k in (Key[]) Enum.GetValues(typeof(Keys)))
            {
                KeyCodeComboBox.Items.Add(k);
            }
            KeyCodeComboBox.SelectedIndex = 0;
            CtrlTB.CheckedChanged += new EventHandler(Update);
            AltTB.CheckedChanged += new EventHandler(Update);
            shiftTB.CheckedChanged += new EventHandler(Update);
            KeyCodeComboBox.SelectedValueChanged += new EventHandler(Update);

        }

        private void Update(object sender, EventArgs ea)
        {
            ctrl = CtrlTB.Checked;
            alt = AltTB.Checked;
            shift = shiftTB.Checked;
            try
            {
                //keyCode = Keys.(KeyCodeComboBox.Items[KeyCodeComboBox.SelectedIndex]);
            }
            catch
            {
               
            }
            RefreshKeyCodeLabel();
        }

        void RefreshKeyCodeLabel()
        {
            keyCodeLabel.Text =
                (ctrl ? "Ctrl" : "") +
                ((ctrl && (alt || shift)) ? " + " : "") +
                (alt ? "Alt" : "") +
                ((alt && shift) ? " + " : "") +
                (shift ? "Shift" : "") +
                ((ctrl || alt || shift) ? " + " : "") +
                keyCode.ToString() ?? "null";
        }
    }
}
