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
    public class BassDriver
    {
        private float[] fft;
        private WASAPIPROC process;

        public int DeviceIndex { get; private set; }
        public int NumBands { get; private set; }
        public BassDriverState State { get; private set; } = BassDriverState.Disabled;
        public BassDriver(int devInd, int numLines = 16)
        {
            DeviceIndex = devInd;
            NumBands = numLines;

            fft = new float[1024];
            process = new WASAPIPROC(Process);

            BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATETHREADS, false);
            bool result = BASS_Init(0, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            if (!result) throw new Exception("BASS init error");
        }

        public void Enable()
        {
            if (State != BassDriverState.Enabled)
            {
                bool result = BASS_WASAPI_Init(DeviceIndex, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, process, IntPtr.Zero);
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

        }
        public void Disable()
        {
            if (State == BassDriverState.Enabled)
            {
                State = BassDriverState.Disabled;
                BASS_WASAPI_Stop(true);
            }
        }

        public List<byte> GetBands(out short levelL, out short levelR)
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
            List<byte> res = new List<byte>(NumBands);

            for (x = 0; x < NumBands; x++)
            {
                float peak = 0;
                int b1 = (int)Math.Pow(2, x * 10.0 / (NumBands - 1));
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

        public void SetNumBands(int n)
        {
            Disable();
            NumBands = n;
            Enable();
        }

        public void SetDeviceIndex(int i)
        {
            Disable();
            DeviceIndex = i;
            Enable();
        }

        private int Process(IntPtr buffer, int length, IntPtr user)
        {
            return length;
        }

        public void Dispose()
        {
            Disable();
            BASS_Free();
            BASS_WASAPI_Free();
        }
    }

    public enum BassDriverState
    {
        Enabled,
        Disabled,
        Error
    }
}
