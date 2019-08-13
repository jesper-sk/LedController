using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Un4seen.Bass;
using Un4seen.BassWasapi;
using static Un4seen.BassWasapi.BassWasapi;
using static Un4seen.Bass.Bass;

namespace BassApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = BASS_WASAPI_GetDeviceCount();
            int defind = 0;
            Console.WriteLine("count: {0}", count);
            for(int i = 0; i < count; i++)
            {
                BASS_WASAPI_DEVICEINFO info = BASS_WASAPI_GetDeviceInfo(i);
                Console.WriteLine(info.name);
                Console.WriteLine($"\tIsLoopback: {info.IsLoopback}");
                Console.WriteLine($"\tSupportsRecording: {info.SupportsRecording}");
                Console.WriteLine($"\tIsDefault: {info.IsDefault}\n");
                Console.WriteLine($"\tid: {info.id}");
                if (info.IsDefault) defind = i;
            }
            Console.ReadKey();

            int Process(IntPtr buffer, int length, IntPtr user) { return length; }
            WASAPIPROC process = new WASAPIPROC(Process);
            BASS_Init(-1, 44100, 0, IntPtr.Zero);
            BASS_WASAPI_Init(defind, 0, 0, BASSWASAPIInit.BASS_WASAPI_BUFFER, 1f, 0.05f, process, IntPtr.Zero);

            BASS_Free();
            BASS_WASAPI_Free();
        }
    }
}
