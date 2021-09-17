using FFX2MemoryReader;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MemoryRandomizer
{
    class Program
    {
        const uint startofDresssphereSaves = 0xA00D1C; // 0x9FBD60; // in process (this - 0x4fbc = ProcessStart)
        const uint ptrOffset = 0x4fbc; // Process + 0x4fbc = Dressspheres
        const uint offset = 0;
        private static ProcessMemoryReader mReader;
        private static BinaryReader br;

        static string mode;

        static void Main(string[] args)
        {
            Process gameProcess = FindGameProcess();
            if (gameProcess != null)
            {
                mReader = new ProcessMemoryReader(gameProcess);
                mReader.OpenProcess();

                // Initial read
                int bytesOut;
                byte[] memoryBytes = mReader.ReadMemory((IntPtr)((uint)gameProcess.Modules[0].BaseAddress + startofDresssphereSaves), 0x1C, out bytesOut);
                if (bytesOut <= 0)
                {
                    gameProcess = FindGameProcess();
                    if (gameProcess != null)
                    {
                        mReader = new ProcessMemoryReader(gameProcess);
                        mReader.OpenProcess();
                    }
                }
                while (true)
                {
                    bytesOut = 0;
                    byte[] memoryBytes = mReader.ReadMemory((IntPtr)((uint)gameProcess.Modules[0].BaseAddress + startofDresssphereSaves), 0x1C, out bytesOut);
                    if (bytesOut <= 0)
                    {
                        gameProcess = FindGameProcess();
                        if (gameProcess != null)
                        {
                            mReader = new ProcessMemoryReader(gameProcess);
                            mReader.OpenProcess();
                        }
                    }
                    else
                    {

                    }
                }
            }
        }

        private static Process FindGameProcess()
        {
            Process[] processes;
            do
            {
                processes = Process.GetProcessesByName("FFX-2");
                if (processes.Length > 0)
                {
                    return processes[0];
                }
                else
                {
                    Console.WriteLine("Process not found, please start the game.");
                    Thread.Sleep(500);
                }
            } while (processes.Length < 1);
            return null;
        }
    }
}
