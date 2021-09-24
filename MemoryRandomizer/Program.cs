using FFX2MemoryReader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MemoryRandomizer
{
    class Program
    {
        const uint startofDresssphereSaves = 0xA00D1C; // 0x9FBD60; // in process (this - 0x4fbc = ProcessStart)
        const uint startOfGGSaves = 0xA00D14;
        const uint startOfAbilitySaves = 0xA022A0;
        const uint ptrOffset = 0x4fbc; // Process + 0x4fbc = Dressspheres
        const uint offset = 0;
        const string DresssphereSaveFileName = "DresssphereRando.txt";
        const string GGSaveFileName = "GGRando.txt";
        const string BothSaveFileName = "FullRando.txt";
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

        private static bool randomizeDS = false;
        private static bool randomizeGG = false;
        private static bool randomizeBoth = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Randomize and mix up Dressspheres and GarmentGrids? y/n");
            var keyIn = Console.ReadKey();
            if (keyIn.Key == ConsoleKey.Y)
            {
                randomizeBoth = true;
            }
            if (!randomizeBoth)
            {
                Console.WriteLine("Randomize Dresspheres? y/n");
                keyIn = Console.ReadKey();
                if (keyIn.Key == ConsoleKey.Y)
                {
                    randomizeDS = true;
                }
                Console.WriteLine("Randomize GarmentGrids? y/n");
                keyIn = Console.ReadKey();
                if (keyIn.Key == ConsoleKey.Y)
                {
                    randomizeGG = true;
                }
            }

            // read save file
            ReadSaveFiles();
            // Attach to process
            FindAndOpenGameProcess();
            // Initial read
            DoInitialReadsAndShuffle();

            // start monitoring
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
                        // check byteArray for changes -> apply changes to mapping 
                        CheckReadBytesBoth(memoryBytesDs, memoryBytesGG);
                        // Write mapping data to memory
                        newByteArrayGG = new byte[0x8];
                        CreateByteArrayBoth(newByteArrayDS, newByteArrayGG);
                        int error = mReader.WriteMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startofDresssphereSaves), newByteArrayDS, out bytesIn);
                        int error2 = mReader.WriteMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startOfGGSaves), newByteArrayGG, out bytesIn);
                        if (error != 0 || error2 != 0)
                        {
                            Console.WriteLine($"Write Memory returned with error code {error}");
                            Console.WriteLine("Closing application...");
                            break;
                        }
                        mSerializer.SaveMapping(BothSaveFileName, RandomizableItemMapping.MappingList);
                    }
                    if (randomizeDS && !randomizeBoth)
                    {
                        // check byteArray for changes -> apply changes to mapping 
                        CheckReadBytesDS(memoryBytesDs);
                        // Write mapping data to memory
                        CreateByteArrayDS(newByteArrayDS);
                        int error = mReader.WriteMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startofDresssphereSaves), newByteArrayDS, out bytesIn);
                        if (error != 0)
                        {
                            Console.WriteLine($"Write Memory returned with error code {error}");
                            Console.WriteLine("Closing application...");
                            break;
                        }
                        mSerializer.SaveMapping(DresssphereSaveFileName, DresssphereMapping.MappingList);
                    }

                    if (randomizeGG && !randomizeBoth)
                    {
                        CheckReadBytesGG(memoryBytesGG);
                        newByteArrayGG = new byte[0x8];
                        CreateByteArrayGG(newByteArrayGG);

                        int error = mReader.WriteMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startOfGGSaves), newByteArrayGG, out bytesIn);
                        if (error != 0)
                        {
                            Console.WriteLine($"Write Memory returned with error code {error}");
                            Console.WriteLine("Closing application...");
                            break;
                        }
                        mSerializer.SaveMapping(GGSaveFileName, GarmentGridMapping.MappingList);
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

        #region bytearray_handling
        private static void CreateByteArrayDS(byte[] newByteArray)
        {
            foreach (Tuple<Dresssphere, Dresssphere> ds in DresssphereMapping.MappingList)
            {
                newByteArray[ds.Item2.Index] = Convert.ToByte(ds.Item2.Count);
                // Console.WriteLine($"{ds.Item1.Name}, {ds.Item1.Count} -> {ds.Item2.Name}, {ds.Item2.Count}");
            }
        }

        private static void CreateByteArrayBoth(byte[] newByteArrayDS, byte[] newByteArrayGG)
        {
            foreach (Tuple<RandomizableItem, RandomizableItem> tuple in RandomizableItemMapping.MappingList)
            {
                if (tuple.Item2.ItemType == RandoItemType.Dresssphere)
                {
                    newByteArrayDS[tuple.Item2.Index] = Convert.ToByte(tuple.Item2.Count);
                }
                else if(tuple.Item2.GotIt)
                {
                    var currentByte = newByteArrayGG[tuple.Item2.ByteIndex];
                    var mask = 1 << tuple.Item2.BitIndex;
                    newByteArrayGG[tuple.Item2.ByteIndex] = (byte)(currentByte ^ (byte)mask);
                }
                Console.WriteLine($"{tuple.Item1.Name}, {tuple.Item1.Count} -> {tuple.Item2.Name}, {tuple.Item2.Count}");
            }
        }

        private static void CreateByteArrayGG(byte[] newByteArray)
        {
            foreach (Tuple<GarmentGrid, GarmentGrid> gg in GarmentGridMapping.MappingList)
            {
                if (gg.Item2.Available)
                {
                    var index = gg.Item2.Index;
                    var byteIndex = index / 8;
                    var bitIndex = index % 8;

                    var currentByte = newByteArray[byteIndex];

                    var mask = 1 << (int)bitIndex;
                    newByteArray[byteIndex] = (byte)(currentByte ^ (byte)mask);
                    // Console.WriteLine($"{ds.Item1.Name}, {ds.Item1.Count} -> {ds.Item2.Name}, {ds.Item2.Count}");
                }
            }
        }

        private static void CheckReadBytesDS(byte[] readByteArray)
        {
            foreach(var tuple in DresssphereMapping.MappingList)
            {
                uint newCount = readByteArray[tuple.Item2.Index];
                if (tuple.Item2.Count != newCount)
                {
                    int diff = (int)(newCount - tuple.Item2.Count);
                    DresssphereMapping.MappingList[(int)tuple.Item2.Index].Item1.Count += (uint)diff;
                    DresssphereMapping.MappingList[(int)tuple.Item2.Index].Item2.Count += (uint)diff;
                }
            }
        }

        private static void CheckReadBytesBoth(byte[] readByteArrayDS, byte[] readByteArrayGG)
        {
            foreach (var tuple in RandomizableItemMapping.MappingList)
            {
                Console.WriteLine($"{tuple.Item1.Name}, {tuple.Item1.Count} -> {tuple.Item2.Name}, {tuple.Item2.Count}");
                if (tuple.Item2.ItemType == RandoItemType.Dresssphere)
                {
                    int newCount = readByteArrayDS[tuple.Item2.Index];
                    bool gotIt = newCount > 0;
                    if (tuple.Item2.GotIt != gotIt)
                    {
                        RandomizableItemMapping.MappingList[tuple.Item2.Index + 64].Item1.Count = newCount;
                        RandomizableItemMapping.MappingList[tuple.Item2.Index + 64].Item2.Count = newCount;
                        RandomizableItemMapping.MappingList[tuple.Item2.Index + 64].Item1.GotIt = gotIt;
                        RandomizableItemMapping.MappingList[tuple.Item2.Index + 64].Item2.GotIt = gotIt;
                    }
                }   
                else
                {
                    byte mask = (byte)(1 << tuple.Item2.BitIndex);
                    bool isSet = (readByteArrayGG[tuple.Item2.ByteIndex] & mask) != 0;
                    if (tuple.Item2.GotIt != isSet)
                    {
                        RandomizableItemMapping.MappingList[tuple.Item2.Index].Item1.GotIt = isSet;
                        RandomizableItemMapping.MappingList[tuple.Item2.Index].Item2.GotIt = isSet;
                        RandomizableItemMapping.MappingList[tuple.Item2.Index].Item1.Count = 1;
                        RandomizableItemMapping.MappingList[tuple.Item2.Index].Item2.Count = 1;
                    }
                }
                Console.WriteLine($"{tuple.Item1.Name}, {tuple.Item1.Count} -> {tuple.Item2.Name}, {tuple.Item2.Count}");
            }
        }

        private static void CheckReadBytesGG(byte[] readByteArray)
        {
            foreach (var tuple in GarmentGridMapping.MappingList)
            {
                var byteIndex = tuple.Item2.Index / 8;
                var bitIndex = tuple.Item2.Index % 8;

                byte mask = (byte)(1 << (int)bitIndex);
                bool isSet = (readByteArray[byteIndex] & mask) != 0;
                if (tuple.Item2.Available != isSet)
                {
                    GarmentGridMapping.MappingList[(int)tuple.Item2.Index].Item1.Available = isSet;
                    GarmentGridMapping.MappingList[(int)tuple.Item2.Index].Item2.Available = isSet;
                }
            }
        }
        #endregion bytearray_handling

        private static void ReadSaveFiles()
        {
            try
            {
                if (randomizeBoth)
                {
                    RandomizableItemMapping.MappingList.Clear();
                    RandomizableItemMapping.MappingList = mSerializer.LoadMapping<List<Tuple<RandomizableItem, RandomizableItem>>>(BothSaveFileName);
                    initialReadDoneBoth = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not read Dresssphere save file {BothSaveFileName}");
                Console.WriteLine("Initiating new mapping...");
            }
            try
            {
                if (randomizeDS)
                {
                    DresssphereMapping.MappingList.Clear();
                    DresssphereMapping.MappingList = mSerializer.LoadMapping<List<Tuple<Dresssphere, Dresssphere>>>(DresssphereSaveFileName);
                    initialReadDoneDS = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not read Dresssphere save file {DresssphereSaveFileName}");
                Console.WriteLine("Initiating new mapping...");
            }
            try
            {
                if (randomizeGG)
                {
                    GarmentGridMapping.MappingList.Clear();
                    GarmentGridMapping.MappingList = mSerializer.LoadMapping<List<Tuple<GarmentGrid, GarmentGrid>>>(GGSaveFileName);
                    initialReadDoneGG = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not read save file {GGSaveFileName}");
                Console.WriteLine("Initiating new mapping...");
            }

        }

        private static void DoInitialReadsAndShuffle()
        {
            while(randomizeBoth && !initialReadDoneBoth)
            {
                byte[] initialMemoryBytesDS = mReader.ReadMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startofDresssphereSaves), 0x1E, out bytesOutDS);
                byte[] initialMemoryBytesGG = mReader.ReadMemory((IntPtr)((uint)mGameProcess.Modules[0].BaseAddress + startOfGGSaves), 0x8, out bytesOutGG);
                if(bytesOutDS <= 0 || bytesOutGG <= 0)
                {
                    FindAndOpenGameProcess();
                }
                else
                {
                    initialReadDoneBoth = true;
                    RandomizableItemMapping.InitiateDressspheres(initialMemoryBytesDS, initialMemoryBytesGG);
                    Randomizer.Shuffle(RandomizableItemMapping.RandomizableItems);
                    RandomizableItemMapping.CreateMapping();
                }
            }
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
                    DresssphereMapping.InitiateDressspheres(initialMemoryBytes);
                    // Initiate randomization
                    Randomizer.Shuffle(DresssphereMapping.RandomizableDresspheres);
                    DresssphereMapping.CreateMapping();
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
                    GarmentGridMapping.InitiateGarmentGrids(initialMemoryBytes);
                    // Initiate randomization
                    Randomizer.Shuffle(GarmentGridMapping.RandomizedGarmentGrids);
                    GarmentGridMapping.CreateMapping();
                }
            }
        }
    }
}
