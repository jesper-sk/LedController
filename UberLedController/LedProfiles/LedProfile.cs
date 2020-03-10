using System;
using System.Drawing;
using System.Xml.Serialization;


namespace UberLedController.LedProfiles
{
    /// <summary>
    /// Base Led Profile class. Every profile must inherit this class.
    /// </summary>
    public abstract class LedProfile : IEquatable<LedProfile>
    {
        #region IEquatable implementation
        public bool Equals(LedProfile other)
        {
            return other.ProfileIndex == ProfileIndex 
                && other.ProfileSetIndex == ProfileSetIndex;
        }

        public override bool Equals(object obj)
        {
            if (obj is LedProfile prof)
            {
                return Equals(obj);
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        /// <summary>
        /// The name of the profile that will be displayed in the UI
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Unique specifier of this profile within its profile set.
        /// </summary>
        public int ProfileIndex { get; set; }

        /// <summary>
        /// Unique specifier of the profile set that contains this profile.
        /// </summary>
        public int ProfileSetIndex { get; set; }

        /// <summary>
        /// Determines the type of this profile
        /// </summary>
        public ProfileType ProfileType { get; set; }

        /// <summary>
        /// The updates per second. How many times per second Update() should be invoked.
        /// </summary>
        public int Ups { get; set; }

        /// <summary>
        /// The master brightness of this profile. Will be sent to the device.
        /// </summary>
        public byte Brightness { get; set; }

        /// <summary>
        /// Reserved for XML deserialization. Do not use.
        /// </summary>
        protected LedProfile() { }

        /// <summary>
        /// Base constructor. Each class that inhertits LedProfile should call this using base().
        /// </summary>
        /// <param name="name">Profile name</param>
        /// <param name="idx">Profile index</param>
        /// <param name="psidx">Profile set index</param>
        /// <param name="type">Profile type</param>
        protected LedProfile(string name, int idx, int psidx, ProfileType type)
        {
            Name = name;
            ProfileIndex = idx;
            ProfileSetIndex = psidx;
            ProfileType = type;
        }

    }

    /// <summary>
    /// Determines the type of a led profile.
    /// </summary>
    public enum ProfileType
    {
        /// <summary>
        /// Static color
        /// </summary>
        Static,
        
        /// <summary>
        /// Rotating rainbow
        /// </summary>
        Rainbow, 

        /// <summary>
        /// Screen capture and project on strip
        /// </summary>
        Ambilight, 

        /// <summary>
        /// Music integration
        /// </summary>
        Music,

        /// <summary>
        /// For None profile type
        /// </summary>
        None, 

        /// <summary>
        /// For debuggin purposes
        /// </summary>
        Other
    }

    /// <summary>
    /// Activate a NoneLedProfile object whenver no profile should be activated
    /// </summary>
    public class NoneLedProfile : LedProfile
    {
        public NoneLedProfile()
        {
            ProfileType = ProfileType.None;
            Ups = 1;
        }
    }
}
