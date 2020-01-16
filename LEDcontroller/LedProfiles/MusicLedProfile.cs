using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LedController.Bass;

namespace LedController.LedProfiles
{
    public class MusicLedProfile : LedProfile
    {
        public string DeviceName;
        public int DeviceIndex;

        private const double maxRos = 0.4;
        private readonly double[] maxRates = { 0.8, 0, 0, 0, 0, 0 };
        private double downWards = 0.02;
        private double rateOfChange;
        private byte[] prev;

        RainbowLedProfile prof;
        public MusicLedProfile() {; }
        public MusicLedProfile(string name, int index, int psindex, ColorMatrix m) : base(name, index, psindex, ProfileType.Music)
        {
            Brightness = 64;
            Ups = 30;
            DeviceName = "none";
            DeviceIndex = -1;
        }

        public override void Init(ColorMatrix m)
        {
            BassDriver.Enable(11);
            prof = new RainbowLedProfile();
            Brightness = 64;
            prof.Speed = 60;
            rateOfChange = 0;
            prev = new byte[6];
            prof.Init(m);
        }

        public override void Update(ColorMatrix m)
        {
            if (m is null) throw new ArgumentNullException(nameof(m));
            m.Clear();
            /*
            var bands = BassDriver.GetBands(4, out short l, out short r);
            int w = m.Width / 2;
            int h = m.Height / 2;

            CColor[][][] colors = new CColor[4][][];

            for (int i = 0; i < 4; i++)
            {
                double perc = (double)bands[i] / 255;
                int[] res = new int[2];
                res[0] = (int)Math.Round(h * perc);
                res[1] = (int)Math.Round(w * perc);
                colors[i] = new CColor[2][];
                for(int j = 0; j < 2; j++)
                {
                    colors[i][j] = new CColor[res[j]];
                    for (int k = 0; k < res[j]; k++) colors[i][j][k] = CColor.FromRgb(255, 0, 0);
                }

            }

            m.AssignFrom(m.TopLeft - 1, colors[0][1]);
            m.AssignUpto(m.TopLeft - 1, colors[0][0]);

            m.AssignFrom(m.TopRight - 1, colors[1][0]);
            m.AssignUpto(m.TopRight - 1, colors[1][1]);

            m.AssignFrom(m.BottomRight - 1, colors[2][1]);
            m.AssignUpto(m.BottomRight - 1, colors[2][0]);

            m.AssignFrom(m.BottomLeft - 1, colors[3][0]);
            m.AssignUpto(m.BottomLeft - 1, colors[3][1]);*/
            var bands = BassDriver.GetBands(6, out short l, out short r);
            rateOfChange = 0;
            for (int i = 0; i < bands.Count; i++)
            {
                double prc = (bands[i] - prev[i]) / 255.0;
                prc = prc < 0 ? 0 : Math.Pow(prc, 1);
                double cng = maxRates[i] * prc;
                rateOfChange += cng;
                prev[i] = bands[i];
            }
            rateOfChange -= downWards;
            rateOfChange = rateOfChange < (maxRos * -1) ? maxRos * -1 : rateOfChange;
            rateOfChange = rateOfChange > maxRos ? maxRos : rateOfChange;

            int brightness = (int)(rateOfChange * 255) + Brightness;

            brightness = Math.Max(Math.Min(brightness, 255), 0);

            Brightness = (byte)brightness;
            prof.Update(m);
        }

        public override void Close()
        {
            BassDriver.Disable();
        }
    }
}
