#define PRECISION
//#define MEDIAN

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using LedController;
using static System.Math;
using System.Text;

namespace Driver
{
    public class DesktopMirror : IDisposable
    {
        #region External Constants

        private const int Map = 1030;
        private const int UnMap = 1031;
        private const int TestMapped = 1051;

        private const int IGNORE = 0;
        private const int BLIT = 12;
        private const int TEXTOUT = 18;
        private const int MOUSEPTR = 48;

        private const int CDS_UPDATEREGISTRY = 0x00000001;
        private const int CDS_TEST = 0x00000002;
        private const int CDS_FULLSCREEN = 0x00000004;
        private const int CDS_GLOBAL = 0x00000008;
        private const int CDS_SET_PRIMARY = 0x00000010;
        private const int CDS_RESET = 0x40000000;
        private const int CDS_SETRECT = 0x20000000;
        private const int CDS_NORESET = 0x10000000;
        private const int MAXIMUM_ALLOWED = 0x02000000;
        private const int DM_BITSPERPEL = 0x40000;
        private const int DM_PELSWIDTH = 0x80000;
        private const int DM_PELSHEIGHT = 0x100000;
        private const int DM_POSITION = 0x00000020;
        #endregion

        #region External Methods

        [DllImport("user32.dll")]
        private static extern int ChangeDisplaySettingsEx(string lpszDeviceName, ref DeviceMode mode, IntPtr hwnd, uint dwflags, IntPtr lParam);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr pointer);

        [DllImport("user32.dll")]
        private static extern bool EnumDisplayDevices(string lpDevice, uint ideviceIndex, ref DisplayDevice lpdevice, uint dwFlags);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern int ExtEscape(IntPtr hdc, int nEscape, int cbInput, IntPtr lpszInData, int cbOutput, IntPtr lpszOutData);

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        private static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        #endregion

        public event EventHandler<DesktopChangeEventArgs> DesktopChange;
        public class DesktopChangeEventArgs : EventArgs
        {
            public int x1;
            public int y1;
            public int x2;
            public int y2;
            public OperationType type;

            public DesktopChangeEventArgs(int x1, int y1, int x2, int y2, OperationType type)
            {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
                this.type = type;
            }
        }

        private string driverInstanceName = "";
        private IntPtr _getChangesBuffer = IntPtr.Zero;
        private Thread _pollingThread = null;

        private static void SafeChangeDisplaySettingsEx(string lpszDeviceName, ref DeviceMode mode, IntPtr hwnd, uint dwflags, IntPtr lParam)
        {
            int result = ChangeDisplaySettingsEx(lpszDeviceName, ref mode, hwnd, dwflags, lParam);
            switch (result)
            {
                case 0: return; //DISP_CHANGE_SUCCESSFUL
                case 1: throw new Exception("The computer must be restarted for the graphics mode to work."); //DISP_CHANGE_RESTART
                case -1: throw new Exception("The display driver failed the specified graphics mode."); // DISP_CHANGE_FAILED
                case -2: throw new Exception("The graphics mode is not supported."); // DISP_CHANGE_BADMODE
                case -3: throw new Exception("Unable to write settings to the registry."); // DISP_CHANGE_NOTUPDATED
                case -4: throw new Exception("An invalid set of flags was passed in."); // DISP_CHANGE_BADFLAGS
                case -5: throw new Exception("An invalid parameter was passed in. This can include an invalid flag or combination of flags."); // DISP_CHANGE_BADPARAM
                case -6: throw new Exception("The settings change was unsuccessful because the system is DualView capable."); // DISP_CHANGE_BADDUALVIEW
            }
        }

        public enum MirrorState
        {
            Idle,
            Loaded,
            Connected,
            Running
        }

        public MirrorState State { get; private set; }

        private const string driverDeviceNumber = "DEVICE0";
        private const string driverMiniportName = "dfmirage";
        private const string driverName = "Mirage Driver";
        private const string driverRegistryPath = @"SYSTEM\CurrentControlSet\Hardware Profiles\Current\System\CurrentControlSet\Services";

        private int primaryScreenOffsetX;
        private int primaryScreenOffsetY;

