using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedController
{
    public partial class AddNewProfileForm : Form
    {
        public AddNewProfileForm()
        {
            InitializeComponent();
            CreateProfileButton.DialogResult = DialogResult.OK;
            CancelButton.DialogResult = DialogResult.Cancel;
            comboBox1.SelectedIndex = 0;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
