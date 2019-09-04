#define PRECISION

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


        public CColor[] GetAvgCColorFromScreen(Rect[] rects, bool print, double precision)
        {
            CColor[] res = new CColor[rects.Length];

            if (State != MirrorState.Connected && State != MirrorState.Running)
                throw new InvalidOperationException("In order to get current screen you must at least be connected to the driver");

            int bytesPerPixel = bitmapBitsPerPixel / 8;
            int totalBytes = bitmapWidth * bytesPerPixel * bitmapHeight;
            int alpha = bytesPerPixel - 3;

            GetChangesBuffer buffer = (GetChangesBuffer)Marshal.PtrToStructure(_getChangesBuffer, typeof(GetChangesBuffer));
            IntPtr pointer = buffer.UserBuffer;

            StringBuilder log = new StringBuilder();

            //Logger.Log($"Bitmap Width {bitmapWidth}, Height {bitmapHeight}");
            //Logger.Log($"No. bytes: {totalBytes}");

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

                    //Logger.Log($"\nRect {i}, {currRect.ToString()}");
                    //Logger.Log($"Coordinates offset: X={currRect.X + primaryScreenOffsetX},Y={currRect.Y + primaryScreenOffsetY}");
                    //GetCoordinates(origin, curr, out int xs, out int ys, out int cs);
                    //Logger.Log($"byte ({xs},{ys},{cs}), no. {curr - origin}");

#if PRECISION
                    int tot = 0;
                    long[] totals = { 0, 0, 0 };

                    int numX = (int)(currRect.Width * precision);
                    int numY = (int)(currRect.Height * precision);

                    int diffX = currRect.Width - numX;
                    int diffY = currRect.Height - numY;

                    int gapX = ((diffX > 0) ? (numX - 1) / diffX : 0);
                    int gapY = ((diffY > 0) ? (numY - 1) / diffY : 0);

                    int wid = numX + (numX - 1) * gapX;
                    int hei = numY + (numY - 1) * gapY;

                    int startX = (currRect.Width - wid) / 2;
                    int startY = (currRect.Height - hei) / 2;

                    int finX = startX + wid;
                    int finY = startY + hei;

                    int deltaX = gapX + 1;
                    int deltaY = gapY + 1;

                    //Logger.Log($"startX:{startX}, startY:{startY}, {deltaX}, {deltaY}");

                    for(int y = startY; y < finY; y += deltaY)
                    {
                        for (int x = startX; x < finX; x += deltaX)
                        {
                            for(int c = 0; c < 3; c++)
                            {
                                byte* curr = start + (y * bitmapWidth * bytesPerPixel) + (x * bytesPerPixel) + c;
                                try
                                {
                                    totals[c] += *curr;
                                }
                                catch(AccessViolationException)
                                {
                                    GetCoordinates(start, curr, out int xp, out int yp, out int cp);
                                    Logger.Log($"AVE: for ({x},{y},{c}), pointer ({xp},{yp},{cp})");
                                }
                            }
                            tot++;
                        }
                    }

                    int b = (int)Round((double)totals[0] / tot);
                    int g = (int)Round((double)totals[1] / tot);
                    int r = (int)Round((double)totals[2] / tot);

                    res[i] = CColor.FromRgb(r, g, b);

#else
                    int tot = 0;
                    long[] totals = { 0, 0, 0 };

                    int x, y;
                    x = y = 0;

                    while(y < currRect.Height)
                    {
                        while(x < currRect.Width)
                        {
                            for(int c = 0; c < 3; c++)
                            {
                                byte* curr = start + (y * bitmapWidth * bytesPerPixel) + (x * bytesPerPixel) + c;
                                totals[c] += *curr;
                            }
                            tot++;
                            x++;
                        }
                        x = 0;
                        y++;
                    }

                    int b = (int)Round((double)totals[0] / tot);
                    int g = (int)Round((double)totals[1] / tot);
                    int r = (int)Round((double)totals[2] / tot);

                    res[i] = CColor.FromRgb(r, g, b);
#endif
                }
            }


            if (print) File.WriteAllText("ding.txt", log.ToString());

            return res;

            unsafe void oGetAverageColors(IntPtr buff, Rect currRect, out int r, out int g, out int b)
            {
                byte* origin = (byte*)buff.ToPointer(); //Gets the byte-pointer of the top-left position of the screens

                byte* recStart = 
                    origin +
                    (currRect.X + primaryScreenOffsetX) * bytesPerPixel +
                    ((currRect.Y + primaryScreenOffsetY) * bitmapWidth) * bytesPerPixel;

                int tot = 0;
                long[] totals = { 0, 0, 0 };

                byte* tr = recStart + (currRect.Width - 1) * bytesPerPixel;
                byte* bl = recStart + (currRect.Height - 1) * (currRect.Width - 1) * bytesPerPixel;
                byte* br = bl + (currRect.Width - 1) * bytesPerPixel;

                AddColors2d(origin, recStart, tr, bl, br, currRect.Height, currRect.Width, ref totals, ref tot, 1);

                b = (int)Round((double)totals[0] / tot);
                g = (int)Round((double)totals[1] / tot);
                r = (int)Round((double)totals[2] / tot);
            }

            unsafe void GetCoordinates(byte* origin, byte* point, out int x, out int y, out int c)
            {
                long d = point - origin;
                y = Convert.ToInt32(DivRem(d, (long)bitmapWidth * bytesPerPixel, out d));
                x = Convert.ToInt32(DivRem(d, (long)bytesPerPixel, out d));
                c = Convert.ToInt32(d);
            }

