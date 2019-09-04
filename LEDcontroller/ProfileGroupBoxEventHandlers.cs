using System;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Drawing;
using System.Collections.Generic;
using LedController.LedProfiles;

namespace LedController
{
    partial class MainForm
    {
        private void InitializeGroupBoxEventHandlers()
        {
            /*
             * Static Color
             */
            StaticBrightnessTrackbar.VisibleChanged += new System.EventHandler(StaticBrightnessTrackbar_VisibleChanged);
            StaticBrightnessTrackbar.ValueChanged += new System.EventHandler(StaticBrightnessTrackbar_ValueChanged);

            StaticBrightnessNUD.VisibleChanged += new System.EventHandler(StaticBrightnessNUD_VisibleChanged);
            StaticBrightnessNUD.ValueChanged += new System.EventHandler(StaticBrightnessNUD_ValueChanged);

            staticProfileColorButton.VisibleChanged += new System.EventHandler(StaticProfileColorButton_VisibleChanged);
            staticProfileColorButton.Click += new System.EventHandler(StaticProfileColorButton_Click);

            staticColortHotkeyLabel.VisibleChanged += new System.EventHandler(StaticColortHotkeyLabel_VisibleChanged);
            staticColortHotkeyLabel.Click += new System.EventHandler(StaticColortHotkeyLabel_Click);

            /*
             * Rainbow Cycle
             */
            RainbowBrightnessTrackBar.VisibleChanged += new System.EventHandler(RainbowBrightnessTrackbar_VisibleChanged);
            RainbowBrightnessTrackBar.ValueChanged += new System.EventHandler(RainbowBrightnessTrackbar_ValueChanged);

            RainbowBrightnessNud.VisibleChanged += new System.EventHandler(RainbowBrightnessNud_VisibleChanged);
            RainbowBrightnessNud.ValueChanged += new System.EventHandler(RainbowBrightnessNud_ValueChanged);

            RainbowSpeedTrackBar.VisibleChanged += new System.EventHandler(RainbowSpeedTrackbar_VisibleChanged);
            RainbowSpeedTrackBar.ValueChanged += new System.EventHandler(RainbowSpeedTrackbar_ValueChanged);

            RainbowSpeedNud.VisibleChanged += new System.EventHandler(RainbowSpeedNud_VisibleChanged);
            RainbowSpeedNud.ValueChanged += new System.EventHandler(RainbowSpeedNud_ValueChanged);

            /*
             * Ambilight
             */
            AmbilightBrightnessTrackBar.VisibleChanged += new EventHandler(AmbilightBrightnessTrackBar_VisibleChanged);
            AmbilightBrightnessTrackBar.ValueChanged += new EventHandler(AmbilightBrightnessTrackBar_ValueChanged);

            AmbilightBrightnessNumericUpDown.VisibleChanged += new EventHandler(AmbilightBrightnessNumericUpDown_VisibleChanged);
            AmbilightBrightnessNumericUpDown.ValueChanged += new EventHandler(AmbilightBrightnessNumericUpDown_ValueChanged);

            AmbilightCaptureScreenComboBox.VisibleChanged += new EventHandler(AmbilightCaptureScreenComboBox_VisibleChanged);
            AmbilightCaptureScreenComboBox.SelectedIndexChanged += new EventHandler(AmbilightCaptureScreenComboBox_SelectedIndexChanged);

            AmbilightCaptureProfileComboBox.VisibleChanged += new EventHandler(AmbilightCaptureProfileComboBox_VisibleChanged);
            AmbilightCaptureProfileComboBox.SelectedIndexChanged += new EventHandler(AmbilightCaptureProfileComboBox_SelectedIndexChanged);
        }

        #region Static Color
        private void StaticBrightnessTrackbar_VisibleChanged(object sender, EventArgs e)
        {
            if (StaticBrightnessTrackbar.Visible)
            {
                StaticBrightnessTrackbar.Value = SelectedProfile.Brightness;
            }
        }

        private void StaticBrightnessTrackbar_ValueChanged(object sender, EventArgs e)
        {
            SelectedProfile.Brightness = (byte)StaticBrightnessTrackbar.Value;
            StaticBrightnessNUD.Value = StaticBrightnessTrackbar.Value;
        }

        private void StaticBrightnessNUD_VisibleChanged(object sender, EventArgs e)
        {
            if (StaticBrightnessNUD.Visible)
            {
                StaticBrightnessNUD.Text = SelectedProfile.Brightness.ToString();
            }
        }

        private void StaticBrightnessNUD_ValueChanged(object sender, EventArgs e)
        {
            SelectedProfile.Brightness = (byte)StaticBrightnessNUD.Value;
            StaticBrightnessTrackbar.Value = (int)StaticBrightnessNUD.Value;
        }

        private void StaticProfileColorButton_VisibleChanged(object sender, EventArgs e)
        {
            if (staticProfileColorButton.Visible)
            {
                staticProfileColorButton.BackColor = SelectedProfile.LedColor;
            }
        }

