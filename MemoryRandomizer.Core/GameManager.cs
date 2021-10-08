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
        private static ProcessMemoryReader mReader;
        private static BinaryReader br;

        static string mode;

        private static bool initialReadDoneDS = false;
        private static bool initialReadDoneGG = false;
        private static bool initialReadDoneBoth = false;
        private static byte[] newByteArrayDS = new byte[0x1E];
        private static byte[] newByteArrayGG = new byte[0x8];
        private static int bytesOutDS;
        private static int bytesOutGG;
        private static int bytesIn;

        private static Process mGameProcess;
        private static Serializer mSerializer = new Serializer();
        private static DresssphereMapping dm;
        private static GarmentGridMapping ggm;
        private static RandomizableItemMapping rim;

        /// <summary>
        ///     Starts the randomization process.
        /// </summary>
        /// <param name="randomizeDS"></param>
        /// <param name="randomizeGG"></param>
        /// <param name="randomizeBoth"></param>
        public void Startup(bool randomizeDS, bool randomizeGG, bool randomizeBoth, bool randomizeDSTC, bool loadDS = true, bool loadGG = true, bool loadBoth = true, bool loadChaosDS = false)
        {
            // Read save files
            if (randomizeBoth && loadBoth)
            {
                rim = new RandomizableItemMapping(mReader, newByteArrayDS, newByteArrayGG);
                initialReadDoneBoth = SaveManager.ReadSaveFile(rim, SaveManager.GGSaveFileName);
            }
            if (randomizeDS && loadDS)
            {
                dm = new DresssphereMapping(mReader, newByteArrayDS);
                initialReadDoneDS = SaveManager.ReadSaveFile(dm, SaveManager.DresssphereSaveFileName);
            }
            if (randomizeGG && loadGG)
            {
                ggm = new GarmentGridMapping(mReader, newByteArrayGG);
                initialReadDoneGG = SaveManager.ReadSaveFile(ggm, SaveManager.GGSaveFileName);
            }

            // Attach to process
            FindAndOpenGameProcess();

            // Initial read
            DoInitialReadsAndShuffle(randomizeBoth, randomizeDSTC, loadChaosDS);
            // We need to write after the Initial read and shuffle, or the Mapping will not fit the  current state
            DoInitialWrite(randomizeBoth, randomizeDSTC, randomizeDS, randomizeGG);

            // Start monitoring
            try
            {
                Monitor(randomizeDS, randomizeGG, randomizeBoth, randomizeDSTC);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.WriteLine("Closing application...");
                throw exc;
            }
        }

        private void Monitor(bool randomizeDS, bool randomizeGG, bool randomizeBoth, bool randomizeDSTotalChaos)
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
                    if (randomizeBoth)
                    {
                        rim.Randomize(ref memoryBytesDs, ref memoryBytesGG);
                    }                    
                    else if (randomizeDSTotalChaos)
                    {
                        ((DresssphereByteArrayHandler)dm.byteArrayHandler).CheckReadBytesTotalChaos(memoryBytesDs);
                        ((DresssphereByteArrayHandler)dm.byteArrayHandler).CreateByteArrayTotalChaos(ref newByteArrayDS);
                        int error = mReader.WriteMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startofDresssphereSaves), newByteArrayDS, out bytesIn);
                        mReader.CheckError(error);
                    }
                    else if (randomizeDS)
                    {
                        dm.Randomize(ref memoryBytesDs, ref memoryBytesGG);
                    }

                    if (randomizeGG && !randomizeBoth)
                    {
                        ggm.Randomize(ref memoryBytesDs, ref memoryBytesGG);
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


        private void DoInitialReadsAndShuffle(bool randomizeBoth, bool totalChaos, bool loadDSTC)
        {
            while (randomizeBoth && !initialReadDoneBoth)
            {
                byte[] initialMemoryBytesDS = mReader.ReadMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startofDresssphereSaves), 0x1E, out bytesOutDS);
                byte[] initialMemoryBytesGG = mReader.ReadMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startOfGGSaves), 0x8, out bytesOutGG);

                rim = new RandomizableItemMapping(mReader, initialMemoryBytesDS, initialMemoryBytesGG);
                initialReadDoneBoth = DoInitialShuffle(rim, bytesOutDS <= 0 || bytesOutGG <= 0);

            }
            while (!initialReadDoneDS)
            {
                byte[] initialMemoryBytes = mReader.ReadMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startofDresssphereSaves), 0x1E, out bytesOutDS);

                dm = new DresssphereMapping(mReader, initialMemoryBytes);
                if (totalChaos)
                {
                    initialReadDoneDS = DoInitiateOnly(dm, bytesOutDS <= 0, loadDSTC);
                }
                else
                {
                    initialReadDoneDS = DoInitialShuffle(dm, bytesOutDS <= 0);
                }
            }
            while (!initialReadDoneGG)
            {
                byte[] initialMemoryBytes = mReader.ReadMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startOfGGSaves), 0x8, out bytesOutGG);

                ggm = new GarmentGridMapping(mReader, initialMemoryBytes);
                initialReadDoneGG = DoInitialShuffle(ggm, bytesOutGG <= 0);
            }
        }

        private static bool DoInitialShuffle<T>(AbstractMapping<T> mapping, bool readSuccessful)
        {
            if (readSuccessful)
            {
                FindAndOpenGameProcess();

                return false;
            }
            else
            {
                mapping.Initiate();
                // Initiate randomization
                Randomizer.Shuffle(mapping.RandomizableItems);
                mapping.CreateMapping();

                return true;
            }
        }
        private static bool DoInitiateOnly<T>(AbstractMapping<T> mapping, bool readSuccessful, bool loadDSTC) where T : Dresssphere
        {
            if (readSuccessful)
            {
                FindAndOpenGameProcess();
                return false;
            }
            else if (loadDSTC)
            {
                mapping.InitiateTotalChaos();
            }
            return true;
        }

        private void DoInitialWrite(bool randomizeBoth, bool randomizeDSTC, bool randomizeDS, bool randomizeGG)
        {
            if (randomizeBoth)
            {
                rim.InitialShuffle(out newByteArrayDS, out newByteArrayGG);
            }
            else if (randomizeDSTC)
            {
                ((DresssphereByteArrayHandler) dm.byteArrayHandler).CreateByteArrayTotalChaos(ref newByteArrayDS);
                int error = mReader.WriteMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startofDresssphereSaves), newByteArrayDS, out bytesIn);
                mReader.CheckError(error);
            }
            else if (randomizeDS)
            {
                dm.InitialShuffle(out newByteArrayDS, out _);
            }

            if (randomizeGG && !randomizeBoth)
            {
                ggm.InitialShuffle(out _, out newByteArrayGG);
            }
        }
    }
}
