using FFX2MemoryReader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace MemoryRandomizer.Core
{
    public class GameManager
    {
        const uint startofDresssphereSaves = 0xA00D1C; // 0x9FBD60; // in process (this - 0x4fbc = ProcessStart)
        const uint startOfGGSaves = 0xA00D14;
        const uint ptrOffset = 0x4fbc; // Process + 0x4fbc = Dressspheres
        const uint offset = 0;
        const string DresssphereSaveFileName = "DresssphereRando.txt";
        const string GGSaveFileName = "GGRando.txt";
        private static ProcessMemoryReader mReader;
        private static BinaryReader br;

        static string mode;

        private static bool initialReadDoneDS = false;
        private static bool initialReadDoneGG = false;
        private static byte[] newByteArrayDS = new byte[0x1E];
        private static byte[] newByteArrayGG = new byte[0x8];
        private static int bytesOutDS;
        private static int bytesOutGG;
        private static int bytesIn;

        private static Process mGameProcess;
        private static Serializer mSerializer = new Serializer();
        private static DresssphereMapping dm = new DresssphereMapping();
        private static GarmentGridMapping ggm = new GarmentGridMapping();

        public void Startup(bool randomizeDS, bool randomizeGG)
        {
            // Read save files
            if (randomizeDS)
            {
                initialReadDoneDS = SaveManager.ReadSaveFile(dm, DresssphereSaveFileName);
            }
            if (randomizeGG)
            {
                initialReadDoneGG = SaveManager.ReadSaveFile(ggm, GGSaveFileName);
            }

            // Attach to process
            FindAndOpenGameProcess();
            // Initial read
            DoInitialReadsAndShuffle();
            // Start monitoring
            Monitor(randomizeDS, randomizeGG);
        }

        private void Monitor(bool randomizeDS, bool randomizeGG)
        {
            while (true)
            {
                bytesOutDS = 0;
                bytesOutGG = 0;
                byte[] memoryBytesDs = mReader.ReadMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startofDresssphereSaves), 0x1E, out bytesOutDS);
                byte[] memoryBytesGG = mReader.ReadMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startOfGGSaves), 0x8, out bytesOutGG);
                if (bytesOutDS <= 0 || bytesOutGG <= 0)
                {
                    FindAndOpenGameProcess();
                }
                else
                {
                    if (randomizeDS)
                    {
                        // check byteArray for changes -> apply changes to mapping 
                        ByteArrayHandler.CheckReadBytesDS(memoryBytesDs);
                        // Write mapping data to memory
                        ByteArrayHandler.CreateByteArrayDS(newByteArrayDS);
                        int error = mReader.WriteMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startofDresssphereSaves), newByteArrayDS, out bytesIn);
                        if (error != 0)
                        {
                            Console.WriteLine($"Write Memory returned with error code {error}");
                            Console.WriteLine("Closing application...");
                            break;
                        }
                        mSerializer.SaveDresssphereMapping(DresssphereSaveFileName, dm.MappingList);
                    }

                    if (randomizeGG)
                    {
                        ByteArrayHandler.CheckReadBytesGG(memoryBytesGG);
                        newByteArrayGG = new byte[0x8];
                        ByteArrayHandler.CreateByteArrayGG(newByteArrayGG);

                        int error = mReader.WriteMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startOfGGSaves), newByteArrayGG, out bytesIn);
                        if (error != 0)
                        {
                            Console.WriteLine($"Write Memory returned with error code {error}");
                            Console.WriteLine("Closing application...");
                            break;
                        }
                        mSerializer.SaveDresssphereMapping(GGSaveFileName, ggm.MappingList);
                    }
                    Thread.Sleep(500);
                }
            }
        }

        #region process
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
        private static void FindAndOpenGameProcess()
        {
            mGameProcess = FindGameProcess();
            if (mGameProcess != null)
            {
                mReader = new ProcessMemoryReader(mGameProcess);
                mReader.OpenProcess();
            }
        }
        #endregion process

        private static void DoInitialReadsAndShuffle()
        {
            while (!initialReadDoneDS)
            {
                byte[] initialMemoryBytes = mReader.ReadMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startofDresssphereSaves), 0x1E, out bytesOutDS);
                if (bytesOutDS <= 0)
                {
                    FindAndOpenGameProcess();
                }
                else
                {
                    initialReadDoneDS = true;
                    dm.Initiate(initialMemoryBytes);
                    // Initiate randomization
                    Randomizer.Shuffle(DresssphereMapping.RandomizableDresspheres);
                    dm.CreateMapping();
                }
            }
            while (!initialReadDoneGG)
            {
                byte[] initialMemoryBytes = mReader.ReadMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startOfGGSaves), 0x8, out bytesOutGG);
                if (bytesOutGG <= 0)
                {
                    FindAndOpenGameProcess();
                }
                else
                {
                    initialReadDoneGG = true;
                    ggm.Initiate(initialMemoryBytes);
                    // Initiate randomization
                    Randomizer.Shuffle(GarmentGridMapping.RandomizedGarmentGrids);
                    ggm.CreateMapping();
                }
            }
        }
    }
}
