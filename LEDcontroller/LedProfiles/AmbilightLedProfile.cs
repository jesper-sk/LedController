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

namespace LedController.LedProfiles
{
    public class AmbilightLedProfile : LedProfile
    {

        readonly DesktopMirror mirror = new DesktopMirror();

        [XmlIgnore]
        public Rect[] Rects { get; private set; }

        bool isChanging = false;

        public AmbilightLedProfile(string name, string parentProfileSet, int index, ColorMatrix m)
        {
            Index = index;
            Parent = parentProfileSet;
            Brightness = 255;
            ProfileType = ProfileType.Ambilight;
            Name = name;
            UName = $"{parentProfileSet}:{name}";
            Ups = 30;

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

        public override void Init(ColorMatrix m)
        {
            Rects = matrix.GetCaptureRects(ScreenIndex, RatioProfile);

            if (mirror.Load())
            {
                if (mirror.Connect()) mirror.Start();
                else MessageBox.Show("Could't connect to dfMirage driver", "Driver connection error");
            }
            else MessageBox.Show("Couldn't load the dfMirage driver", "Driver loading error");
        }

        public override void Update(ColorMatrix m)
        {
            if (!isChanging)
            {
                try
                {
                    m.AssignFrom(m.TopLeft, mirror.GetAvgCColorFromScreen(Rects, false));
                }
                catch
                {
                    mirror.Stop();
                    mirror.Disconnect();
                    mirror.Unload();
                    mirror.Dispose();
                    throw;
                }
            }
            else
            {
                for(int i = 0; i < m.MasterLength; i++)
                {
                    m[i] = new CColor();
                }
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
            //Logger.Log(Rects.Length);
            isChanging = false;
        }
    }
}