        private void StaticProfileColorButton_Click(object sender, EventArgs e)
        {
            Logger.Log(sender.ToString());
            ColorDialog MyDialog = new ColorDialog
            {
                AllowFullOpen = true,
                ShowHelp = true,
                Color = staticProfileColorButton.BackColor
            };
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                staticProfileColorButton.BackColor = MyDialog.Color;
                SelectedProfile.LedColor = staticProfileColorButton.BackColor;
            }
        }

        private void StaticColortHotkeyLabel_VisibleChanged(object sender, EventArgs e)
        {
            if (staticColortHotkeyLabel.Visible)
            {
                staticColortHotkeyLabel.Text = /*SelectedProfile.Hotkey?.ToString() ?? */"Not assigned";
            }
        }

        private void StaticColortHotkeyLabel_Click(object sender, EventArgs e)
        {
            AssignHotkeyForm AHF = new AssignHotkeyForm();
            if (AHF.ShowDialog() == DialogResult.OK)
            {

            }
        }

        #endregion

        #region Rainbow Cycle
        //RainbowBrightnessTrackbar
        private void RainbowBrightnessTrackbar_VisibleChanged(object sender, EventArgs ea)
        {
            if (RainbowBrightnessTrackBar.Visible)
            {
                RainbowBrightnessTrackBar.Value = SelectedProfile.Brightness;
            }
        }

        private void RainbowBrightnessTrackbar_ValueChanged(object sender, EventArgs ea)
        {
            RainbowBrightnessNud.Value = RainbowBrightnessTrackBar.Value;
            SelectedProfile.Brightness = (byte)RainbowBrightnessTrackBar.Value;
        }

        //RainbowBrightnessNud
        private void RainbowBrightnessNud_VisibleChanged(object sender, EventArgs ea)
        {
            if (RainbowBrightnessNud.Visible)
            {
                RainbowBrightnessNud.Value = SelectedProfile.Brightness;
            }
        }

        private void RainbowBrightnessNud_ValueChanged(object sender, EventArgs ea)
        {
            RainbowBrightnessTrackBar.Value = (int)RainbowBrightnessNud.Value;
            SelectedProfile.Brightness = (byte)RainbowBrightnessNud.Value;
        }

        //RainbowSpeedTrackbar
        private void RainbowSpeedTrackbar_VisibleChanged(object sender, EventArgs ea)
        {
            if (RainbowSpeedTrackBar.Visible)
            {
                RainbowSpeedTrackBar.Value = SelectedProfile.Speed;
            }
        }

        private void RainbowSpeedTrackbar_ValueChanged(object sender, EventArgs ea)
        {
            RainbowSpeedNud.Value = RainbowSpeedTrackBar.Value;
            SelectedProfile.Speed = (int)RainbowSpeedNud.Value;
        }

        //RainbowSpeedNud
        private void RainbowSpeedNud_VisibleChanged(object sender, EventArgs ea)
        {
            if (RainbowSpeedNud.Visible)
            {
                RainbowSpeedNud.Value = SelectedProfile.Speed;
            }
        }

        private void RainbowSpeedNud_ValueChanged(object sender, EventArgs ea)
        {
            RainbowSpeedTrackBar.Value = (int)RainbowSpeedNud.Value;
            SelectedProfile.Speed = RainbowSpeedTrackBar.Value;
        }

        #endregion

        #region Ambilight
        void AmbilightBrightnessTrackBar_VisibleChanged(object s, EventArgs e)
        {
            if (AmbilightBrightnessTrackBar.Visible)
            {
                AmbilightBrightnessTrackBar.Value = SelectedProfile.Brightness;
            }
        }
        void AmbilightBrightnessTrackBar_ValueChanged(object s, EventArgs e)
        {
            AmbilightBrightnessNumericUpDown.Value = AmbilightBrightnessTrackBar.Value;
            SelectedProfile.Brightness = (byte)AmbilightBrightnessTrackBar.Value;
        }

        void AmbilightBrightnessNumericUpDown_VisibleChanged(object s, EventArgs e)
        {
            if (AmbilightBrightnessNumericUpDown.Visible)
            {
                AmbilightBrightnessNumericUpDown.Value = SelectedProfile.Brightness;
            }
        }
        void AmbilightBrightnessNumericUpDown_ValueChanged(object s, EventArgs e)
        {
            AmbilightBrightnessTrackBar.Value = (int)AmbilightBrightnessNumericUpDown.Value;
            SelectedProfile.Brightness = (byte)AmbilightBrightnessNumericUpDown.Value;
        }

