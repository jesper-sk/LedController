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
            BassDriver.Enable(13);
        }

        public override void Update(ColorMatrix m)
        {
            m.Clear();

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
            m.AssignUpto(m.BottomLeft - 1, colors[3][1]);
        }

        public override void Close()
        {
            BassDriver.Disable();
        }
    }
}
