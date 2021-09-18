using FFX2MemoryReader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json;
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

        private static bool initialReadDone = false;
        private static byte[] newByteArray = new byte[0x1E];
        private static int bytesOut;
        private static int bytesIn;

        private static Process gameProcess;

        static void Main(string[] args)
        {
            // read save file
            try
            {
                string savedMapping = File.ReadAllText("Mapping.txt");
                DresssphereMapping.MappingList.Clear();
                DresssphereMapping.MappingList = JsonConvert.DeserializeObject<List<Tuple<Dresssphere, Dresssphere>>>(savedMapping);
                initialReadDone = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not read save file: {ex}");
                Console.WriteLine("Initiating new mapping...");
            }

            // Attach to process
            FindAndOpenGameProcess();

            // Initial read
            while (!initialReadDone)
            {
                byte[] initialMemoryBytes = mReader.ReadMemory((IntPtr)((uint)gameProcess.Modules[0].BaseAddress + startofDresssphereSaves), 0x1E, out bytesOut);
                if (bytesOut <= 0)
                {
                    FindAndOpenGameProcess();
                }
                else
                {
                    initialReadDone = true;
                    InitiateDressspheres(initialMemoryBytes);
                    // Initiate randomization
                    Randomizer.Shuffle(DresssphereMapping.RandomizableDresspheres);
                    DresssphereMapping.CreateMapping();
                }
            }

            // start monitoring
            while (true)
            {
                bytesOut = 0;
                byte[] memoryBytes = mReader.ReadMemory((IntPtr)((uint)gameProcess.Modules[0].BaseAddress + startofDresssphereSaves), 0x1E, out bytesOut);
                if (bytesOut <= 0)
                {
                    FindAndOpenGameProcess();
                }
                else
                {
                    // check byteArray for changes -> apply changes to mapping 
                    CheckReadBytes(memoryBytes);

                    // Write mapping data to memory
                    CreateByteArray(newByteArray);
                    int error = mReader.WriteMemory((IntPtr)((uint)gameProcess.Modules[0].BaseAddress + startofDresssphereSaves), newByteArray, out bytesIn);
                    if (error != 0)
                    {
                        Console.WriteLine($"Write Memory returned with error code {error}");
                        Console.WriteLine("Closing application...");
                        break;
                    }
                    SaveMapping();
                    Thread.Sleep(500);
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

        private static void CreateByteArray(byte[] newByteArray)
        {
            foreach (Tuple<Dresssphere, Dresssphere> ds in DresssphereMapping.MappingList)
            {
                newByteArray[ds.Item2.Index] = Convert.ToByte(ds.Item2.Count);
                // Console.WriteLine($"{ds.Item1.Name}, {ds.Item1.Count} -> {ds.Item2.Name}, {ds.Item2.Count}");
            }
        }

        private static void InitiateDressspheres(byte[] initialByteArray)
        {
            int i = 0;
            foreach (byte b in initialByteArray)
            {
                DresssphereMapping.Dresspheres[i].Count = Convert.ToUInt32(initialByteArray[i]);
                i++;
            }
        }

        private static void CheckReadBytes(byte[] readByteArray)
        {
            foreach(var tuple in DresssphereMapping.MappingList)
            {
                uint newCount = readByteArray[tuple.Item2.Index];
                if (tuple.Item2.Count != newCount)
                {
                    DresssphereMapping.MappingList[(int)tuple.Item2.Index].Item1.Count = newCount;
                    DresssphereMapping.MappingList[(int)tuple.Item2.Index].Item2.Count = newCount;
                }
            }
        }

        private static void SaveMapping()
        {
            var jsonString = JsonConvert.SerializeObject(DresssphereMapping.MappingList);
            File.WriteAllText("Mapping.txt", jsonString);
        }

        private static void FindAndOpenGameProcess()
        {
            gameProcess = FindGameProcess();
            if (gameProcess != null)
            {
                mReader = new ProcessMemoryReader(gameProcess);
                mReader.OpenProcess();
            }
        }
    }
}
