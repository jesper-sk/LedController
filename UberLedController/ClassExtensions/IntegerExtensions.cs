using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberLedController.ClassExtensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        /// Breaks the input int32 into high and low int16s.
        /// </summary>
        /// <param name="val">Input int32</param>
        /// <param name="high">High int16</param>
        /// <param name="low">Low int16</param>
        public static unsafe void HighLow(this int val, out short high, out short low)
        {
            low = ((short*)&val)[0];
            high = ((short*)&val)[1];
        }

        /// <summary>
        /// Get the integer square root.
        /// </summary>
        /// <param name="n">Input integer</param>
        /// <returns>The integer square root of the input</returns>
        public static int Isqrt(this int n)
        {
            if (n < 0) throw new ArgumentException(nameof(n) + " was smaller than 0");
            if (n < 2) return n;
            int small = Isqrt(n >> 2) << 1;
            int large = small + 1;
            if (small * large > n) return small;
            return large;
        }

        /// <summary>
        /// Alters the range of an integer.
        /// </summary>
        /// <param name="x">Input integer</param>
        /// <param name="inLow">Current lower limit</param>
        /// <param name="inHigh">Current upper limit</param>
        /// <param name="outLow">Lower limit to normalize to</param>
        /// <param name="outHigh">Upper limit to normalize to</param>
        /// <returns></returns>
        public static int Normalize(this int x, int inLow, int inHigh, int outLow = 0, int outHigh = 100) 
            => (x - inLow) * (outHigh - outLow) / (inHigh - inLow) + outLow;
    }
}
