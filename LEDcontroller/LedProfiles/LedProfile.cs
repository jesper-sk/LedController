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

namespace LedController.LedProfiles
{
    [XmlInclude(typeof(StaticLedProfile))]
    [XmlInclude(typeof(AmbilightLedProfile))]
    [XmlInclude(typeof(RainbowLedProfile))]
    [XmlInclude(typeof(MusicLedProfile))]
    public abstract class LedProfile : IEquatable<LedProfile>
    {
        /*
         * LED profile classes should inferit this abstract class. They should further include:
         *  - Constructor that takes the profile name as a string, and a LEDconfig class
         *  - Method Update() that returns an array of Colors (leds)
         *  - Assignments of all relevant properties
         *  - Add the XmlInclude(typeof(ConcreteProfile)) attribute to the abstract profile class
         */
        public int ProfileIndex { get; set; }
        public int ProfileSetIndex { get; set; }
        public ProfileType ProfileType { get; set; }
        public int Ups { get; set; }
        public byte Brightness { get; set; }
        public string Name { get; set; }

        //TODO: transfer below properties to right LedProfile type

        //StaticLedProfile
        public CColor LedColor { get; set; }

        //RainbowLedProfile
        public int Speed { get; set; }

        //AmbilightLedProfile
        public int ScreenIndex { get; set; }
        public RatioProfile RatioProfile { get; set; }

        protected LedProfile() {; }
        protected LedProfile(string name, int index, int psindex, ProfileType type)
        {
            Name = name;
            ProfileIndex = index;
            ProfileSetIndex = psindex;
            ProfileType = type;
        }

        public virtual void Init(ColorMatrix m) {; }
        public virtual void Update(ColorMatrix m) {; }
        public virtual void Close() {; }

