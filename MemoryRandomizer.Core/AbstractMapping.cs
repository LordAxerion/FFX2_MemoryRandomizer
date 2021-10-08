using FFX2MemoryReader;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    internal abstract class AbstractMapping<T>
    {
        protected const uint startofDresssphereSaves = 0xA00D1C; // 0x9FBD60; // in process (this - 0x4fbc = ProcessStart)
        protected const uint startOfGGSaves = 0xA00D14;

        protected readonly ProcessMemoryReader mReader;
        protected readonly Serializer mSerializer;

        internal IByteArrayHandler<T> byteArrayHandler;

        internal abstract List<Tuple<T, T>> MappingList { get; set; }
        internal abstract List<T> RandomizableItems { get; set; }

        protected AbstractMapping(ProcessMemoryReader mReader)
        {
            this.mReader = mReader;
            this.mSerializer = new Serializer();
        }

        internal abstract void Initiate();
        internal abstract void CreateMapping();
        internal abstract void InitiateTotalChaos();

        protected abstract void Save();
        protected abstract void WriteMemory(byte[] memoryBytesDS, byte[] memoryBytesGG);

        internal void Randomize(ref byte[] memoryBytesDS, ref byte[] memoryBytesGG)
        {
            // check byteArray for changes -> apply changes to mapping 
            this.byteArrayHandler.CheckReadBytes(ref memoryBytesDS, ref memoryBytesGG);
            // Write mapping data to memory
            this.byteArrayHandler.CreateByteArray(out byte[] newByteArrayDS, out byte[] newByteArrayGG);
            this.WriteMemory(newByteArrayDS, newByteArrayGG);
            // Write mapping data to save file
            this.Save();
        }

        internal void InitialShuffle(out byte[] newByteArrayDS, out byte[] newByteArrayGG)
        {
            // Write mapping data to memory
            this.byteArrayHandler.CreateByteArray(out newByteArrayDS, out newByteArrayGG);
            this.WriteMemory(newByteArrayDS, newByteArrayGG);
            // Write mapping data to save file
            this.Save();
        }
    }
}
