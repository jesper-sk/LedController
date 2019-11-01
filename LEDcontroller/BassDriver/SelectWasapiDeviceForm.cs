using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.BassWasapi;
using static Un4seen.BassWasapi.BassWasapi;

namespace LedController.Bass
{
    public partial class SelectWasapiDeviceForm : Form
    {
        public SelectWasapiDeviceForm()
        {
            InitializeComponent();
            OkButton.DialogResult = DialogResult.OK;
            CancelButton.DialogResult = DialogResult.Cancel;
            SetDevices();
        }

        private void SetDevices()
        {
            DevicesListView.Items.Clear();
            BASS_WASAPI_DEVICEINFO[] infos = BASS_WASAPI_GetDeviceInfos();
            int l = infos.Length;
            for (int i = 0; i < l; i++)
            {
                BASS_WASAPI_DEVICEINFO info = infos[i];
                if (info.IsInput)
                {
                    Clvi curr = new Clvi();
                    curr.absIndex = i;
                    string fullName = info.name;
                    List<string> spl = fullName.Split('(').ToList();
                    string driver = spl[spl.Count - 1];
                    driver = driver.Substring(0, driver.Length - 1);
                    spl.RemoveAt(spl.Count - 1);
                    string name = string.Concat(spl);
                    curr.Text = name;
                    curr.SubItems.Add(driver);
                    List<string> details = new List<string>();
                    if (info.IsDefault || (info.IsLoopback && infos[i-1].IsDefault)) details.Add("default");
                    if (info.IsUnplugged) details.Add("unplugged");
                    if (info.IsInitialized) details.Add("initialized");
                    if (info.IsDisabled) details.Add("disabled");
                    StringBuilder dets = new StringBuilder();
                    if (details.Count > 0)
                    {
                        dets.Append("(");
                        dets.Append(details[0]);
                        int c = details.Count;
                        for (int j = 1; j < c; j++)
                        {
                            dets.Append(", ");
                            dets.Append(details[j]);
                        }
                        dets.Append(")");
                    }
                    curr.SubItems.Add(dets.ToString());
                    curr.SubItems.Add(i.ToString());
                    if (!info.IsInput) curr.ForeColor = Color.FromArgb(128, 128, 128);
                    DevicesListView.Items.Add(curr);
                    if (info.IsInput && !info.IsLoopback) curr.Group = DevicesListView.Groups[0];
                    else curr.Group = DevicesListView.Groups[1];
                }
            }
        }

        public int GetAbsoluteSelectedIndex(out string name)
        {
            if (DevicesListView.SelectedItems.Count == 0)
            {
                name = "none";
                return -1;
            }
            var item = DevicesListView.SelectedItems[0];
            name = string.Concat(item.SubItems[0].Text, " (", item.SubItems[1].Text, ")");
            return ((Clvi)item).absIndex;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {        
            SetDevices();
        }

        private void ShowInvalidCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetDevices();
        }

        private class Clvi : ListViewItem
        {
            public int absIndex;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DevicesListView.SelectedItems.Clear();
        }
    }
}
