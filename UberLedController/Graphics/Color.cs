using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using static UberLedController.ClassExtensions.FuncExtensions;
using static UberLedController.ClassExtensions.IntegerExtensions;
using DColor = System.Drawing.Color;
using MColor = System.Windows.Media.Color;

namespace UberLedController.Graphics
{
    /// <summary>
    /// Custom serializable color class
    /// </summary>
    public class Color
    {
        [XmlAttribute]
        public byte R = 0;
        [XmlAttribute]
        public byte G = 0;
        [XmlAttribute]
        public byte B = 0;

        public static Color FromRgb(byte r, byte g, byte b)
        {
            return new Color
            {
                R = r,
                G = g,
                B = b
            };
        }
        public static Color FromRgb(int r, int g, int b)
        {
            return new Color
            {
                R = (byte)r,
                G = (byte)g,
                B = (byte)b
            };
        }
        public Color FromHsv(double h, double s, double v)
        {
            HsvToRgb(h, s, v, out byte r, out byte g, out byte b);
            return FromRgb(r, g, b);
        }

        public static Color FromExtColor(DColor c) => FromRgb(c.R, c.G, c.B);
        public static Color FromExtColor(MColor c) => FromRgb(c.R, c.G, c.B);

        public int ToArgb32() => 255 << 24 | R << 16 | G << 8 | B;
        public static Color FromArgb32(int rgb) => FromRgb(
            (rgb >> 16) & 255,
            (rgb >> 8) & 255,
             rgb & 255);

        private static void HsvToRgb(double h, double s, double v, out byte r, out byte g, out byte b)
        {
            double H = h % 360;

            if (H < 0) throw new InvalidOperationException("h should be greater than 0");

            double R, G, B;

            if (v <= 0) R = G = B = 0;
            else if (s <= 0) R = G = B = v;
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
        private static void RgbToHsv(byte r, byte g, byte b, out double h, out double s, out double v)
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

        public void GetHsv(out double h, out double s, out double v) => RgbToHsv(R, G, B, out h, out s, out v);
        public void SetHsv(double h, double s, double v) => HsvToRgb(h, s, v, out R, out G, out B);

        public DColor ToDColor() => DColor.FromArgb(R, G, B);
        public MColor ToMColor() => MColor.FromArgb(255, R, G, B);

        public static implicit operator Color(DColor c) => FromExtColor(c);
        public static implicit operator Color(MColor c) => FromExtColor(c);

        public static implicit operator DColor(Color c) => c.ToDColor();
        public static implicit operator MColor(Color c) => c.ToMColor();

        public static Color Blend(Color a, Color b, double t = 0.5)
        {
            return FromRgb(
                BlendColorValues(a.R, b.R),
                BlendColorValues(a.G, b.G),
                BlendColorValues(a.B, b.B)
                );

            byte BlendColorValues(byte a, byte b) => (byte)Math.Sqrt(((1 - t) * (a * a)) + (t * (b * b)));
        }

        public static Color[] EqualBlend(params Color[][] lists) 
            => new Func<IEnumerable<Color>, Color>( cols => cols.EqualBlend() ).ZipOver(lists);
    }

    public static class ColorExtensions
    {
        public static Color BlendWith(this Color a, Color b, double t = 0.5) => Color.Blend(a, b, t);

        public static Color EqualBlend(this IEnumerable<Color> colors)
        {
            int l = colors.Count();
            return Color.FromRgb(
                (colors.Select(c => c.R * c.R).Sum() / l).Isqrt(),
                (colors.Select(c => c.G * c.G).Sum() / l).Isqrt(),
                (colors.Select(c => c.B * c.B).Sum() / l).Isqrt()
                );
        }
    }

}
