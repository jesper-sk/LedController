using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Driver;
using System.Runtime.InteropServices;
using Rectangle = System.Drawing.Rectangle;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LedController
{
    public class AmbilightLedProfile : LedProfile
    {

        readonly DesktopMirror mirror = new DesktopMirror();

        [XmlIgnore]
        public Rectangle[] Rects { get; private set; }

        bool isChanging = false;

        public AmbilightLedProfile(string name, string parentProfileSet, int index, LedMatrix m)
        {
            matrix = m;
            Index = index;
            Parent = parentProfileSet;
            Brightness = 255;
            LedColor = new CColor(Color.White);
            ProfileType = ProfileType.Ambilight;
            Name = name;
            UName = $"{parentProfileSet}:{name}";
            NumLeds = (short)matrix.MasterLength;
            Ups = 30;
            matrix = m;

            for(int i = 0; i < Screen.AllScreens.Length; i++)
            {
                if (Screen.AllScreens[i].Primary)
                {
                    ScreenIndex = i;
                    break;
                }
            }

            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            RatioProfile = new RatioProfile(bounds.Width / (double)bounds.Height);
        }

        public AmbilightLedProfile() { }

        public override void Init()
        {
            Rects = matrix.GetCaptureRects(ScreenIndex, RatioProfile);
            mirror.Load();
            mirror.Connect();
            mirror.Start();
        }

        public override CColor[] Update()
        {
            if (!isChanging)
            {
                CColor[] rel = mirror.GetAvgCColorFromScreen(Rects);
                matrix.AssignFrom(matrix.TopLeft, rel);
                return matrix.ReturnAbsoluteColors();
            }
            else
            {
                CColor[] res = new CColor[matrix.MasterLength];
                for(int i = 0; i < res.Length; i++)
                {
                    res[i] = new CColor(Color.Black);
                }
                return res;
            }
        }

        public override void Close()
        {
            mirror.Stop();
            mirror.Disconnect();
            mirror.Unload();
            mirror.Dispose();
        }

        public void UpdateRects(int index, RatioProfile ratio)
        {
            isChanging = true;
            ScreenIndex = index;
            RatioProfile = ratio;
            Rects = matrix.GetCaptureRects(ScreenIndex, RatioProfile);
            Console.WriteLine(Rects.Length);
            isChanging = false;
        }
    }
}
