using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using LedController.LedProfiles;
using System.Xml.Serialization;
using System.Windows.Threading;
using System.IO;
using Timer = System.Timers.Timer;

namespace LedController
{
    public static class nComHandler
    {
        public static List<ComDevice> Devices { get; private set; } = new List<ComDevice>();
        public static string Error { get; private set; }
        
        public static void AddDevice(string comPort, int baudRate = 115200)
        {
            Devices.Add(new ComDevice(comPort, baudRate));
        }

        public static bool ConnectDevice(int devInd)
        {
            if (devInd < Devices.Count)
            {
                ComDevice device = Devices[devInd];
                try
                {
                    device.Port.Open();
                }
                catch (Exception e)
                {
                    Error = e.Message;
                    return false;
                }
                device.IsConnected = true;
                return true;
            }
            else
            {
                throw new InvalidOperationException("Invalid device index");
            }
        }

        public static bool CheckCompatibility(int devInd)
        {
            if (devInd < Devices.Count)
            {
                ComDevice device = Devices[devInd];
                if (!device.IsConnected){ throw new InvalidOperationException("Device is not connected");}
                else if (device.IsEnabled){ throw new InvalidOperationException("Disable device first");}
                else
                {
                    string inp;
                    try
                    {
                        inp = device.Port.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Logger.Log($"Possible device compatibility issue: {e.Message}");
                        return false;
                    }
                    return inp == "Ada";
                }
            }
            else
            {
                throw new InvalidOperationException("Invalid device index");
            }
        }

        public static void DisconnectDevice(int devInd)
        {
            if (devInd < Devices.Count)
            {
                if (Devices[devInd].IsConnected) Devices[devInd].Port.Close();
            }
            else
            {
                throw new InvalidOperationException("Invalid device index");
            }
        }
    }

    public struct ComDevice
    {
        public bool IsConnected;
        public bool IsEnabled;
        public SerialPort Port;

        public ComDevice(string comPort, int baudRate)
        {
            Port = new SerialPort(comPort, baudRate);
            IsConnected = false;
            IsEnabled = false;
        }
    }




    public static class LedHandler
    {
        private static List<LedStrip> strips = new List<LedStrip>();

        public static LedhandlerState ErrorState = LedhandlerState.OK;
        public static string Error;
        public static void AddStrip(StripInfo info)
        {
            strips.Add(new LedStrip(info));
        }

        public static bool Open(int stripIndex)
        {
            if (stripIndex >= strips.Count) throw new InvalidOperationException("Invalid strip index");
            else
            {
                LedStrip strip = strips[stripIndex];
                try
                {
                    strip.SerialPort.Open();
                }
                catch(Exception e)
                {
                    ErrorState = LedhandlerState.DeviceFailedToConnect;
                    Error = e.Message;
                    return false;
                }
                strip.State = StripState.Open;
                ErrorState = LedhandlerState.OK;
                return true;
            }
        }

        public static bool Close(int stripIndex)
        {
            if (stripIndex >= strips.Count) throw new InvalidOperationException("Invalid strip index");
            else
            {
                LedStrip strip = strips[stripIndex];
                try
                {
                    strip.SerialPort.Close();
                }
                catch (Exception e)
                {
                    ErrorState = LedhandlerState.DeviceFailedToDisconnect;
                    Error = e.Message;
                    return false;
                }
                strip.State = StripState.Closed;
                ErrorState = LedhandlerState.OK;
                return true;
            }
        }

        public static void SetActiveProfile(int stripIndex, LedProfile profile)
        {
            if (stripIndex >= strips.Count) throw new InvalidOperationException("Invalid strip index");
            else
            {
                LedStrip strip = strips[stripIndex];
                LedProfile temp = strip.ActiveProfile;
                strip.ActiveProfile = new NoneLedProfile();
                if (strip.ActiveProfile != null) strip.ActiveProfile.Close();
                profile.Init(strip.ColorMatrix);
                strip.ActiveProfile = profile;
            }
        }

