using Driver;
using System.Windows.Forms;
using System.Xml.Serialization;
using Rectangle = System.Drawing.Rectangle;

namespace LedController.LedProfiles
{
    public class AmbilightLedProfile : LedProfile
    {

        protected readonly DesktopMirror mirror = new DesktopMirror();

        [XmlIgnore]
        public Rect[] Rects { get; protected set; }

        /*
        public double Precision;
        public double Smoothing;
        public int NumHueSlices;
        public bool UseMedian;
        */

        private CColor[] prev;
        private CColor[] next;
        int count;
        int lim = 3;

        bool isChanging = false;

        public AmbilightLedProfile(string name, int index, int psindex, ColorMatrix m) : base(name, index, psindex, ProfileType.Ambilight)
        {
            Brightness = 255;
            Ups = 60;

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
            count = lim;
            next = new CColor[Rects.Length];
            for (int i = 0; i < Rects.Length; i++) next[i] = new CColor();
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
                if (count == lim)
                {
                    prev = next;
                    next = mirror.GetAvgCColorFromScreen(Rects, 0.5);
                    count = 0;
                }
                CColor[] res = new CColor[Rects.Length];
                for(int i = 0; i < Rects.Length; i++)
                {
                    res[i] = CColor.Blend(prev[i], next[i], (double)(count + 1) / lim);
                }
                m.AssignFrom(m.TopLeft, res);
                count++;
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
