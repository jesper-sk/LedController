using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LedController
{
    public class StripConfig
    {
        public int Width;
        public int Height;
        public int MasterLength;
        public int Start;
        public bool IsClockwise;

        public StripConfig() { }

        public StripConfig(int w, int h, int s, bool cw, int ml = 0)
        {
            Width = w;
            Height = h;
            Start = s;
            IsClockwise = cw;
            MasterLength = ml;
        }

    }
    public class CColor
    {
        private Color color = Color.Black;

        public CColor() { }
        public CColor(Color c) { color = c; }

        public CColor(int R, int G, int B)
        {
            color = Color.FromArgb(R, G, B);
        }

        public CColor(int H, byte S, byte V)
        {
            HsvToRgb(H, S, V, out int R, out int G, out int B);
            color = Color.FromArgb(R, G, B);
        }

        public CColor(double H, double S, double V)
        {
            HsvToRgb(H, S, V, out int R, out int G, out int B);
            color = Color.FromArgb(R, G, B);
        }

        public Color ToColor()
        {
            return color;
        }

        public void FromColor(Color c)
        {
            color = c;
        }

        public CColor(double H, byte S, byte V)
        {
            SetFromHsv(H, S, V);
        }

        public static implicit operator Color(CColor x)
        {
            return x.ToColor();
        }

        public static implicit operator CColor(Color c)
        {
            return new CColor(c);
        }

        public void SetFromHsv(int h, byte s, byte v)
        {
            if (h > 360)
            {
                throw new InvalidOperationException("Hue should be lower than or equal to 360!");
            }
            double S = s / 255;
            double V = v / 255;
            double H = h;
            HsvToRgb(H, S, V, out int r, out int g, out int b);
            color = Color.FromArgb(r, g, b);
        }

        public void SetFromHsv(double h, byte s, byte v)
        {
            if (h > 360)
            {
                throw new InvalidOperationException("Hue should be lower than or equal to 360!");
            }
            double S = s / 255;
            double V = v / 255;
            HsvToRgb(h, S, V, out int r, out int g, out int b);
            color = Color.FromArgb(r, g, b);
        }

        static void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((int)(R * 255.0));
            g = Clamp((int)(G * 255.0));
            b = Clamp((int)(B * 255.0));
        }

        /// <summary>
        /// Clamp a value to 0-255
        /// </summary>
        static int Clamp(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }

        [XmlIgnore]
        public float H
        {
            get { return color.GetHue(); }
        }

        [XmlIgnore]
        public float S
        {
            get { return color.GetSaturation(); }
        }

        [XmlIgnore]
        public float V
        {
            get { return color.GetBrightness(); }
        }

        [XmlAttribute]
        public byte R
        {
            get { return color.R; }
            set
            {
                color = Color.FromArgb(value, G, B);
            }
        }
        [XmlAttribute]
        public byte G
        {
            get { return color.G; }
            set
            {
                color = Color.FromArgb(R, value, B);
            }
        }
        [XmlAttribute]
        public byte B
        {
            get { return color.B; }
            set
            {
                color = Color.FromArgb(R, G, value);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"R{R} ");
            sb.Append($"G{G} ");
            sb.Append($"B{B}");
            return sb.ToString();
        }
    }
    public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + (y * Width);
            Int32 col = colour.ToArgb();

            Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }


        public void Save(string name)
        {
            Bitmap.Save(name);
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
    public struct Slice : IComparable<Slice>
    {
        private int from;
        private int to;
        public int From
        {
            get
            {
                return from;
            }
            set
            {
                if (value >= to)
                {
                    throw new InvalidOperationException("From can't me higher than to!");
                }
                from = value;
            }
        }
        public int To
        {
            get
            {
                return to;
            }
            set
            {
                if (value <= from)
                {
                    throw new InvalidOperationException("To can't be lower than From!");
                }
                to = value;
            }
        }

        public Slice(int f, int t)
        {
            from = f;
            to = t;
        }

        public int CompareTo(Slice other)
        {
            return other.To.CompareTo(to);
        }

        public override string ToString()
        {
            return from + ":" + to;
        }
    }
    public class LedMatrix
    {
        public readonly int TopLeft;
        public readonly int TopRight;
        public readonly int BottomLeft;
        public readonly int BottomRight;

        public readonly int ResolutionX;
        public readonly int ResolutionY;

        public readonly int ScreenIndex;

        public readonly int Width;
        public readonly int Height;
        public readonly int Start;
        public readonly int Length;
        public readonly int MasterLength;
        public readonly bool IsCw;

        private readonly int offset;
        private CColor[] colors;

        public Rectangle[] CapRects;

        public LedMatrix(int w, int h, int s, bool cw, int ml = 0)
        {
            Width = w;
            Height = h;
            Start = s;
            IsCw = cw;

            MasterLength = ml == 0 ? Length : ml;

            ScreenIndex = 0;

            TopLeft = 0;
            TopRight = Width - 1;
            BottomRight = TopRight + Height - 1;
            BottomLeft = BottomRight + Width - 1;
            Length = BottomLeft + Height - 1;

            Length = (2 * Width + 2 * Height) - 4;
            offset = Length - Start;

            colors = new CColor[Length];
            for (int i = 0; i < Length; i++)
            {
                colors[i] = new CColor();
            }
        }

        public LedMatrix(StripConfig config)
        {
            Width = config.Width;
            Height = config.Height;
            Start = config.Start;
            IsCw = config.IsClockwise;

            MasterLength = config.MasterLength == 0 ? Length : config.MasterLength;

            ScreenIndex = 0;

            TopLeft = 0;
            TopRight = Width - 1;
            BottomRight = TopRight + Height - 1;
            BottomLeft = BottomRight + Width - 1;
            Length = BottomLeft + Height - 1;

            Length = (2 * Width + 2 * Height) - 4;
            offset = Length - Start;

            colors = new CColor[Length];
            for (int i = 0; i < Length; i++)
            {
                colors[i] = new CColor();
            }
        }

        public Rect[] GetCaptureRects(int screenIndex, RatioProfile prof)
        {
            Screen curr = Screen.AllScreens[screenIndex];
            Rectangle bounds = curr.Bounds;
            int sw = bounds.Width;
            int sh = bounds.Height;

            //Console.WriteLine($"Screen: {sw}x{sh}");

            double sr = sw / (double)sh;
            double tr = prof.CalculateRatio();

            //Console.WriteLine($"Screen Ratio {sr}, Target Ratio {tr}");

            double f = sr / tr;

            //Console.WriteLine($"Factor {f}");
            //Console.WriteLine($"1/Factor: {1 / f}");

            int tw = (int)(sw * Math.Min(1.0, 1 / f));
            int th = (int)(sh * Math.Min(1.0, f));

            //Console.WriteLine($"Target {tw}x{th}");

            int dx = (int)Math.Round((double)tw / Width);
            int dy = (int)Math.Round((double)th / Height);

            //Console.WriteLine($"dx{dx}, dy{dy}");

            int xOffset = bounds.X + ((sw - tw) / 2);
            int yOffset = bounds.Y + ((sh - th) / 2);

            //Console.WriteLine($"Offset ({xOffset},{yOffset})");

            int x = 0;
            int y = 0;
            int i = 0;
            Rect[] res = new Rect[Length];
            do
            {
                res[i] = new Rect((x * dx) + xOffset, (y * dy) + yOffset, dx, dy);
                x++;
                i++;
            } while (x < Width - 1);
            do
            {
                res[i] = new Rect((x * dx) + xOffset, (y * dy) + yOffset, dx, dy);
                y++;
                i++;
            } while (y < Height - 1);
            do
            {
                res[i] = new Rect((x * dx) + xOffset, (y * dy) + yOffset, dx, dy);
                x--;
                i++;
            } while (x > 0);
            do
            {
                res[i] = new Rect((x * dx) + xOffset, (y * dy) + yOffset, dx, dy);
                y--;
                i++;
            } while (y > 0);
            return res;
        }

        public void CleanSlate()
        {
            foreach (CColor c in colors)
            {
                c.FromColor(Color.Black);
            }
        }

        public StripConfig ToStripConfig()
        {
            return new StripConfig(Width, Height, Start, IsCw, MasterLength);
        }

        public void AssignFrom(int index, List<CColor> c)
        {
            int i = 0;
            int j = index;
            while (i < c.Count)
            {
                colors[j] = c[i];
                j = (j + 1) % Length;
                i++;
            }
        }

        public void AssignFrom(int index, CColor[] c)
        {
            int i = 0;
            int j = index;
            while (i < c.Length)
            {
                colors[j] = c[i];
                j = (j + 1) % Length;
                i++;
            }
        }

        public void AssignUpto(int index, List<CColor> c)
        {
            int i = c.Count - 1;
            int j = (index + Length - c.Count + 1) % Length;
            while (i >= 0)
            {
                colors[j] = c[i];
                j = (j + 1) % Length;
                i--;
            }
        }

        public void AssignUpto(int index, CColor[] c)
        {
            int i = c.Length - 1;
            int j = (index + Length - c.Length + 1) % Length;
            while (i >= 0)
            {
                colors[j] = c[i];
                j = (j + 1) % Length;
                i--;
            }
        }

        public CColor[] ReturnAbsoluteColors()
        {
            return IsCw ? ColorsCw() : ColorsCcw();
        }

        public CColor[] ReturnRelativeColors()
        {
            return colors;
        }

        private CColor[] ColorsCw()
        {
            CColor[] res = new CColor[Length];
            for (int i = 0; i < Length; i++)
            {
                res[CwAbsolute(i)] = colors[i];
            }
            return res;
        }

        private CColor[] ColorsCcw()
        {
            CColor[] res = new CColor[Length];
            for (int i = 0; i < Length; i++)
            {
                res[CcwAbsolute(i)] = colors[i];
            }
            return res;
        }

        private int CwAbsolute(int r)
        {
            return (offset + r) % Length;
        }

        private int CcwAbsolute(int r)
        {
            return (Length - CwAbsolute(r)) % Length;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            CColor[] temp = ReturnAbsoluteColors();
            sb.Append("Colors: \n");
            for (int i = 0; i < Length; i++)
            {
                int a = IsCw ? CwAbsolute(i) : CcwAbsolute(i);
                sb.Append($"r{i}, \t a{a}: \t {temp[a]}\n");
            }
            sb.Append($"\nTopLeft: {(IsCw ? CwAbsolute(TopLeft) : CcwAbsolute(TopLeft))}");
            sb.Append($"\nTopRight: {(IsCw ? CwAbsolute(TopRight) : CcwAbsolute(TopRight))}");
            sb.Append($"\nBottomRight: {(IsCw ? CwAbsolute(BottomRight) : CcwAbsolute(BottomRight))}");
            sb.Append($"\nBottomLeft: {(IsCw ? CwAbsolute(BottomLeft) : CcwAbsolute(BottomLeft))}");
            return sb.ToString();
        }
    }

    public static class AspectRatio
    {
        public static void EstimateAspectRatio(double ratio, out double w, out double h)
        {
            switch (Math.Round(ratio, 2))
            {
                case (1.33):
                    w = 4;
                    h = 3;
                    break;
                case (1.50):
                    w = 3;
                    h = 2;
                    break;
                case (1.60):
                    w = 16;
                    h = 10;
                    break;
                case (1.78):
                    w = 16;
                    h = 9;
                    break;
                case (2.00):
                    w = 18;
                    h = 9;
                    break;
                case (2.33):
                    w = 21;
                    h = 9;
                    break;
                case (2.39):
                    w = 43;
                    h = 18;
                    break;
                case (3.56):
                    w = 32;
                    h = 9;
                    break;
                default:
                    w = Math.Round(ratio, 2);
                    h = 1;
                    break;
            }
        }
    }

    [Serializable]
    public class RatioProfile : IEquatable<object>
    {
        [XmlAttribute]
        public double RatioWidth { get; set; }

        [XmlAttribute]
        public double RatioHeight { get; set; }

        public RatioProfile() { }
        public RatioProfile(double ratio)
        {
            AspectRatio.EstimateAspectRatio(ratio, out double w, out double h);
            RatioWidth = w;
            RatioHeight = h;
        }

        public double CalculateRatio()
        {
            return RatioWidth / RatioHeight;
        }
        public override string ToString()
        {
            return $"{RatioWidth}:{RatioHeight}";
        }

        public override bool Equals(object other)
        {
            if (other is RatioProfile r) return RatioWidth == r.RatioWidth && RatioHeight == r.RatioHeight;
            return false;
        }

        public static bool operator ==(RatioProfile a, RatioProfile b)
        {
            return a?.Equals(b) ?? b?.Equals(null) ?? true;
        }

        public static bool operator !=(RatioProfile a, RatioProfile b)
        {
            return !a?.Equals(b) ?? !b?.Equals(null) ?? false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public struct Rect
    {
        public int Width;
        public int Height;
        public int X;
        public int Y;

        public Rect(int x, int y, int w, int h)
        {
            Width = w;
            Height = h;
            X = x;
            Y = y;
        }
    }
}