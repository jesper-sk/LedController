#define NEW

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LedController
{
    [XmlInclude(typeof(StaticLedProfile))]
    [XmlInclude(typeof(AmbilightLedProfile))]
    [XmlInclude(typeof(RainbowLedProfile))]
    public abstract class LedProfile : IEquatable<LedProfile>
    {
        /*
         * LED profile classes should inferit this abstract class. They should further include:
         *  - Constructor that takes the profile name as a string, and a LEDconfig class
         *  - Method Update() that returns an array of Colors (leds)
         *  - Assignments of all relevant properties
         *  - Add the XmlInclude(typeof(ConcreteProfile)) attribute to the abstract profile class
         */
        public string Name { get; set; }
        public string UName { get; set; }
        public ProfileType ProfileType { get; set; }
        public CColor LedColor { get; set; }
        public byte Brightness { get; set; }
        public short NumLeds { get; set; }
        public int Ups { get; set; } 
        public int Speed { get; set; }
        public int Index { get; set; }
        public string Parent { get; set; }
        public int ScreenIndex { get; set; }
        public RatioProfile RatioProfile { get; set; }

        protected LedMatrix matrix;

        protected Color[] leds;

        public bool Equals(LedProfile other)
        {
            return other.UName == UName;
        }
        public override string ToString()
        {
            return Name;
        }

        public void SetMatrix(LedMatrix m)
        {
            matrix = m;
        }

        public abstract void Init();
        public abstract CColor[] Update();
        public abstract void Close();
        protected string Bytetostring(byte[] inp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            sb.Append(inp[0]);
            for (int i = 1; i < inp.Length; i++)
            {
                byte b = inp[i];
                sb.Append(", ");
                sb.Append(b);
            }
            sb.Append(']');
            return sb.ToString();
        }
    }

    public class StaticLedProfile : LedProfile
    {
        public StaticLedProfile() { }

        public StaticLedProfile(string name, string parentProfileSet, int index, LedMatrix m)
        {
            matrix = m;
            Index = index;
            Parent = parentProfileSet;
            Brightness = 64;
            LedColor = new CColor(Color.White);
            ProfileType = ProfileType.Static;
            Name = name;
            UName = $"{parentProfileSet}:{name}";
            NumLeds = (short)matrix.MasterLength;
            Ups = 30;
            matrix = m;
        }

        public override void Init()
        {

        }

        public override CColor[] Update()
        {
            CColor[] res = new CColor[NumLeds];
            for (int i = 0; i < NumLeds; i++)
            {
                res[i] = LedColor;
            }
            return res;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Close()
        {
            
        }
    }

    public class RainbowLedProfile : LedProfile
    {
        double deltaHue, deltaStartHue, currHue, startHue;

        public RainbowLedProfile() { }

        public RainbowLedProfile(string name, string parentProfileSet, int index, LedMatrix m)
        {
            matrix = m;
            Index = index;
            Parent = parentProfileSet;
            Brightness = 64;
            ProfileType = ProfileType.Rainbow;
            Name = name;
            UName = $"{parentProfileSet}:{name}";
            NumLeds = (short)matrix.MasterLength;
            Ups = 60;
            matrix = m;
        }

        public override void Close()
        {
            
        }

        public override void Init()
        {
            deltaHue = Math.Floor(360.0 / NumLeds);
            startHue = 0;
        }
        public override CColor[] Update()
        {
            CColor[] res = new CColor[NumLeds];
            deltaStartHue = ((double)Map(Speed, 0, 100, 0, (int)Math.Floor(deltaHue * 10))) / 10;
            currHue = startHue;
            for (int i = 0; i < NumLeds; i++)
            {
                res[i] = new CColor(currHue, 255, 255);
                currHue = (currHue + deltaHue) % 360;
            }
            startHue = (startHue + deltaStartHue) % 360;
            return res;
        }
        int Map(int x, int inLow, int inHigh, int outLow, int outHigh)
        {
            return (x - inLow) * (outHigh - outLow) / (inHigh - inLow) + outLow;
        }
    }

    #region SetupLedFrom profiles
    public class MovingPixelLedProfile : LedProfile
    {
        int i;
        int updates;
        int fps = 10;

        public MovingPixelLedProfile() { }

        public MovingPixelLedProfile(string name, string parentProfileSet, int numLeds, byte brightness)
        {
            Name = name;
            UName = $"{parentProfileSet}:{name}";
            Brightness = brightness;
            LedColor = Color.White;
            Ups = 10;
            NumLeds = (short)numLeds;
            ProfileType = ProfileType.Other;

            i = 0;
            updates = 0;
        }

        public override void Close()
        {
            
        }

        public override void Init() { }

        public override CColor[] Update()
        {
            updates++;
            CColor[] res = new CColor[NumLeds];
            for (int j = 0; j < NumLeds; j++)
            {
                res[j] = new CColor(); //defaults to black
            }
            res[i] = new CColor(Color.White);
            if (updates >= 1 / fps)
            {
                i++;
                i %= NumLeds;
                updates = 0;
            }
            return res;
        }
    }

    public class SliceOfPixelsLedProfile : LedProfile
    {
        public List<Slice> Slices
        {
            get
            {
                return slices;
            }
            set
            {
                if (value.Count != sliceColors.Count)
                {
                    throw new NotImplementedException("Slices and SliceColors should be the same length!");
                }
                slices = value;
                NumLeds = Math.Max((byte)slices.Max(t => t.To), NumLeds);
            }
        }
        private List<Slice> slices;

        public List<CColor> SliceColors
        {
            get
            {
                return sliceColors;
            }
            set
            {
                if (value.Count != sliceColors.Count)
                {
                    throw new NotImplementedException("Slices and SliceColors should be the same length!");
                }
            }
        }
        private List<CColor> sliceColors;

        public SliceOfPixelsLedProfile() { }

        public SliceOfPixelsLedProfile(string name, List<Slice> s, List<CColor> sC, byte brightness = 255)
        {
            Name = name;
            Brightness = brightness;
            Ups = 30;
            if (s.Count != sC.Count) { throw new InvalidOperationException("Slices and SliceColors should be the same length!"); }
            slices = s;
            sliceColors = sC;
            NumLeds = (short)slices.Max(t => t.To);
        }

        public void ChangeSlicesAndCColors(List<Slice> s, List<CColor> sC)
        {
            if (s.Count != sC.Count) { throw new InvalidOperationException("Slices and SliceColors should be the same length!"); }
            slices = s;
            sliceColors = sC;
            NumLeds = Math.Max((byte)slices.Max(t => t.To), NumLeds);
        }

        public override void Init()
        {
            //
        }

        public override CColor[] Update()
        {
            CColor[] res = new CColor[NumLeds];
            for (int i = 0; i < NumLeds; i++)                           // Initiate result array to all black
            {
                res[i] = new CColor(Color.Black);
            }
            for (int i = 0; i < Slices.Count; i++)                      // Iterate over all Slices (and CColors)
            {
                Slice currSlice = slices[i];
                CColor currCColor = sliceColors[i];
                for (int j = currSlice.From; j < currSlice.To; j++)     // Assign all items in res between Slice.From and Slice.To the currCColor
                {
                    res[j] = currCColor;
                }
            }
            return res;
        }

        public override void Close()
        {
            
        }
    }


    public class ColoredSidesLedProfile : LedProfile
    {
        public ColoredSidesLedProfile(LedMatrix m, byte b)
        {
            matrix = m;
            Brightness = b;
            Ups = 30;
        }

        public override void Close()
        {
            
        }

        public override void Init()
        {
            //
        }

        public override CColor[] Update()
        {
            CColor[] Top = new CColor[matrix.Width];
            CColor[] Left = new CColor[matrix.Height];
            CColor[] Bot = new CColor[matrix.Width];
            CColor[] Right = new CColor[matrix.Height];
            for (int i = 0; i < matrix.Width; i++)
            {
                Top[i] = new CColor(Color.Red);
                Bot[i] = new CColor(Color.Red);
            }
            for (int i = 0; i < matrix.Height; i++)
            {
                Left[i] = new CColor(Color.Blue);
                Right[i] = new CColor(Color.Blue);
            }
            matrix.AssignFrom(matrix.TopLeft, Top);
            matrix.AssignFrom(matrix.TopRight, Right);
            matrix.AssignFrom(matrix.BottomRight, Bot);
            matrix.AssignFrom(matrix.BottomLeft, Left);

            return matrix.ReturnAbsoluteColors();
        }
    }
    #endregion
}