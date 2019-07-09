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
using System.Windows.Threading;

namespace LedController
{
    /*
     * TODO:
     * - change arduino check thread to (async) Task
     * - 
     */ 
    public class ComHandler
    {
        public bool IsConnected { get; private set; }
        public bool IsSending { get; private set; }
        public string Com { get; private set; }
        public LedProfile ActiveProfile
        {
            get
            {
                return activeProfile;
            }
            private set
            {
                if (activeProfile != null) activeProfile.Close();
                activeProfile = value;
                if (activeProfile != null) activeProfile.Init();
            }
        }
        private LedProfile activeProfile;

        private MainForm main;
        public Visualizer CurrVisualizer { get; private set; } 

        private System.Timers.Timer comCheckTimer;
        private System.Threading.Timer updateTimer;

        private SerialPort arduPort;
        private bool outputCheckerCancelled;

        private DateTime dt;

        private delegate void UpdateFpsLabel(int fps);
        private UpdateFpsLabel myDelegate;

        private LedMatrix matrix;

        public ComHandler(MainForm mainForm)
        {
            CurrVisualizer = null;
            ActiveProfile = null;
            IsConnected = false;
            IsSending = false;

            main = mainForm;

            comCheckTimer = new System.Timers.Timer()
            {
                Interval = 1000
            };
            arduPort = new SerialPort
            {
                BaudRate = 115200,
                ReadTimeout = 1200,
                
                //WriteTimeout = 1000
            };
            outputCheckerCancelled = false;

            myDelegate = new UpdateFpsLabel(main.UpdateFpsLabel);

            comCheckTimer.Elapsed += new ElapsedEventHandler(ComCheckTimer_Tick);
        }

        #region COM Connect/Disconnect
        public bool ConnectCom(string com, bool overrideCheck = false)
        {
            arduPort.PortName = com;
            try { arduPort.Open(); }
            catch(Exception e) { MessageBox.Show("Opening COM-port failed: " +  e.Message); return false; }
            //outputChecker.Start();
            IsConnected = true;
            Com = arduPort.PortName;
            return true;
        }

        public bool CheckIfArduino()
        {
            string inp;
            try
            {
                arduPort.ReadLine();
                inp = arduPort.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Arduino Connection Failure: {0}", e.Message);
                inp = "notAda";
            }
            if (inp == "Ada") { return true; }
            else { return false; }
        }

        public void DisconnectCom()
        {
            Console.WriteLine("disconnect Com");
            outputCheckerCancelled = true;
            Console.WriteLine("Deactivating...");
            Deactivate();
            Console.WriteLine("closing arduPort...");
            IsConnected = false;
            arduPort.Close();
        }

        private void CheckArduinoInput()
        {
            Console.WriteLine("Arduino Checker Task started.");
                while (!outputCheckerCancelled)
                {
                    try
                    {
                        string inp = arduPort.ReadLine();
                        Console.WriteLine(inp);
                    }
                    catch (TimeoutException) {Console.WriteLine("string doing stuff"); }
                }
            outputCheckerCancelled = false;
            Console.WriteLine("Arduino Checker Task stopped.");
        }

        public void StartCheckComs()
        {
            //if (!comCheckTimer.Enabled) comCheckTimer.Start();
            Parallel.Invoke(() => ComCheckTimer_Tick(null, null));
        }

        public void StopCheckComs()
        {
            if (comCheckTimer.Enabled) comCheckTimer.Stop();
        }

        private void ComCheckTimer_Tick(object sender, ElapsedEventArgs eea)
        {
            string[] comPorts = SerialPort.GetPortNames();
            Task<bool>[] tasks = new Task<bool>[comPorts.Length];
            for (int i = 0; i < comPorts.Length; i++)
            {
                tasks[i] = Task<bool>.Factory.StartNew
                    (
                    (object obj) =>
                    {
                        if (!(obj is string com)) return false;
                        return true;
                    },
                    comPorts[i]
                    );
            }
            bool done = false;
            while (!done)
            {
                int index = Task.WaitAny(tasks);
                if (tasks[index].Result == true)
                {
                    comCheckTimer.Stop();
                    Console.WriteLine("Arduino found on {0}", comPorts[index]);
                    done = true;
                }
            }      
        }