#region Failed Trials
            unsafe void AddColors(byte* origin, byte* scan0, int width, int height, ref long[] totals, ref int num, int maxI = -1, int i = 0)
            {
                if (i == maxI || (width == 0 && height == 0))
                {
                    byte* curr = &*scan0;
                    for (int color = 0; color < 3; color++)
                    {
                        totals[color] += *curr;
                        curr++;
                    }
                    num++;
                }
                else
                {
                    int newWidth = width / 2;
                    int newHeight = height / 2;
                    byte* scan1 = scan0 + (newWidth + 1) * bytesPerPixel;
                    byte* scan2 = scan0 + (newHeight + 1) * bitmapWidth * bytesPerPixel;
                    byte* scan3 = scan2 + (newWidth + 1) * bytesPerPixel;
                    int ni = i + 1;
                    AddColors(origin, scan0, newWidth, newHeight, ref totals, ref num, maxI, ni);
                    AddColors(origin, scan1, newWidth, newHeight, ref totals, ref num, maxI, ni);
                    AddColors(origin, scan2, newWidth, newHeight, ref totals, ref num, maxI, ni);
                    AddColors(origin, scan3, newWidth, newHeight, ref totals, ref num, maxI, ni);
                }
            }

            unsafe void AddColors2d(byte* origin, byte* tl, byte* tr, byte* bl, byte* br, int h, int w, ref long[] totals, ref int tot, int maxI = -1, int i = 0, string ind = "")
            {
                int nw = w / 2;
                int nh = h / 2;

                //Logger.Log($"nw: {nw}, nh: {nh}");

                if ((nw == 0 && nh == 0) || i == maxI)
                {
                    AddColors0d(origin, tl, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                    AddColors0d(origin, tr, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                    AddColors0d(origin, bl, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                    AddColors0d(origin, br, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                }
                else if (nw == 0)
                {
                    int hEven = h % 2 == 0 ? 1 : 0;

                    byte*[][] n = new byte*[4][];
                    for (int x = 0; x < 4; x++) n[x] = new byte*[2];

                    //topleft
                    n[0][0] = tl;
                    n[0][1] = n[0][0] + nh * bitmapWidth * bytesPerPixel;

                    //topright
                    n[1][0] = tr;
                    n[1][1] = n[1][0] + nh * bitmapWidth * bytesPerPixel;

                    //bottomleft
                    n[2][1] = bl;
                    n[2][0] = n[2][0] - nh * bitmapWidth * bytesPerPixel;

                    //bottomright
                    n[3][1] = br;
                    n[3][0] = n[3][0] - nh * bitmapWidth * bytesPerPixel;

                    for (int x = 0; x < 4; x++)
                    {
                        //Logger.Log($"{ind}{x}:");
                        for (int y = 0; y < 2; y++)
                        {
                            //Logger.Log($"{ind}  {y}: {(*n[x][y]).ToString()}");
                        }
                    }

                    for (int j = 0; j < 4; j++)
                    {
                        AddColors1d(origin, n[j][0], n[j][1], nh, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                    }
                }
                else if (nh == 0)
                {
                    int wEven = w % 2 == 0 ? 1 : 0;

                    byte*[][] n = new byte*[4][];
                    for (int x = 0; x < 4; x++) n[x] = new byte*[2];

                    //topleft
                    n[0][0] = tl;
                    n[0][1] = n[0][0] + nw * bytesPerPixel;

                    //topright
                    n[1][1] = tr;
                    n[1][0] = n[1][1] - nw * bytesPerPixel;

                    //bottomleft
                    n[2][0] = bl;
                    n[2][1] = n[2][0] + nw * bytesPerPixel;

                    //topright
                    n[3][1] = tr;
                    n[3][0] = n[1][1] - nw * bytesPerPixel;

                    for (int x = 0; x < 4; x++)
                    {
                        //Logger.Log($"{ind}{x}:");
                        for (int y = 0; y < 2; y++)
                        {
                            //Logger.Log($"{ind}  {y}: {(*n[x][y]).ToString()}");
                        }
                    }

                    for (int j = 0; j < 4; j++)
                    {
                        AddColors1d(origin, n[j][0], n[j][1], nw, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                    }
                }
                else
                {
                    int wEven = w % 2 == 0 ? 1 : 0;
                    int hEven = h % 2 == 0 ? 1 : 0;

                    byte*[,] n = new byte*[4, 4];

                    //topleft
                    n[0, 0] = tl;
                    n[0, 1] = n[0, 0] + nw * bytesPerPixel;
                    n[0, 2] = n[0, 0] + nh * bitmapWidth * bytesPerPixel;
                    n[0, 3] = n[0, 2] + nw * bytesPerPixel;

                    //topright
                    n[1, 1] = tr;
                    n[1, 0] = n[1, 1] - nw * bytesPerPixel;
                    n[1, 2] = n[1, 0] + nh * bitmapWidth * bytesPerPixel;
                    n[1, 3] = n[1, 2] + nw * bytesPerPixel;

                    //bottomleft
                    n[2, 2] = bl;
                    n[2, 3] = n[2, 2] + nw * bytesPerPixel;
                    n[2, 1] = n[2, 3] - nh * bitmapWidth * bytesPerPixel;
                    n[2, 0] = n[2, 1] - nw * bytesPerPixel;

                    //bottomright
                    n[3, 3] = br;
                    n[3, 2] = n[3, 3] - nw * bytesPerPixel;
                    n[3, 0] = n[3, 2] - nh * bitmapWidth * bytesPerPixel;
                    n[3, 1] = n[3, 0] + nw * bytesPerPixel;

                    for (int x = 0; x < 4; x++)
                    {
                        //Logger.Log($"{ind}{x}:");
                        for (int y = 0; y < 4; y++)
                        {
                            //Logger.Log($"{ind}  {y}: {(*n[x, y]).ToString()}");
                        }
                    }

                    for (int j = 0; j < 4; j++)
                    {
                        AddColors2d(origin, n[j, 0], n[j, 1], n[j, 2], n[j, 3], nh, nw, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                    }
                }
            }

            unsafe void AddColors1d(byte* origin, byte* f, byte* t, int l, ref long[] totals, ref int tot, int maxI, int i, string ind)
            {
                int nl = l / 2;
                //Logger.Log($"{ind}nl: {nl}");

                if (nl == 0)
                {
                    AddColors0d(origin, f, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                    AddColors0d(origin, t, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                }
                else
                {
                    byte* ft = f + nl * bytesPerPixel;
                    byte* tf = t - nl * bytesPerPixel;

                    //Logger.Log($"{ind}0:");
                    //Logger.Log($"{ind} 0: {*f}");
                    //Logger.Log($"{ind} 1: {*ft}");
                    //Logger.Log($"{ind}1:");
                    //Logger.Log($"{ind} 0: {*tf}");
                    //Logger.Log($"{ind} 1: {*t}");

                    AddColors1d(origin, f, ft, nl, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                    AddColors1d(origin, tf, t, nl, ref totals, ref tot, maxI, i + 1, $"{ind}|   ");
                }
            }

            unsafe void AddColors0d(byte* origin, byte* l, ref long[] totals, ref int tot, int maxI, int i, string ind)
            {
                byte* curr = l;
                for (int color = 0; color < 3; color++)
                {
                    try
                    {
                        totals[color] += *curr;
                    }
                    catch
                    {
                        GetCoordinates(origin, curr, out int xa, out int ya, out int ca);
                        Logger.Log($"AVE at abs({xa},{ya},{ca}), no. {curr - origin}");
                        Console.ReadKey();
                    }
                    totals[color] += *curr;
                    curr++;
                }
                tot++;
            }
#endregion

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
