﻿using System;
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
        public ColorMatrix Matrix { get; set; }
        public LedProfile ActiveProfile
        {
            get
            {
                return activeProfile;
            }
            private set
            {
                Logger.Log($"Activating profile {value?.Name ?? "null"}");
                Logger.Log($"\tClosing previous profile... ({activeProfile?.Name ?? "null"})");
                if (activeProfile != null) activeProfile.Close();
                activeProfile = value;
                Logger.Log("\tInitiating new profile...");
                if (activeProfile != null) activeProfile.Init(Matrix);
                else SetLedsBlack();
                Logger.Log($"New profile activated.");
            }
        }
        private LedProfile activeProfile;

        private readonly MainForm main;
        public Visualizer CurrVisualizer { get; private set; } 

        private readonly System.Timers.Timer comCheckTimer;
        private System.Threading.Timer updateTimer;

        private readonly SerialPort arduPort;
        private bool outputCheckerCancelled;

        private DateTime dt;

        private delegate void UpdateFpsLabel(int fps);
        private readonly UpdateFpsLabel UpdateFpsDelegate;
        private delegate void UpdateVisualizer(CColor[] rel);
        private readonly UpdateVisualizer UpdateVisDelegate;

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

            UpdateFpsDelegate = new UpdateFpsLabel(main.UpdateFpsLabel);
            UpdateVisDelegate = new UpdateVisualizer(main.VisualizeColors);

            comCheckTimer.Elapsed += new ElapsedEventHandler(ComCheckTimer_Tick);
        }

        #region COM Connect/Disconnect
        public bool ConnectCom(string com)
        {
            arduPort.PortName = com;
            try { arduPort.Open(); }
            catch(Exception e) { Logger.Balloon("LedController Connection Error", "Opening COM-port failed: " +  e.Message, ToolTipIcon.Error); return false; }
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
                Logger.Log($"Arduino Connection Failure: {e.Message}");
                inp = "notAda";
            }
            if (inp == "Ada") { return true; }
            else { return false; }
        }

        public void DisconnectCom()
        {
            Logger.Log("disconnect Com");
            outputCheckerCancelled = true;
            Logger.Log("Deactivating...");
            Deactivate();
            Logger.Log("closing arduPort...");
            IsConnected = false;
            arduPort.Close();
        }

        private void CheckArduinoInput()
        {
            Logger.Log("Arduino Checker Task started.");
                while (!outputCheckerCancelled)
                {
                    try
                    {
                        string inp = arduPort.ReadLine();
                        Logger.Log(inp);
                    }
                    catch (TimeoutException) {Logger.Log("string doing stuff"); }
                }
            outputCheckerCancelled = false;
            Logger.Log("Arduino Checker Task stopped.");
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
                    Logger.Log($"Arduino found on {comPorts[index]}");
                    done = true;
                }
            }      
        }

        #endregion

        #region Activate/Deactivate LEDProfiles
        public void SetActiveProfile(LedProfile act)
        {

        }

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
            if (updateTimer != null)
            {
                updateTimer.Dispose();
                if (ActiveProfile != null) Thread.Sleep((int)((1.0 / ActiveProfile.Ups) * 1000));
            }
            LedProfile temp = ActiveProfile;
            if (reset) ActiveProfile = null;
            IsSending = false;
            return temp;
        }

        public bool Reenable()
        {
            if (ActiveProfile != null)
            {
                dt = DateTime.Now;
                int interval = Convert.ToInt32((1 / (double)ActiveProfile.Ups) * 1000);
                updateTimer = new System.Threading.Timer(new TimerCallback(SendBytes), null, 0, interval);
                IsSending = true;
                return true;
            }
            else return false;
        }
        #endregion

        #region Sending to Arduino
        public void OverrideSendBytes(CColor[] ledColors, byte brightness)
        {
            if (IsConnected)
            {
                Deactivate();
                Logger.Log("sending bytes");
                ManualSendBytes(ledColors, brightness);
            }
        }

        public void SetLedsBlack()
        {
            if (IsConnected)
            {
                CColor[] black = new CColor[Matrix.MasterLength];
                for (int i = 0; i < Matrix.MasterLength; i++)
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
                buffer[i * 3 + 7] = Convert.ToByte(ledColors[i].R);
                buffer[i * 3 + 7 + 1] = Convert.ToByte(ledColors[i].G);
                buffer[i * 3 + 7 + 2] = Convert.ToByte(ledColors[i].B);
            }
            int count = buffer.Length;
            try { arduPort.Write(buffer, 0, count); }
            catch (Exception e) { Logger.Log($"Writing bytes to port failed: {e.Message}"); }
        }

        private void SendBytes(object state)
        {
            //Logger.Log($"\nSending bytes from thread \n{formatThreadInfo()}");
            //DateTime start = DateTime.Now;
            ActiveProfile.Update(Matrix);
            CColor[] ledColors = Matrix.ReturnAbsoluteColors();
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
            //Logger.Log(ByteToString(buffer));
            int count = buffer.Length;
            try { arduPort.Write(buffer, 0, count); }
            catch { /*Logger.Log("Writing bytes to port failed: {0}", e.Message);*/ }
            TimeSpan span = DateTime.Now - dt;
            dt = DateTime.Now;
            int fps = (int)(1 / span.TotalSeconds);
            try
            {
                main.Invoke(UpdateVisDelegate, new object[] { Matrix.ReturnRelativeColors() });
                main.Invoke(UpdateFpsDelegate, new object[] { fps });
            }
            catch (ObjectDisposedException) {; }
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

        #endregion
    }
}
