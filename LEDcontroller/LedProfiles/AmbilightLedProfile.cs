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

        protected readonly DesktopMirror mirror = new DesktopMirror();

        [XmlIgnore]
        public Rect[] Rects { get; protected set; }
        [XmlIgnore]
        public double Precision = 1;

        bool isChanging = false;

        public AmbilightLedProfile(string name, int index, int psindex, ColorMatrix m) : base(name, index, psindex, ProfileType.Ambilight)
        {
            Brightness = 255;
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
            Rects = m.GetCaptureRects(ScreenIndex, RatioProfile);

            if (mirror.Load())
            {
                if (mirror.Connect()) mirror.Start();
                else MessageBox.Show("Could't connect to dfMirage driver", "Driver connection error");
            }
            else MessageBox.Show("Couldn't load the dfMirage driver", "Driver loading error");

            Rect currRect = Rects[0];
            Logger.Log($"\nFor {currRect.ToString()}, precision {Precision}:");

            int numX = (int)(currRect.Width * Precision);
            int numY = (int)(currRect.Height * Precision);

            int diffX = currRect.Width - numX;
            int diffY = currRect.Height - numY;

            int gapX = diffX / (numX - 1);
            int gapY = diffY / (numY - 1);

            int wid = numX + (numX - 1) * gapX;
            int hei = numY + (numY - 1) * gapY;

            int startX = (currRect.Width - wid) / 2;
            int startY = (currRect.Height - hei) / 2;

            int finX = startX + wid;
            int finY = startY + hei;

            int deltaX = gapX + 1;
            int deltaY = gapY + 1;

            int tot = 0;
            for (int y = startY; y < finY; y += deltaY)
            {
                for (int x = startX; x < finX; x += deltaX)
                {
                    tot++;
                }
            }

            Logger.Log($"\nEvaluating {numX} in x and {numY} in y direction\nwith gaps {gapX}(x) and {gapY}(y)\ntotal width {wid} and height {hei}\nstarting ({startX},{startY})\nfinishing ({finX},{finY})\ntotal evals: {tot}x3\n");      
        }

        public override void Update(ColorMatrix m)
        {
            if (!isChanging)
            {
                /*try
                {*/
                    m.AssignFrom(m.TopLeft, mirror.GetAvgCColorFromScreen(Rects, false, 1));
                /*}
                catch
                {
                    mirror.Stop();
                    mirror.Disconnect();
                    mirror.Unload();
                    mirror.Dispose();
                    throw;
                }*/
            }
            else for (int i = 0; i < m.MasterLength; i++){ m[i] = new CColor(); }
        }

        public override void Close()
        {
            mirror.Stop();
            mirror.Disconnect();
            mirror.Unload();
            mirror.Dispose();
        }

        public void UpdateRects(int index, RatioProfile ratio, ColorMatrix matrix)
        {
            isChanging = true;
            ScreenIndex = index;
            RatioProfile = ratio;
            Rects = matrix.GetCaptureRects(ScreenIndex, RatioProfile);
            //Logger.Log(Rects.Length);
            isChanging = false;
        }
    }

    public class TestAmbilightLedProfile : AmbilightLedProfile
    {
        public TestAmbilightLedProfile()
        {
            Brightness = 255;
            Ups = 30;

            for (int i = 0; i < Screen.AllScreens.Length; i++)
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

        public override void Init(ColorMatrix m)
        {
            Rects = new Rect[]{ new Rect(0, 0, 10, 10)};

            if (mirror.Load())
            {
                if (mirror.Connect()) mirror.Start();
                else MessageBox.Show("Could't connect to dfMirage driver", "Driver connection error");
            }
            else MessageBox.Show("Couldn't load the dfMirage driver", "Driver loading error");
        }
    }
}
