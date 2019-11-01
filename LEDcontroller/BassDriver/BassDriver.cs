using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.BassWasapi;
using static Un4seen.Bass.Bass;
using static Un4seen.BassWasapi.BassWasapi;

namespace LedController.Bass
{
    public static class BassDriver
    {
        private static float[] fft;
        private static WASAPIPROC process;

        public static int CurrentDeviceIndex { get; private set; }
        public static string CurrentDeviceName { get { return BASS_WASAPI_GetDeviceInfo(CurrentDeviceIndex).name; } }
        public static BassDriverState State { get; private set; } = BassDriverState.Dormant;

        public static void Init()
        {
            fft = new float[1024];
            process = new WASAPIPROC(Process);

            BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
            bool result = BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            if (!result) throw new Exception("BASS init error");
            State = BassDriverState.Initialized;
        }

        public static void Enable(int devInd)
        {
            if (devInd > BASS_WASAPI_GetDeviceCount()) throw new InvalidOperationException("Invalid device index");
            if (State == BassDriverState.Dormant) Init();
            if (devInd != CurrentDeviceIndex)
            {
                CurrentDeviceIndex = devInd;
                Disable();
            }
            else if (State == BassDriverState.Enabled) return;

            bool result = BASS_WASAPI_Init(CurrentDeviceIndex, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, process, IntPtr.Zero);
            if (!result)
            {
                var error = BASS_ErrorGetCode();
                throw new Exception($"BASS Wasapi error: {error.ToString()}");
            }
            else
            {
                State = BassDriverState.Enabled;
            }
            BASS_WASAPI_Start();
        }
        public static void Disable()
        {
            if (State == BassDriverState.Enabled)
            {
                State = BassDriverState.Initialized;
                BASS_WASAPI_Stop(true);
                Free();
            }
        }

        public static List<byte> GetBands(int numBands, out short levelL, out short levelR)
        {
            if (State != BassDriverState.Enabled) throw new InvalidOperationException("Enable driver first");
            int LRLevel = BASS_WASAPI_GetLevel();
            if (LRLevel == -1)
            {
                var error = BASS_ErrorGetCode();
                throw new Exception($"BASS Wasapi error: {error.ToString()}");
            }
            else { Util.HighLow32(LRLevel, out levelR, out levelL); }

            int ret = BASS_WASAPI_GetData(fft, (int)BASSData.BASS_DATA_FFT2048);  
            if (ret < 0)
            {
                var error = BASS_ErrorGetCode();
                throw new Exception($"BASS Wasapi error: {error.ToString()}");
            }

            int x, y;
            int b0 = 0;
            List<byte> res = new List<byte>(numBands);

            for (x = 0; x < numBands; x++)
            {
                float peak = 0;
                int b1 = (int)Math.Pow(2, x * 10.0 / (numBands - 1));
                if (b1 > 1023) b1 = 1023;
                if (b1 <= b0) b1 = b0 + 1;
                for (; b0 < b1; b0++)
                {
                    if (peak < fft[1 + b0]) peak = fft[1 + b0];
                }
                y = (int)(Math.Sqrt(peak) * 3 * 255 - 4);
                if (y > 255) y = 255;
                if (y < 0) y = 0;
                res.Add((byte)y);
                //Console.Write("{0, 3} ", y);
            }

            return res;
        }

        private static int Process(IntPtr buffer, int length, IntPtr user)
        {
            return length;
        }

        public static void Free()
        {
            Disable();
            BASS_Free();
            BASS_WASAPI_Free();
            State = BassDriverState.Dormant;
        }
    }

    public enum BassDriverState
    {
        Dormant,
        Initialized,
        Enabled,
        Error
    }
}
