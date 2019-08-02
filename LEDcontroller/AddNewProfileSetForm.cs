using System.Windows.Forms;

namespace LedController
{
    public partial class AddNewProfileSetForm : Form
    {
        public AddNewProfileSetForm()
        {
            InitializeComponent();
            CreateButton.DialogResult = DialogResult.OK;
            newCancelButton.DialogResult = DialogResult.Cancel;
        }
    }
}