        List<string> logger = new List<string>();
        public bool Load()
        {
            if (State != MirrorState.Idle)
            {
                logger.Add("Invalid Operation Exception Thrown at Load");
                throw new InvalidOperationException("You may call Load only if the state is Idle");
            }

            var device = new DisplayDevice();
            var deviceMode = new DeviceMode { dmDriverExtra = 0 };              /* Assigned properties:
                                                                                    - dmDriverExtra - 0
                                                                                    - dmSize - size of deviceMode
                                                                                    - dmBitsPerPel - primary screen bpp
                                                                                    - dmDeviceName - empty string
                                                                                    - dmFiels - idk lol
                                                                                */

            device.CallBack = Marshal.SizeOf(device);
            deviceMode.dmSize = (short)Marshal.SizeOf(deviceMode);
            deviceMode.dmBitsPerPel = Screen.PrimaryScreen.BitsPerPixel;        // Get bits per pixel of primaryscreen

            if (deviceMode.dmBitsPerPel == 24)
                deviceMode.dmBitsPerPel = 32;

            bitmapBitsPerPixel = deviceMode.dmBitsPerPel;
            logger.Add($"Bitmap Bpp: {bitmapBitsPerPixel}");

            deviceMode.dmDeviceName = string.Empty;
            deviceMode.dmFields = (DM_BITSPERPEL | DM_PELSWIDTH | DM_PELSHEIGHT | DM_POSITION);

            GetPrimaryScreenOffsets(out primaryScreenOffsetX, out primaryScreenOffsetY);

            int totalWidth = 0;
            int totalHeight = 0;
            foreach(Screen s in Screen.AllScreens)
            {
                int limitX = primaryScreenOffsetX + s.Bounds.X + s.Bounds.Width;
                int limitY = primaryScreenOffsetY + s.Bounds.Y + s.Bounds.Height;
                if (limitX > totalWidth) totalWidth = limitX;
                if (limitY > totalHeight) totalHeight = limitY;
            }

            bitmapHeight = deviceMode.dmPelsHeight = totalHeight;
            bitmapWidth = deviceMode.dmPelsWidth = totalWidth;

            logger.Add($"Bitmap width; {bitmapWidth}, height: {bitmapHeight}");
            logger.Add("driver res " + Screen.PrimaryScreen.Bounds.ToString());
            bool deviceFound;
            uint deviceIndex = 0;

            while (deviceFound = EnumDisplayDevices(null, deviceIndex, ref device, 0))      // User user32.dll::EnumDisplayDevices to enumerate all devices until the right one occurs
            {
                if (device.DeviceString == driverName)
                    break;
                deviceIndex++;
            }

            if (!deviceFound) return false;

            driverInstanceName = device.DeviceName;

            /*_registryKey = Registry.LocalMachine.OpenSubKey(driverRegistryPath, true);
            if (_registryKey != null)
                _registryKey = _registryKey.CreateSubKey(driverMiniportName);
            else
                throw new Exception("Couldn't open registry key");

            if (_registryKey != null)
                _registryKey = _registryKey.CreateSubKey(driverDeviceNumber);
            else
                throw new Exception("Couldn't open registry key");

            //			_registryKey.SetValue("Cap.DfbBackingMode", 0);
            //			_registryKey.SetValue("Order.BltCopyBits.Enabled", 1);
            _registryKey.SetValue("Attach.ToDesktop", 0);*/

            #region This was CommitDisplayChanges

            SafeChangeDisplaySettingsEx(device.DeviceName, ref deviceMode, IntPtr.Zero, CDS_UPDATEREGISTRY, IntPtr.Zero);
            SafeChangeDisplaySettingsEx(device.DeviceName, ref deviceMode, IntPtr.Zero, 0, IntPtr.Zero);

            #endregion

            State = MirrorState.Loaded;

            return true;
        }

        public bool Connect()
        {
            if (State != MirrorState.Loaded)
                throw new InvalidOperationException("You may call Connect only if the state is Loaded");

            bool result = MapSharedBuffers(); // Adjusts _running
            if (result)
            {
                State = MirrorState.Connected;
            }

            return result;
        }

        public void Start()
        {
            if (State != MirrorState.Connected)
                throw new InvalidOperationException("You may call Start only if the state is Connected");

            if (_terminatePollingThread == null)
                _terminatePollingThread = new ManualResetEvent(false);
            else
                _terminatePollingThread.Reset();

            _pollingThread = new Thread(pollingThreadProc) { IsBackground = true };
            _pollingThread.Start();

            State = MirrorState.Running;
        }

