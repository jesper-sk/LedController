using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LedController
{
    public class LedConfig
    {
        public int Height;                      //Height in Led-units of backlight 
        public int Width;                       //Width in Led-units of backlight
        public int Offset;                      //Where does the actual Led-strip start relative to top-left corner?
        public bool IsClockwise;                //Does the strip run clockwise or counter-clockwise (from user's perspective)?
        public int Overlap;                     //With how many Led-units do the start and the end of the strip overlap?

        public LedConfig() { }                  //For XML serialization

        public LedConfig(int height, int width, int offset = 0, int overlap = 0, bool iscw = true)
        {
            Height = height;
            Width = width;
            Offset = offset;
            Overlap = overlap;
            IsClockwise = iscw;

        }

        public List<CColor> GetRelativeColors()
        {
            throw new NotImplementedException();
        }
    }

    public class LED
    {
        public byte R;
        public byte G;
        public byte B;
        public Surface Surf;
    }

    public class Surface
    {
        public Size Size;
        public Point Location;
        public LED LED;
    }
}