        public static void SaveToFile()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<StripInfo>));
            List<StripInfo> toSave = new List<StripInfo>(strips.Count);
            foreach (LedStrip strip in strips) toSave.Add(strip.ToStripInfo());
            using (TextWriter writer = new StreamWriter(@".\LedInfo.txt")) ser.Serialize(writer, toSave);
        }

        public static bool LoadFromFile()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<StripInfo>));
            List<StripInfo> info;
            if (!File.Exists(@".\LedInfo.txt")) return false;
            using (FileStream stream = new FileStream(@".\LedInfo.txt", FileMode.Open)) { info = (List<StripInfo>) ser.Deserialize(stream); }
            foreach(StripInfo inf in info) { strips.Add(new LedStrip(inf)); }
            return true;
        }
    }

    public struct StripStatus
    {
        string Name;
        string PortName;
        bool IsConnected;
        bool IsActive;
        string ActiveProfileSet;
        int ActiveProfileIndex;
    }
    public enum LedhandlerState
    {
        OK,
        DeviceFailedToConnect,
        DeviceFailedToDisconnect
    }

    public class StripInfo
    {
        public string Name;
        public int Width;
        public int Height;
        public int MasterLength;
        public int Start;
        public bool IsClockwise;
        public string ComPort;
        public int BaudRate;
        public LedProfile ActiveProfile;

        public StripInfo() { }

        public StripInfo(int w, int h, int s, bool cw, int overlap = 0)
        {
            Width = w;
            Height = h;
            Start = s;
            IsClockwise = cw;
            MasterLength = 2 * w + 2 * h + overlap;
        }

    }

    public class LedStrip
    {
        public string Name;
        public SerialPort SerialPort;          // The COM-port of the device
        public LedProfile ActiveProfile;       // If active, the profile currently using. Null otherwise
        public ColorMatrix ColorMatrix;        // The matrix the strip is associated with
        public Timer UpdateTimer;
        public StripState State;

        public LedStrip(StripInfo info)
        {
            State = StripState.Closed;
            ActiveProfile = info.ActiveProfile;
            SerialPort = new SerialPort(info.ComPort, info.BaudRate);
            ColorMatrix = new ColorMatrix(info);
            UpdateTimer = new Timer
            {
                AutoReset = true,
                Enabled = false
            };
            UpdateTimer.Elapsed += UpdateTimer_Elapsed;
        }

        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SendBytes();
        }

        public StripInfo ToStripInfo()
        {
            return new StripInfo()
            {
                Width = ColorMatrix.Width,
                Height = ColorMatrix.Height,
                MasterLength = ColorMatrix.MasterLength,
                Start = ColorMatrix.Start,
                IsClockwise = ColorMatrix.IsCw,
                ComPort = SerialPort.PortName,
                BaudRate = SerialPort.BaudRate
            };
        }

        private void SendBytes()
        {
            ActiveProfile.Update(ColorMatrix);
            CColor[] ledColors = ColorMatrix.ReturnAbsoluteColors();
            byte brightness = ActiveProfile.Brightness;
            ushort numLeds = (ushort)ledColors.Length;
            byte lHigh = checked((byte)(numLeds >> 8));
            byte lLow = checked((byte)(numLeds & 0xFF));
            byte checksum = checked((byte)(lHigh ^ lLow ^ 0x55));
            byte[] buffer = new byte[numLeds * 3 + 7];
            buffer[0] = (byte)'A';
            buffer[1] = (byte)'d';
            buffer[2] = (byte)'b';
            buffer[3] = lHigh;
            buffer[4] = lLow;
            buffer[5] = checksum;
            buffer[6] = brightness;
            for (int i = 0; i < numLeds; i++)
            {
                buffer[i * 3 + 7] = Convert.ToByte(ledColors[i].R);
                buffer[i * 3 + 7 + 1] = Convert.ToByte(ledColors[i].G);
                buffer[i * 3 + 7 + 2] = Convert.ToByte(ledColors[i].B);
            }
            int count = buffer.Length;
            try { SerialPort.Write(buffer, 0, count); }
            catch { /*Logger.Log("Writing bytes to port failed: {0}", e.Message);*/ }
        }
    }

    public enum StripState
    {
        Closed,
        Open,
        Sending
    }

}
