using FFX2MemoryReader;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public interface IMapping<T>
    {
        const uint startofDresssphereSaves = 0xA00D1C; // 0x9FBD60; // in process (this - 0x4fbc = ProcessStart)
        const uint startOfGGSaves = 0xA00D14;

        List<Tuple<T, T>> MappingList { get; set; }
        List<T> RandomizableItems { get; set; }

        void Initiate();
        void CreateMapping();
        void InitiateTotalChaos();
        void Randomize(ProcessMemoryReader mReader, ByteArrayHandler byteArrayHandler, byte[] memoryBytesDS, byte[] memoryBytesGG);
    }
}
