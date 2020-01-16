using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace LedController
{
    public static class Util
    {
        public static double Map(double x, double inLow, double inHigh, double outLow, double outHigh)
        {
            return (x - inLow) * (outHigh - outLow) / (inHigh - inLow) + outLow;
        }

        public static unsafe void HighLow32(int val, out short high, out short low)
        {
            low = ((short*)&val)[0];
            high = ((short*)&val)[1];
        }

        public static void RgbToHsv(byte r, byte g, byte b, out double h, out double s, out double v)
        {
            double cMax;
            double cMin;

            int dom = 1;

            double rNorm = cMax = cMin = r / 255.0;
            double gNorm = g / 255.0;
            if (gNorm > rNorm)
            {
                cMax = gNorm;
                dom = 2;
            }
            else cMin = gNorm;
            double bNorm = b / 255.0;
            if (bNorm > cMax)
            {
                cMax = bNorm;
                dom = 3;
            }
            else if (bNorm < cMin) cMin = bNorm;

            double delta = cMax - cMin;
            //Logger.Log($"delta: {delta}");
            //Logger.Log($"dom: {dom}");
            if (delta == 0) h = s = 0;
            else
            {
                h = dom switch
                {
                    1 => 60 * ((gNorm - bNorm) / delta % 6),
                    2 => 60 * (((bNorm - rNorm) / delta) + 2),
                    _ => 60 * (((rNorm - gNorm) / delta) + 4),
                };
                while (h < 0) h += 360;
                s = delta / cMax;
            }
            v = cMax;
        }

        public static void HsvToRgb(double h, double s, double v, out int r, out int g, out int b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (v <= 0)
            { R = G = B = 0; }
            else if (s <= 0)
            {
                R = G = B = v;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = v * (1 - s);
                double qv = v * (1 - s * f);
                double tv = v * (1 - s * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = v;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = v;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = v;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = v;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = v;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = v;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = v;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = v;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        R = G = B = v; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((byte)(R * 255.0));
            g = Clamp((byte)(G * 255.0));
            b = Clamp((byte)(B * 255.0));

            static byte Clamp(byte i)
            {
                if (i < 0) return 0;
                if (i > 255) return 255;
                return i;
            }
        }

        public static string ByteToString(byte[] inp)
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

        public static string ByteToString(List<byte> inp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            sb.Append(inp[0]);
            for (int i = 1; i < inp.Count; i++)
            {
                byte b = inp[i];
                sb.Append(", ");
                sb.Append(b);
            }
            sb.Append(']');
            return sb.ToString();
        }

        public static string FormatProfileName(string name)
        {
            if (name == null) return "None";
            string[] spl = name.Split(':');
            StringBuilder sb = new StringBuilder();
            sb.Append(spl[0]);
            sb.Append(" -> ");
            sb.Append(spl[1]);
            return sb.ToString();
        }
    }
    public class CColor
    {
        [XmlAttribute]
        public int R = 0;
        [XmlAttribute]
        public int G = 0;
        [XmlAttribute]
        public int B = 0;

        /// <summary>
        /// Returns a CColor-instance based on RGB-values
        /// </summary>
        /// <param name="r">R-value, [0, 255]</param>
        /// <param name="g">G-value, [0, 255]</param>
        /// <param name="b">B-value, [0, 255]</param>
        /// <returns>A CColor-instance based on RGB-values</returns>
        public static CColor FromRgb(int r, int g, int b)
        {
            CColor res = new CColor
            {
                R = r,
                G = g,
                B = b
            };
            return res;
        }

        /// <summary>
        /// Returns a CColor-instance based on HSV-values
        /// </summary>
        /// <param name="h">H-value, in degrees [0, 360]</param>
        /// <param name="s">S-value, [0, 1]</param>
        /// <param name="v">V-value, [0, 1]</param>
        /// <returns>A CColor-instance based on HSV-values</returns>
        public static CColor FromHsv(double h, double s, double v)
        {
            HsvToRgb(h, s, v, out int r, out int g, out int b);
            CColor res = new CColor
            {
                R = r,
                G = g,
                B = b
            };
            return res;
        }

        public static CColor FromColor(Color c)
        {
            return FromRgb(c.R, c.G, c.B);
        }

        static void RgbToHsv(int r, int g, int b, out double h, out double s, out double v)
        {
            double cMax;
            double cMin;

            int dom = 1;

            double rNorm = cMax = cMin = r / 255.0;
            double gNorm = g / 255.0;
            if (gNorm > rNorm)
            {
                cMax = gNorm;
                dom = 2;
            }
            else cMin = gNorm;
            double bNorm = b / 255.0;
            if (bNorm > cMax)
            {
                cMax = bNorm;
                dom = 3;
            }
            else if (bNorm < cMin) cMin = bNorm;

            double delta = cMax - cMin;
            Logger.Log($"delta: {delta}");
            Logger.Log($"dom: {dom}");
            if (delta == 0) h = s = 0;
            else
            {
                h = dom switch
                {
                    1 => 60 * ((gNorm - bNorm) / delta % 6),
                    2 => 60 * (((bNorm - rNorm) / delta) + 2),
                    _ => 60 * (((rNorm - gNorm) / delta) + 4),
                };
                while (h < 0) h += 360;
                s = delta / cMax;
            }
            v = cMax;
        }

        static void HsvToRgb(double h, double s, double v, out int r, out int g, out int b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (v <= 0)
            { R = G = B = 0; }
            else if (s <= 0)
            {
                R = G = B = v;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = v * (1 - s);
                double qv = v * (1 - s * f);
                double tv = v * (1 - s * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = v;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = v;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = v;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = v;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = v;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = v;
                        G = pv;
                        B = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                    case 6:
                        R = v;
                        G = tv;
                        B = pv;
                        break;
                    case -1:
                        R = v;
                        G = pv;
                        B = qv;
                        break;

                    // The color is not defined, we should throw an error.

                    default:
                        R = G = B = v; // Just pretend its black/white
                        break;
                }
            }
            r = Clamp((byte)(R * 255.0));
            g = Clamp((byte)(G * 255.0));
            b = Clamp((byte)(B * 255.0));

            byte Clamp(byte i)
            {
                if (i < 0) return 0;
                if (i > 255) return 255;
                return i;
            }
        }

        public void GetHsv(out double h, out double s, out double v)
        {
            RgbToHsv(R, G, B, out h, out s, out v);
        }
        public void SetHsv(double h, double s, double v)
        {
            HsvToRgb(h, s, v, out R, out G, out B);
        }

        public Color ToColor()
        {
            return Color.FromArgb(R, G, B);
        }

        public static implicit operator CColor(Color c)
        {
            return FromColor(c);
        }
        public static implicit operator Color(CColor c)
        {
            return c.ToColor();
        }

        public static CColor operator +(CColor x, CColor y)
        {
            int nR = (x.R + y.R) / 2;
            int nG = (x.G + y.G) / 2;
            int nB = (x.B + y.B) / 2;
            Logger.Log($"x: {x}");
            Logger.Log($"y: {y}");
            CColor res = FromRgb(nR, nG, nB);
            Logger.Log($"r: {res}");
            return res;
        }

        public static CColor Blend(CColor c1, CColor c2, double t)
        {
            return FromRgb(
                BlendColorValue(c1.R, c2.R),
                BlendColorValue(c1.G, c2.G),
                BlendColorValue(c1.B, c2.B)
                );

            int BlendColorValue(int a, int b)
            {
                return (int)Math.Sqrt(((1 - t) * (a * a)) + (t * (b * b)));
            }
        }

        public override string ToString()
        {
            return $"R={R},G={G},B={B}";
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
    public class ColorMatrix
    {
        public readonly int TopLeft;
        public readonly int TopRight;
        public readonly int BottomLeft;
        public readonly int BottomRight;

        public readonly int Width;
        public readonly int Height;
        public readonly int Start;
        public readonly int Length;
        public readonly int MasterLength;
        public readonly bool IsCw;
        public readonly int Offset;

        private CColor[] colors;

        public ColorMatrix(int w, int h, int s, bool cw, int ml = 0)
        {
            Width = w;
            Height = h;
            Start = s;
            IsCw = cw;

            MasterLength = ml == 0 ? Length : ml;

            TopLeft = 0;
            TopRight = Width - 1;
            BottomRight = TopRight + Height - 1;
            BottomLeft = BottomRight + Width - 1;
            Length = BottomLeft + Height - 1;

            Length = 2 * Width + 2 * Height - 4;
            Offset = Length - Start;

            Init();
        }

        public ColorMatrix() {; }

        public ColorMatrix(StripInfo config)
        {
            Width = config.Width;
            Height = config.Height;
            Start = config.Start;
            IsCw = config.IsClockwise;

            MasterLength = config.MasterLength == 0 ? Length : config.MasterLength;

            TopLeft = 0;
            TopRight = Width - 1;
            BottomRight = TopRight + Height - 1;
            BottomLeft = BottomRight + Width - 1;
            Length = BottomLeft + Height - 1;

            Length = 2 * Width + 2 * Height - 4;
            Offset = Length - Start;

            Init();
        }

        public CColor this[int i]
        {
            get { return colors[i]; }
            set { colors[i] = value; }
        }

        public void Init()
        {
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

            //Logger.Log($"Screen: {sw}x{sh}");

            double sr = sw / (double)sh;
            double tr = prof.CalculateRatio();

            //Logger.Log($"Screen Ratio {sr}, Target Ratio {tr}");

            double f = sr / tr;

            //Logger.Log($"Factor {f}");
            //Logger.Log($"1/Factor: {1 / f}");

            int tw = (int)(sw * Math.Min(1.0, 1 / f));
            int th = (int)(sh * Math.Min(1.0, f));

            //Logger.Log($"Target {tw}x{th}");

            int dx = (int)Math.Round((double)tw / Width);
            int dy = (int)Math.Round((double)th / Height);

            //Logger.Log($"dx{dx}, dy{dy}");

            int xOffset = bounds.X + ((sw - tw) / 2);
            int yOffset = bounds.Y + ((sh - th) / 2);

            //Logger.Log($"Offset ({xOffset},{yOffset})");

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

        public void Clear()
        {
            for (int c = 0; c < colors.Length; c++)
            {
                colors[c] = new CColor();
            }
        }

        public StripInfo ToStripConfig()
        {
            return new StripInfo(Width, Height, Start, IsCw, MasterLength);
        }

        public void AssignFrom(int index, List<CColor> c)
        {
            int i = 0;
            int j = Filter(index);
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
            int j = Filter(index);
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
            int j = (Filter(index) + Length - c.Count + 1) % Length;
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
            int j = (Filter(index) + Length - c.Length + 1) % Length;
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

        private int Filter(int p)
        {
            return (Length + p) % Length;
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
            return (Offset + r) % Length;
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
    public static class Logger
    {
        public static Queue<string> Q = new Queue<string>();
        public static Queue<Tuple<string, string, int, ToolTipIcon>> BalloonQueue = new Queue<Tuple<string, string, int, ToolTipIcon>>();

        private static StringBuilder log = new StringBuilder();
        public static void Log(string txt)
        {
            if (Env.Debugging) Console.WriteLine(txt);
            log.AppendLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {txt}");
            Q.Enqueue($"[{DateTime.Now.ToString("HH:mm:ss")}] {txt}\n");
        }

        public static void Balloon(string title, string text, ToolTipIcon icon, int customTimeout = 5000)
        {
            BalloonQueue.Enqueue(new Tuple<string, string, int, ToolTipIcon>(title, text, customTimeout, icon));
        }

        public static void Warn(string txt)
        {
            throw new NotImplementedException();
        }

        public static void Error(string txt)
        {
            throw new NotImplementedException();
        }

        public static void Save()
        {
            File.WriteAllText(@".\Execution Log.txt", log.ToString()); 
        }

        public static void Archive()
        {
            if (!Directory.Exists(@".\Log Archive")) Directory.CreateDirectory(@".\Log Archive");
            Console.WriteLine(DateTime.Now.ToString(@"yyyy.MM.dd.HH\hmm\mss\s K"));
            File.WriteAllText($@".\Log Archive\Log {DateTime.Now.ToString(@"yyyy-MM-dd HH\hmm\mss\s")}.txt", log.ToString());
        }
    }
    public static class AspectRatio
    {
        public static void EstimateAspectRatio(double ratio, out double w, out double h)
        {
            switch (Math.Round(ratio, 2))
            {
                case 1.33:
                    w = 4;
                    h = 3;
                    break;
                case 1.50:
                    w = 3;
                    h = 2;
                    break;
                case 1.60:
                    w = 16;
                    h = 10;
                    break;
                case 1.78:
                    w = 16;
                    h = 9;
                    break;
                case 2.00:
                    w = 18;
                    h = 9;
                    break;
                case 2.33:
                    w = 21;
                    h = 9;
                    break;
                case 2.39:
                    w = 43;
                    h = 18;
                    break;
                case 3.56:
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

        public override string ToString()
        {
            return $"X={X},Y={Y},W={Width},H={Height}";
        }
    }
}