        /// <summary>
        /// Driver buffer polling interval, in msec.
        /// </summary>
        private const int PollInterval = 100;

        private void pollingThreadProc()
        {
            long oldCounter = long.MaxValue;
            while (true)
            {
                var getChangesBuffer = (GetChangesBuffer)Marshal.PtrToStructure(_getChangesBuffer, typeof(GetChangesBuffer));
                var buffer = (ChangesBuffer)Marshal.PtrToStructure(getChangesBuffer.Buffer, typeof(ChangesBuffer));

                // Initialize oldCounter
                if (oldCounter == long.MaxValue)
                    oldCounter = buffer.counter;

                if (oldCounter != buffer.counter)
                {
                    //Trace.WriteLine(string.Format("Counter changed. Old is {0} new is {1}", oldCounter, buffer.counter));
                    for (long currentChange = oldCounter; currentChange != buffer.counter; currentChange++)
                    {
                        if (currentChange >= ChangesBuffer.MAXCHANGES_BUF)
                            currentChange = 0;

                        if (DesktopChange != null)
                            DesktopChange(this,
                                          new DesktopChangeEventArgs(buffer.pointrect[currentChange].rect.x1,
                                                                     buffer.pointrect[currentChange].rect.y1,
                                                                     buffer.pointrect[currentChange].rect.x2,
                                                                     buffer.pointrect[currentChange].rect.y2,
                                                                     (OperationType)buffer.pointrect[currentChange].type));
                    }

                    oldCounter = buffer.counter;
                }

                // Just to prevent 100-percent CPU load and to provide thread-safety use manual reset event instead of simple in-memory flag.
                if (_terminatePollingThread.WaitOne(PollInterval, false))
                {
                    //Trace.WriteLine("The thread now exits");
                    break;
                }
            }

            // We can be sure that _pollingThreadTerminated exists
            _pollingThreadTerminated.Set();
        }

        private ManualResetEvent _terminatePollingThread;
        private ManualResetEvent _pollingThreadTerminated;

        private const int PollingThreadTerminationTimeout = 10000;

        public void Stop()
        {
            if (State != MirrorState.Running) return;

            if (_pollingThreadTerminated == null)
                _pollingThreadTerminated = new ManualResetEvent(false);
            else
                _pollingThreadTerminated.Reset();

            // Terminate polling thread
            _terminatePollingThread.Set();

            // Wait for it...
            if (!_pollingThreadTerminated.WaitOne(PollingThreadTerminationTimeout, false))
                _pollingThread.Abort();

            State = MirrorState.Connected;
        }

        public void Disconnect()
        {
            if (State == MirrorState.Running)
                Stop();

            if (State != MirrorState.Connected)
                return;

            UnmapSharedBuffers();
            State = MirrorState.Loaded;
        }

        public void Unload()
        {
            if (State == MirrorState.Running)
                Stop();
            if (State == MirrorState.Connected)
                Disconnect();

            if (State != MirrorState.Loaded)
                return;

            var deviceMode = new DeviceMode();
            deviceMode.dmSize = (short)Marshal.SizeOf(typeof(DeviceMode));
            deviceMode.dmDriverExtra = 0;
            deviceMode.dmFields = (DM_BITSPERPEL | DM_PELSWIDTH | DM_PELSHEIGHT | DM_POSITION);

            var device = new DisplayDevice();
            device.CallBack = Marshal.SizeOf(device);
            deviceMode.dmDeviceName = string.Empty;
            uint deviceIndex = 0;
            while (EnumDisplayDevices(null, deviceIndex, ref device, 0))
            {
                if (device.DeviceString.Equals(driverName))
                    break;

                deviceIndex++;
            }

            /*Debug.Assert(_registryKey != null);

            _registryKey.SetValue("Attach.ToDesktop", 0);
            _registryKey.Close();*/

            deviceMode.dmDeviceName = driverMiniportName;

            if (deviceMode.dmBitsPerPel == 24) deviceMode.dmBitsPerPel = 32;

            #region This was CommitDisplayChanges

            SafeChangeDisplaySettingsEx(device.DeviceName, ref deviceMode, IntPtr.Zero, CDS_UPDATEREGISTRY, IntPtr.Zero);
            SafeChangeDisplaySettingsEx(device.DeviceName, ref deviceMode, IntPtr.Zero, 0, IntPtr.Zero);

            #endregion

            State = MirrorState.Idle;
        }