        public bool Equals(LedProfile other)
        {
            return other.ProfileIndex == ProfileIndex && other.ProfileSetIndex == ProfileSetIndex;
        }
        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LedProfile);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class StaticLedProfile : LedProfile
    {
        public StaticLedProfile() { }

        public StaticLedProfile(string name, int index, int psindex, ColorMatrix m) : base(name, index, psindex, ProfileType.Static)
        {
            ProfileIndex = index;
            Brightness = 64;
            LedColor = CColor.FromColor(Color.White);
            Ups = 30;
        }

        public override void Update(ColorMatrix m)
        {
            for (int i = 0; i < m.Length; i++)
            {
                m[i] = LedColor;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class RainbowLedProfile : LedProfile
    {
        double deltaHue, deltaStartHue, currHue, startHue;

        public RainbowLedProfile() { }

        public RainbowLedProfile(string name, int index, int psindex, ColorMatrix m) : base(name, index, psindex, ProfileType.Rainbow)
        {
            Brightness = 64;
            Ups = 60;
        }

        public override void Init(ColorMatrix m)
        {
            deltaHue = Math.Floor(360.0 / m.Length);
            startHue = 0;
        }
        public override void Update(ColorMatrix m)
        {
            deltaStartHue = ((double)Util.Map(Speed, 0, 100, 0, (int)Math.Floor(deltaHue * 10))) / 10;
            currHue = startHue;
            for (int i = 0; i < m.Length; i++)
            {
                m[i] = CColor.FromHsv(currHue, 1, 1);
                currHue = (currHue + deltaHue) % 360;
            }
            startHue = (startHue + deltaStartHue) % 360;
        }
    }

    public class PolitieLedProfile : LedProfile
    {
        bool redVisible, blueVisible, redVisibleVisible, blueVisibleVisible;
        int redCount, blueCount, darkCount;
        int redCountCount, blueCountCount;
        public PolitieLedProfile() { }

        public PolitieLedProfile(string name, int index, int psindex, ColorMatrix m) : base(name, index, psindex, ProfileType.Rainbow)
        {
            Brightness = 255;
            Ups = 60;
        }

        public override void Init(ColorMatrix m)
        {
            redVisible = blueVisible = false;
            redCount = blueCount = redCountCount = blueCountCount = 0;
        }

        public override void Update(ColorMatrix m)
        {
            CColor[] colorsRed = new CColor[m.Length / 2];
            CColor[] colorsBlue = new CColor[m.Length / 2];

            if (redVisible)
            {
                if (redVisibleVisible)
                {
                    for (int i = 0; i < m.Length / 2; i++)
                    {
                        colorsRed[i] = CColor.FromColor(Color.Red);
                        colorsBlue[i] = CColor.FromColor(Color.Black);
                    }
                    if (redCountCount++ > 3)
                    {
                        redVisibleVisible = false;
                        redCountCount = 0;
                    }
                }
                else
                {
                    for (int i = 0; i < m.Length / 2; i++)
                    {
                        colorsRed[i] = CColor.FromColor(Color.Black);
                        colorsBlue[i] = CColor.FromColor(Color.Black);
                    }
                    if (redCountCount++ > 3)
                    {
                        redVisibleVisible = true;
                        redCountCount = 0;
                    }
                }
                if (redCount++ > 4)
                {
                    redCount = 0;
                    redVisible = false;
                    redVisibleVisible = true;
                    blueVisible = true;
                }
            }
            else if (blueVisible)
            {
                if (blueVisibleVisible)
                {
                    for (int i = 0; i < m.Length / 2; i++)
                    {
                        colorsRed[i] = CColor.FromColor(Color.Black);
                        colorsBlue[i] = CColor.FromColor(Color.Blue);
                    }
                    if (blueCountCount++ > 1)
                    {
                        blueVisibleVisible = false;
                        blueCountCount = 0;
                    }
                }
                else
                {
                    for (int i = 0; i < m.Length / 2; i++)
                    {
                        colorsRed[i] = CColor.FromColor(Color.Black);
                        colorsBlue[i] = CColor.FromColor(Color.Black);
                    }
                    if (blueCountCount++ > 1)
                    {
                        blueVisibleVisible = true;
                        blueCountCount = 0;
                    }
                }
                if (blueCount++ > 4)
                {
                    blueCount = 0;
                    blueVisible = false;
                    blueVisibleVisible = true;
                }
            }
            else
            { 
                for (int i = 0; i < m.Length / 2; i++)
                {
                    colorsRed[i] = CColor.FromColor(Color.Black);
                    colorsBlue[i] = CColor.FromColor(Color.Black);
                }
                if (darkCount++ > 15) redVisible = true;
            }

            m.AssignFrom(m.TopLeft + (m.Width / 2) - 1, colorsRed);
            m.AssignFrom(m.BottomLeft + (m.Width / 2) - 1, colorsBlue);
        }
    }

    public class NoneLedProfile : LedProfile
    {
        public NoneLedProfile()
        {
            ProfileType = ProfileType.None;
            Ups = 1;
        }
    }

    #region SetupLedFrom profiles
    /*
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

        public override void Update()
        {
            updates++;
            CColor[] res = new CColor[NumLeds];
            for (int j = 0; j < NumLeds; j++)
            {
                res[j] = new CColor(); //defaults to black
            }
            res[i] = CColor.FromColor(Color.White);
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
                res[i] = new CColor();
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
        public ColoredSidesLedProfile(ColorMatrix m, byte b)
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
                Top[i] = CColor.FromColor(Color.Red);
                Bot[i] = CColor.FromColor(Color.Red);
            }
            for (int i = 0; i < matrix.Height; i++)
            {
                Left[i] = CColor.FromColor(Color.Blue);
                Right[i] = CColor.FromColor(Color.Blue);
            }
            matrix.AssignFrom(matrix.TopLeft, Top);
            matrix.AssignFrom(matrix.TopRight, Right);
            matrix.AssignFrom(matrix.BottomRight, Bot);
            matrix.AssignFrom(matrix.BottomLeft, Left);

            return matrix.ReturnAbsoluteColors();
        }
    }
    */
    #endregion
}