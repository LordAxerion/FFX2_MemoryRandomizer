using FFX2MemoryReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MemoryRandomizer.Core
{
    public abstract class AbstractMapping<T> where T : IRandomizable
    {
        protected const uint startofDresssphereSaves = 0xA00D1C; // 0x9FBD60; // in process (this - 0x4fbc = ProcessStart)
        protected const uint startOfGGSaves = 0xA00D14;

        protected readonly ProcessMemoryReader mReader;
        protected readonly Serializer mSerializer;

        internal abstract string SaveFile { get; }
        internal List<Tuple<T, T>> MappingList { get; set; }
        internal abstract List<T> RandomizableItems { get; set; }
        private protected IByteArrayHandler<T> ByteArrayHandler { get; set; }

        protected AbstractMapping(ProcessMemoryReader mReader)
        {
            this.mReader = mReader;
            this.mSerializer = new Serializer();
            this.MappingList = new List<Tuple<T, T>>();
        }

        internal abstract void Initiate();
        internal abstract void CreateMapping();
        internal abstract void InitiateTotalChaos();

        protected abstract int WriteMemory(byte[] memoryBytesDS, byte[] memoryBytesGG);

        internal void Randomize(ref byte[] memoryBytesDS, ref byte[] memoryBytesGG)
        {
            // check byteArray for changes -> apply changes to mapping 
            this.ByteArrayHandler.CheckReadBytes(ref memoryBytesDS, ref memoryBytesGG);
            // Write mapping data to memory
            this.ByteArrayHandler.CreateByteArray(out byte[] newByteArrayDS, out byte[] newByteArrayGG);
            this.WriteMemory(newByteArrayDS, newByteArrayGG);
            // Write mapping data to save file
            this.mSerializer.SaveMapping(this.SaveFile, this.MappingList);
        }

        internal void InitialWrite()
        {
            // Write mapping data to memory
            this.ByteArrayHandler.CreateByteArray(out byte[] newByteArrayDS, out byte[] newByteArrayGG);
            this.WriteMemory(newByteArrayDS, newByteArrayGG);
            // Write mapping data to save file
            this.mSerializer.SaveMapping(this.SaveFile, this.MappingList);
        }

        protected void CheckError(params int[] errors)
        {
            foreach (int error in errors)
            {
                if (error != 0)
                {
                    throw new IOException($"Write Memory returned with error code {error}");
                }
            }
        }
    }
}