        private IntPtr _globalDC;

        private bool MapSharedBuffers()
        {
            _globalDC = CreateDC(driverInstanceName, null, null, IntPtr.Zero);
            if (_globalDC == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }

            if (_getChangesBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_getChangesBuffer);
            }

            _getChangesBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(GetChangesBuffer)));

            int res = ExtEscape(_globalDC, Map, 0, IntPtr.Zero, Marshal.SizeOf(typeof(GetChangesBuffer)), _getChangesBuffer);
            if (res > 0)
                return true;

            return false;
        }

        private void UnmapSharedBuffers()
        {
            int res = ExtEscape(_globalDC, UnMap, Marshal.SizeOf(typeof(GetChangesBuffer)), _getChangesBuffer, 0, IntPtr.Zero);
            if (res < 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());

            Marshal.FreeHGlobal(_getChangesBuffer);
            _getChangesBuffer = IntPtr.Zero;

            ReleaseDC(IntPtr.Zero, _globalDC);
        }

        private int bitmapWidth, bitmapHeight, bitmapBitsPerPixel;
        public Bitmap GetScreen()
        {

            if (State != MirrorState.Connected && State != MirrorState.Running)
                throw new InvalidOperationException("In order to get current screen you must at least be connected to the driver");

            PixelFormat format;
            if (bitmapBitsPerPixel == 16)
                format = PixelFormat.Format16bppRgb565;
            else if (bitmapBitsPerPixel == 24)
                format = PixelFormat.Format24bppRgb;
            else if (bitmapBitsPerPixel == 32)
                format = PixelFormat.Format32bppArgb;
            else
            {
                Debug.Fail("Unknown pixel format");
                throw new Exception("Unknown pixel format");
            }

            Bitmap result = new Bitmap(bitmapWidth, bitmapHeight, format);

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bitmapWidth, bitmapHeight);
            BitmapData bmpData = result.LockBits(rect, ImageLockMode.WriteOnly, format);
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;
            // Declare an array to hold the bytes of the bitmap.
            int bytes = bitmapWidth * 4 * bitmapHeight;

            GetChangesBuffer getChangesBuffer = (GetChangesBuffer)Marshal.PtrToStructure(_getChangesBuffer, typeof(GetChangesBuffer));
            byte[] data = new byte[bytes];
            try
            {
                Marshal.Copy(getChangesBuffer.UserBuffer, data, 0, bytes);
            }
            catch (AccessViolationException)
            {
                logger.Add("AccessViolationException thrown");
            }

            // Copy the RGB values into the bitmap.

            Marshal.Copy(data, 0, ptr, bytes);

            result.UnlockBits(bmpData);

            return result;
        }

        public CColor[] GetAvgCColorFromScreen(Rect[] rects, double precision)
        {
            if (State != MirrorState.Connected && State != MirrorState.Running)
                throw new InvalidOperationException("In order to get current screen you must at least be connected to the driver");

            int bytesPerPixel = bitmapBitsPerPixel / 8;

            GetChangesBuffer buffer = (GetChangesBuffer)Marshal.PtrToStructure(_getChangesBuffer, typeof(GetChangesBuffer));
            IntPtr pointer = buffer.UserBuffer;

            CColor[] res = new CColor[rects.Length];

            unsafe
            {
                byte* origin = (byte*)pointer.ToPointer();

                for(int i = 0; i < rects.Length; i++)
                {
                    Rect currRect = rects[i];

                    byte* start =
                        origin +
                        (currRect.X + primaryScreenOffsetX) * bytesPerPixel +
                        (currRect.Y + primaryScreenOffsetY) * bitmapWidth * bytesPerPixel;

                    int numX = (int)(currRect.Width * precision);
                    int numY = (int)(currRect.Height * precision);

                    int deltaX = (numX - 1) / (currRect.Width - numX) + 1;
                    int deltaY = (numY - 1) / (currRect.Height - numY) + 1;

                    int wid = numX + (numX - 1) * (deltaX - 1);
                    int hei = numY + (numY - 1) * (deltaY - 1);

                    int startX = (currRect.Width - wid) / 2;
                    int startY = (currRect.Height - hei) / 2;

                    int finX = startX + wid;
                    int finY = startY + hei;

                    int tot = 0;
                    long[] totals = { 0, 0, 0 };

                    for (int y = startY; y < finY; y += deltaY)
                    {
                        for (int x = startX; x < finX; x += deltaX)
                        {
                            for(int c = 0; c < 3; c++)
                            {
                                totals[c] += *(start + (y * bitmapWidth * bytesPerPixel) + (x * bytesPerPixel) + c);
                            }
                            tot++;
                        }
                    }

                    int b = (int)Round((double)totals[0] / tot);
                    int g = (int)Round((double)totals[1] / tot);
                    int r = (int)Round((double)totals[2] / tot);

                    res[i] = CColor.FromRgb(r, g, b);
                }
            }

            return res;
        }

        public CColor[] GetMedCColorFromScreen(Rect[] rects, double precision, int numHueSlices)
        {
            if (State != MirrorState.Connected && State != MirrorState.Running)
                throw new InvalidOperationException("In order to get current screen you must at least be connected to the driver");

            int bytesPerPixel = bitmapBitsPerPixel / 8;

            GetChangesBuffer buffer = (GetChangesBuffer)Marshal.PtrToStructure(_getChangesBuffer, typeof(GetChangesBuffer));
            IntPtr pointer = buffer.UserBuffer;

            CColor[] res = new CColor[rects.Length];

            unsafe
            {
                byte* origin = (byte*)pointer.ToPointer();

                for (int i = 0; i < rects.Length; i++)
                {
                    Rect currRect = rects[i];

                    byte* start =
                        origin +
                        (currRect.X + primaryScreenOffsetX) * bytesPerPixel +
                        (currRect.Y + primaryScreenOffsetY) * bitmapWidth * bytesPerPixel;

                    int numX = (int)(currRect.Width * precision);
                    int numY = (int)(currRect.Height * precision);

                    int deltaX = (numX - 1) / (currRect.Width - numX) + 1;
                    int deltaY = (numY - 1) / (currRect.Height - numY) + 1;

                    int wid = numX + (numX - 1) * (deltaX - 1);
                    int hei = numY + (numY - 1) * (deltaY - 1);

                    int startX = (currRect.Width - wid) / 2;
                    int startY = (currRect.Height - hei) / 2;

                    int finX = startX + wid;
                    int finY = startY + hei;

                    double[] hues = new double[numHueSlices];
                    int[] hueCount = new int[numHueSlices];

                    double sats = 0;
                    double vals = 0;
                    int count = 0;

                    for (int y = startY; y < finY; y += deltaY)
                    {
                        for (int x = startX; x < finX; x += deltaX)
                        {
                            byte* bp = start + (y * bitmapWidth * bytesPerPixel) + (x * bytesPerPixel);
                            byte* gp = bp + 1;
                            byte* rp = gp + 1;
                            Util.RgbToHsv(*rp, *gp, *bp, out double h, out double s, out double v);
                            int slice = (int)(h % numHueSlices);
                            hues[slice] += h;
                            hueCount[slice]++;
                            sats += s;
                            vals += v;
                            count++;
                        }
                    }

                    int ind = 0;
                    for (int j = 1; j < numHueSlices; j++)
                        if (hueCount[j] > hueCount[ind])
                            ind = j;

                    double hueFinal = hues[ind] / hueCount[ind];
                    double satFinal = sats / count;
                    double valFinal = vals / count;

                    res[i] = CColor.FromHsv(hueFinal, satFinal, valFinal);
                }
            }
            return res;
        }

        unsafe void GetCoordinates(byte* origin, byte* point, int bytesPerPixel, out int x, out int y, out int c)
        {
            long d = point - origin;
            y = Convert.ToInt32(DivRem(d, (long)bitmapWidth * bytesPerPixel, out d));
            x = Convert.ToInt32(DivRem(d, (long)bytesPerPixel, out d));
            c = Convert.ToInt32(d);
        }

        private void GetPrimaryScreenOffsets(out int x, out int y)
        {
            x = y = 0;
            foreach (Screen s in Screen.AllScreens)
            {
                if (s.Bounds.X < x) x = s.Bounds.X;
                if (s.Bounds.Y < y) y = s.Bounds.Y;
            }
            x *= -1;
            y *= -1;
            Logger.Log($"Primary Screen offsets: {x}, {y}");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (State != MirrorState.Idle)
                Unload();
            File.WriteAllLines("logDriver.txt", logger);
        }
    }
}
