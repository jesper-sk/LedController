using LedController.Bass;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace LedController
{
    public partial class SpectrumAnalyzer : Form
    {
        private readonly Size defaultFormSize = new Size(816, 436);
        private readonly Color accentColor = Color.FromArgb(92, 33, 24);
        private readonly Color backForegroundColor = Color.FromArgb(20, 20, 20);
        private bool isNumBandsVisible = false;
        private int numBands;
        private bool refreshBusy = false;
        public SpectrumAnalyzer()
        {
            InitializeComponent();
            ResetDimensions();
            numBands = numBandsTrackBar.Value;
            if (BassDriver.State != BassDriverState.Enabled)
            {
                audioDeviceLabel.Text = "Audio capture is not enabled!";
            }
            else
            {
                audioDeviceLabel.Text = BassDriver.CurrentDeviceName;
                updateTimer.Enabled = true;
            }
            UpdateTextLabelLocation();
        }

        public void ResetDimensions()
        {
            Size = defaultFormSize;
        }

        public void UpdateDimensions()
        {
            spectrumPanel.Width = ClientSize.Width - 24;
            spectrumPanel.Height = ClientSize.Height - 12 - lrPanel.Height - 12 - resetDimensionsButton.Height - 12;

            lrPanel.Width = ClientSize.Width - 24;
            lrPanel.Location = new Point(
                lrPanel.Location.X,
                spectrumPanel.Height + spectrumPanel.Location.Y + 6
                );

            numBandsPanel.Size = lrPanel.Size;
            numBandsPanel.Location = lrPanel.Location;

            numBandsTrackBar.Width = numBandsPanel.Width - 6 - numBandsNud.Width;

            numBandsNud.Location = new Point(
                numBandsPanel.Width - numBandsNud.Width,
                numBandsNud.Location.Y
                );

            showTrackBarButton.Location = new Point(
                showTrackBarButton.Location.X,
                ClientSize.Height - showTrackBarButton.Height - 12
                );

            resetDimensionsButton.Location = new Point(
                ClientSize.Width - resetDimensionsButton.Width - 12,
                showTrackBarButton.Location.Y
                );

            UpdateTextLabelLocation();
        }

        private void UpdateTextLabelLocation()
        {
            int interButtonSize = ((resetDimensionsButton.Location.X - (showTrackBarButton.Location.X + showTrackBarButton.Width)) / 2);
            audioDeviceLabel.Location = 
                new Point(
                    showTrackBarButton.Location.X + showTrackBarButton.Width + (interButtonSize - (audioDeviceLabel.Width / 2)),
                    resetDimensionsButton.Location.Y + 5
                );
            if (interButtonSize < audioDeviceLabel.Width - 64) audioDeviceLabel.Visible = false;
            else audioDeviceLabel.Visible = true;
        }

        private void ShowTrackBarButton_Click(object sender, EventArgs e)
        {
            if (isNumBandsVisible)
            {
                showTrackBarButton.Text = "Show";
                numBandsPanel.Visible = false;
                lrPanel.Visible = true;
                isNumBandsVisible = false;
            }
            else
            {
                showTrackBarButton.Text = "Hide";
                lrPanel.Visible = false;
                numBandsPanel.Visible = true;
                isNumBandsVisible = true;
            }
        }

        private void DrawAnalyzer(List<byte> bands, short l, short r)
        {
            int n = bands.Count;
            int w = (int)((spectrumPanel.Width - (n - 1)) / (double)n);
            int xs = (int)((double)(spectrumPanel.Width - (n * w + (n - 1) * 1)) / 2);
            int y = spectrumPanel.Height;
            int x = xs;
            using (Graphics e = spectrumPanel.CreateGraphics())
            {
                for (int b = 0; b < n; b++)
                {
                    double perc = (double)bands[b] / byte.MaxValue;
                    int h = (int)(perc * y);
                    using (SolidBrush brush = new SolidBrush(backForegroundColor))
                    {
                        e.FillRectangle(brush, new Rectangle(x, 0, w, y));
                    }
                    using (SolidBrush brush = new SolidBrush(accentColor))
                    {
                        e.FillRectangle(brush, new Rectangle(x, y - h, w, h));
                    }
                    x += w + 1;
                }
            }
            int lh = (lrPanel.Height - 1) / 2;
            int lys = (lrPanel.Height - ((lh * 2) + 1)) / 2;

            double percl = (double)l / short.MaxValue;
            double percr = (double)r / short.MaxValue;
            int hl = (int)(percl * lrPanel.Width);
            int hr = (int)(percr * lrPanel.Width);
            using (Graphics e = lrPanel.CreateGraphics())
            {
                using (SolidBrush brush = new SolidBrush(backForegroundColor))
                {
                    e.FillRectangle(brush, new Rectangle(0, lys, lrPanel.Width, lh));
                    e.FillRectangle(brush, new Rectangle(0, lys + 1 + lh, lrPanel.Width, lh));
                }
                using (SolidBrush brush = new SolidBrush(accentColor))
                {
                    e.FillRectangle(brush, new Rectangle(0, lys, hl, lh));
                    e.FillRectangle(brush, new Rectangle(0, lys + 1 + lh, hr, lh));
                }
            }
        }

        private void NSpectrumAnalyzer_Resize(object s, EventArgs _)
        {
            refreshBusy = true;
            UpdateDimensions();
            using (Graphics e = spectrumPanel.CreateGraphics()) e.Clear(Color.Black);
            using (Graphics e = lrPanel.CreateGraphics()) e.Clear(Color.Black);
            refreshBusy = false;
        }

        private void ChangeBands(int val)
        {
            refreshBusy = true;
            numBands = val;
            using (Graphics e = spectrumPanel.CreateGraphics()) e.Clear(Color.Black);
            using (Graphics e = lrPanel.CreateGraphics()) e.Clear(Color.Black);
            refreshBusy = false;
        }

        private void NumBandsTrackBar_Scroll(object sender, EventArgs e)
        {
            numBandsNud.Value = numBandsTrackBar.Value;
        }

        private void NumBandsNud_ValueChanged(object sender, EventArgs e)
        {
            numBandsTrackBar.Value = (int)numBandsNud.Value;
            ChangeBands((int)numBandsNud.Value);
        }

        private void ClearAnalyzer() { DrawAnalyzer(new byte[numBandsTrackBar.Value].ToList(), 0, 0); }

        private void ResetDimensionsButton_Click(object sender, EventArgs e)
        {
            ResetDimensions();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (BassDriver.State == BassDriverState.Enabled)
            {
                if (!refreshBusy) DrawAnalyzer(BassDriver.GetBands(numBands, out short l, out short r), l, r);
            }
            else
            {
                updateTimer.Enabled = false;
                ClearAnalyzer();
                audioDeviceLabel.Text = "Audio capture is not enabled!";
            }
        }
    }
}
