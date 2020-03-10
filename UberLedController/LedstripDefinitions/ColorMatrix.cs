using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UberLedController.LedstripDefinitions
{
    public class ColorMatrix
    {
        public int TopLeftIdx { get; private set; }
        public int TopRightIdx { get; private set; }
        public int BottomLeftIdx { get; private set; }
        public int BottomRightIdx { get; private set; }
        public int StripStartIdx { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Overlap { get; private set; }

        /// <summary>
        /// The strip length, with possible led overlap exlcluded
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// The strip length, with possible led overlap included.
        /// </summary>
        public int LengthOverlap { get; private set; }
        public bool RotatesClockwise { get; private set; }

        /// <summary>
        /// For XML deserialization.
        /// </summary>
        public ColorMatrix() { }


    }
}