        #endregion

        #region Activate/Deactivate LEDProfiles
        public void SetActive(LedProfile activeProfile)
        {
            if (activeProfile == null)
            {
                IsSending = false;
                ActiveProfile = null;
                main.UpdateFpsLabel();
            }
            else
            {
                IsSending = true;
                Deactivate();
                ActiveProfile = activeProfile;
                /*nupdateTimer.Interval = (1 / (float)this.ActiveProfile.Ups) * 1000;
                ActiveProfile.Init();
                SendBytes(this.ActiveProfile.Update(), this.ActiveProfile.Brightness);
                nupdateTimer.Start();*/

                dt = DateTime.Now;
                int interval = Convert.ToInt32((1 / (double)ActiveProfile.Ups) * 1000);
                updateTimer = new System.Threading.Timer(new TimerCallback(SendBytes), null, 0, interval);
            }
        }

        public LedProfile Deactivate(bool reset = false)
        {
            if (updateTimer != null) updateTimer.Dispose();
            SetLedsBlack();
            LedProfile temp = ActiveProfile;
            if (reset) ActiveProfile = null;
            IsSending = false;
            return temp;
        }
        #endregion

        #region Sending to Arduino
        public void OverrideSendBytes(CColor[] ledColors, byte brightness)
        {
            if (IsConnected)
            {
                Deactivate();
                Console.WriteLine("sending bytes");
                ManualSendBytes(ledColors, brightness);
            }
        }

        public void SetLedsBlack()
        {
            if (IsConnected)
            {
                CColor[] black = new CColor[60];
                for (int i = 0; i < 60; i++)
                {
                    black[i] = new CColor(); //defaults to black
                }
                ManualSendBytes(black, 0);
            }
        }

        private void ManualSendBytes(CColor[] ledColors, byte brightness)
        {
            ushort numLeds = Convert.ToUInt16(ledColors.Length);
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
                buffer[i * 3 + 7] = ledColors[i].R;
                buffer[i * 3 + 7 + 1] = ledColors[i].G;
                buffer[i * 3 + 7 + 2] = ledColors[i].B;
            }
            //Console.WriteLine(ByteToString(buffer));
            if (CurrVisualizer != null)
            {
                CurrVisualizer.Update(ledColors);
            }
            int count = buffer.Length;
            try { arduPort.Write(buffer, 0, count); }
            catch (Exception e) { Console.WriteLine("Writing bytes to port failed: {0}", e.Message); }
        }

        private void SendBytes(object state)
        {
            //Console.WriteLine($"\nSending bytes from thread \n{formatThreadInfo()}");
            DateTime start = DateTime.Now;
            CColor[] ledColors = ActiveProfile.Update();
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
                buffer[i * 3 + 7] = ledColors[i].R;
                buffer[i * 3 + 7 + 1] = ledColors[i].G;
                buffer[i * 3 + 7 + 2] = ledColors[i].B;
            }
            //Console.WriteLine(ByteToString(buffer));
            if (CurrVisualizer != null)
            {
                CurrVisualizer.Update(ledColors);
            }
            int count = buffer.Length;
            try { arduPort.Write(buffer, 0, count); }
            catch (Exception e) { Console.WriteLine("Writing bytes to port failed: {0}", e.Message); }
            TimeSpan span = DateTime.Now - dt;
            int fps = (int)(1 / span.TotalSeconds);
            try { main.Invoke(myDelegate, new object[] { fps }); }
            catch (ObjectDisposedException) {; }
            dt = DateTime.Now;
        }

        private string getThreadInfo()
        {
            Thread thread = Thread.CurrentThread;
            StringBuilder sb = new StringBuilder();
            sb.Append($"Name: {thread.Name}");
            sb.Append($"\nBackground: {thread.IsBackground}");
            sb.Append($"\nThread Pool: {thread.IsThreadPoolThread}");
            sb.Append($"\nThread ID: {thread.ManagedThreadId}");
            return sb.ToString();
        }

        #region Visualizer
        public void AddVisualizer(Visualizer v)
        {
            CurrVisualizer = v;
        }

        public void RemoveVisualizer()
        {
            CurrVisualizer = null;
        }
        #endregion

        string ByteToString(byte[] inp)
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
        #endregion
    }
}
