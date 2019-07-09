using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace LedController
{
    public partial class SetupLedForm : Form
    {
        Point currGBLocation = new Point(12, 12);
        Size formSize = new Size(370, 285);
        int currStep = 0;
        List<GroupBox> steps;
        int totalSteps;
        ComHandler ComHandler;
        SliceOfPixelsLedProfile step1Profile;
        LedProfile currProfile;
        bool connected;

        bool c;
        int l;
        int w;
        int h;
        int s;

        public LedMatrix Result;

        public SetupLedForm(ComHandler comHandler)
        {
            InitializeComponent();

            Size = formSize;
            ComHandler = comHandler;
            if (ComHandler.IsConnected)
            {
                connected = true;
                if (ComHandler.IsSending)
                {
                    ComHandler.Deactivate();
                }
                label6.Text = "Does this make sense?";
            }
            else
            {
                connected = false;
                setupBrightnessTrackbar.Enabled = false;
                button1.Enabled = false;
                label6.Text = "Click 'yes' to proceed.";
            }

           
            cancelButton.DialogResult = DialogResult.Cancel;
            OkButton.DialogResult = DialogResult.OK;

            //Groupbox eventhandlers
            Step1GroupBox.VisibleChanged += new EventHandler(Step1GroupBox_VisibleChanged);
            numLedsNumericUpDown.ValueChanged += new EventHandler(NumLedsNUD_ValueChanged);

            Step2GroupBox.VisibleChanged += new EventHandler(Step2GroupBox_VisibleChanged);
            CwRadioButton.CheckedChanged += new EventHandler(RadioButtons_CheckedChanged);
            CcwRadioButton.CheckedChanged += new EventHandler(RadioButtons_CheckedChanged);

            Step3GroupBox.VisibleChanged += new EventHandler(Step3GroupBox_VisibleChanged);
        }

        private void SetupLedForm_Load(object sender, EventArgs e)
        {
            foreach (GroupBox gb in Controls.OfType<GroupBox>())
            {
                gb.Visible = false;
                gb.Location = currGBLocation;
            }

            OkButton.Location = nextButton.Location;
            OkButton.Visible = false;
            OkButton.Enabled = false;

            steps = new List<GroupBox>()
            {
                Step1GroupBox,
                Step2GroupBox,
                Step3GroupBox,
                Step4GroupBox
            };
            totalSteps = steps.Count();

            steps[currStep].Visible = true;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            steps[currStep++].Visible = false;
            nextButton.Enabled = currStep != totalSteps - 1;
            prevButton.Enabled = currStep != 0;
            steps[currStep].Visible = true;
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            steps[currStep--].Visible = false;
            nextButton.Enabled = currStep != totalSteps - 1;
            prevButton.Enabled = currStep != 0;
            steps[currStep].Visible = true;
        }

        //Step 1
        private void NumLedsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (step1Profile != null)
            {
                step1Profile.Slices = new List<Slice>() { new Slice(0, (int)numLedsNumericUpDown.Value + 1) };
            }
            else
            {
            }
        }

        private void Step1GroupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (Step1GroupBox.Visible)
            {
                step1Profile = new SliceOfPixelsLedProfile("setup", new List<Slice> { new Slice(0, (int)numLedsNumericUpDown.Value + 1) }, new List<CColor> { new CColor(Color.White) }, (byte)setupBrightnessTrackbar.Value);
                currProfile = step1Profile;
                if (connected) ComHandler.SetActive(currProfile);
            }
            else
            {
                l = (int)numLedsNumericUpDown.Value;
                if (connected) ComHandler.Deactivate();
            }
        }

        //Step 2
        private void Step2GroupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (Step2GroupBox.Visible)
            {
                nextButton.Enabled = CwRadioButton.Checked || CcwRadioButton.Checked;
                MovingPixelLedProfile myProfile = new MovingPixelLedProfile("setup", "root", (int)numLedsNumericUpDown.Value, (byte)setupBrightnessTrackbar.Value);
                currProfile = myProfile;
                if (connected) ComHandler.SetActive(currProfile);
            }
            else
            {
                if (connected) ComHandler.Deactivate();
            }
        }

        private void RadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            c = CwRadioButton.Checked;
            if (!nextButton.Enabled) nextButton.Enabled = true;
        }

        //Step 3
        private void Step3GroupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (Step3GroupBox.Visible)
            {
                nextButton.Enabled = StartNud.Value <= 2 * WidthNud.Value + 2 * HeightNud.Value - 4;
            }
            else
            {
                s = (int)StartNud.Value;
                w = (int)WidthNud.Value;
                h = (int)HeightNud.Value;
            }
        }

        private void NudChanged(Object sender, EventArgs e)
        {
            nextButton.Enabled = true;
        }

        //Step 4
        private void Step4GroupBox_VisibleChanged(object sender, EventArgs e)
        {
            if (Step4GroupBox.Visible)
            {
                nextButton.Enabled = false;
                nextButton.Visible = false;
                OkButton.Visible = true;
                OkButton.Enabled = true;

                Result = new LedMatrix(w, h, s, c, l);
                currProfile = new ColoredSidesLedProfile(Result, (byte)setupBrightnessTrackbar.Value);
                if (connected) ComHandler.SetActive(currProfile);
            }
            else
            {
                nextButton.Enabled = true;
                nextButton.Visible = true;
                OkButton.Visible = false;
                OkButton.Enabled = false;
            }
        }

        

        private void SetupBrightnessTrackbar_Scroll(object sender, EventArgs e)
        {
            currProfile.Brightness = (byte)setupBrightnessTrackbar.Value;
        }

        private void SetupLedForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ComHandler.Deactivate();
        }

        private class ScreenSlices
        {

        }

        enum ScreenSide { Left, Right, Top, Bottom, TopRight, BottomRight, TopLeft, BottomLeft}
    }
}
