using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LedController.LedProfiles
{
    public class MusicLedProfile : LedProfile
    {
        public string DeviceName;
        public int DeviceIndex;
        public MusicLedProfile() {; }
        public MusicLedProfile(string name, string parentProfileSet, int index, ColorMatrix m)
        {
            Index = index;
            Parent = parentProfileSet;
            Brightness = 64;
            LedColor = CColor.FromColor(Color.White);
            ProfileType = ProfileType.Music;
            Name = name;
            UName = $"{parentProfileSet}:{name}";
            Ups = 30;
            DeviceName = "none";
            DeviceIndex = -1;
        }

        public override void Init(ColorMatrix m)
        {
            
        }

        public override void Update(ColorMatrix m)
        {

        }

        public override void Close()
        {
            
        }
    }
}