        void AmbilightCaptureScreenComboBox_VisibleChanged(object s, EventArgs e)
        {
            if (AmbilightCaptureScreenComboBox.Visible)
            {
                AmbilightCaptureScreenComboBox.Items.Clear();
                for(int i = 0; i < Screen.AllScreens.Length; i++)
                {
                    Screen scr = Screen.AllScreens[i];
                    string msg = (scr.Primary) ? $"Screen {i} : {scr.Bounds.Width} x{scr.Bounds.Height} (Primary)" : $"Screen {i}: {scr.Bounds.Width} x{scr.Bounds.Height}";
                    AmbilightCaptureScreenComboBox.Items.Add(msg);
                }
                if (SelectedProfile.ScreenIndex < Screen.AllScreens.Length) AmbilightCaptureScreenComboBox.SelectedIndex = SelectedProfile.ScreenIndex;
                else AmbilightCaptureScreenComboBox.SelectedIndex = 0;
            }
        }
        void AmbilightCaptureScreenComboBox_SelectedIndexChanged(object s, EventArgs e)
        {
            UpdateAmbilightCaptureProfileComboBox();       
        }

        void AmbilightSetCaptureScreenButton_Click(object s, EventArgs e)
        {
            SelectedProfile.ScreenIndex = AmbilightCaptureScreenComboBox.SelectedIndex;
        }

        void AmbilightCaptureProfileComboBox_VisibleChanged(object s, EventArgs e)
        {
            if (RatioProfiles == null)
            {
                LoadRatioProfiles(SelectedProfile.RatioProfile);
            }
            Screen curr;
            if (SelectedProfile.ScreenIndex >= Screen.AllScreens.Length) curr = Screen.AllScreens[0];
            else curr = Screen.AllScreens[SelectedProfile.ScreenIndex];
            Rectangle bounds = curr.Bounds;
            double r = Math.Round((double)bounds.Width / bounds.Height, 2);
            RatioProfile profile = new RatioProfile(r);
            if (!RatioProfiles.Contains(profile)) RatioProfiles.Add(profile);
            AmbilightCaptureProfileComboBox.Items.Clear();
            for (int i = 0; i < RatioProfiles.Count; i++)
            {
                bool native = profile == RatioProfiles[i];
                string msg = (native) ? $"{RatioProfiles[i].ToString()} (Native)" : RatioProfiles[i].ToString();
                AmbilightCaptureProfileComboBox.Items.Add(msg);
            }
            AmbilightCaptureProfileComboBox.SelectedIndex = RatioProfiles.FindIndex((RatioProfile p) => { return p == SelectedProfile.RatioProfile; });
        }
        void UpdateAmbilightCaptureProfileComboBox()
        {
            if (RatioProfiles == null)
            {
                LoadRatioProfiles(SelectedProfile.RatioProfile);
            }
            Screen curr = Screen.AllScreens[AmbilightCaptureScreenComboBox.SelectedIndex];
            Rectangle bounds = curr.Bounds;
            double r = Math.Round((double)bounds.Width / (double)bounds.Height, 2);
            RatioProfile profile = new RatioProfile(r);
            if (!RatioProfiles.Contains(profile)) RatioProfiles.Add(profile);
            AmbilightCaptureProfileComboBox.Items.Clear();
            for (int i = 0; i < RatioProfiles.Count; i++)
            {
                bool native = profile == RatioProfiles[i];
                string msg = (native) ? $"{RatioProfiles[i].ToString()} (Native)" : RatioProfiles[i].ToString();
                AmbilightCaptureProfileComboBox.Items.Add(msg);
                if (native)
                {
                    AmbilightCaptureProfileComboBox.SelectedIndex = i;
                }
            }
            if (SelectedProfile is AmbilightLedProfile prof)
            {
                prof.UpdateRects(AmbilightCaptureScreenComboBox.SelectedIndex, RatioProfiles[AmbilightCaptureProfileComboBox.SelectedIndex], LedMatrix);
            }
            else throw new NotImplementedException("Invalid LedProfile, expected type AmbilightLedProfile");
        }
        void AmbilightCaptureProfileComboBox_SelectedIndexChanged(object s, EventArgs e)
        {
            if (SelectedProfile is AmbilightLedProfile prof)
            {
                int scrInd = 0;
                if (prof.ScreenIndex < Screen.AllScreens.Length) scrInd = prof.ScreenIndex;
                prof.UpdateRects(scrInd, RatioProfiles[AmbilightCaptureProfileComboBox.SelectedIndex], LedMatrix);
            }
            else throw new NotImplementedException("Invalid LedProfile, expected type AmbilightLedProfile");
        }
        void AmbilightPrecisionTrackBar_ValueChanged(object s, EventArgs e)
        {
            if (SelectedProfile is AmbilightLedProfile prof)
            {
                prof.Precision = (double)AmbilightPrecisionTrackBar.Value / 11;
                Logger.Log($"Precision = {prof.Precision}");
            }
        }
        #endregion
        void ProfileSettingsChanged()
        {
            if (SelectedProfile == ActiveProfile)
            {
                ActivateSelectedProfile();
            }
        }
    }
}
