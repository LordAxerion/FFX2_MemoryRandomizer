using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    internal class ByteArrayHandler
    {
        private static DresssphereMapping dresssphereMapping;
        private static GarmentGridMapping ggMapping;

        internal ByteArrayHandler(DresssphereMapping dresssphereMapping, GarmentGridMapping garmentGridMapping)
        {
            ByteArrayHandler.dresssphereMapping = dresssphereMapping;
            ByteArrayHandler.ggMapping = garmentGridMapping;
        }

        internal static void CreateByteArrayDS(byte[] newByteArray)
        {
            foreach (Tuple<Dresssphere, Dresssphere> ds in dresssphereMapping.MappingList)
            {
                newByteArray[ds.Item2.Index] = Convert.ToByte(ds.Item2.Count);
                // Console.WriteLine($"{ds.Item1.Name}, {ds.Item1.Count} -> {ds.Item2.Name}, {ds.Item2.Count}");
            }
        }

        internal static void CreateByteArrayGG(byte[] newByteArray)
        {
            foreach (Tuple<GarmentGrid, GarmentGrid> gg in ggMapping.MappingList)
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

        internal static void CheckReadBytesDS(byte[] readByteArray)
        {
            foreach (var tuple in dresssphereMapping.MappingList)
            {
                uint newCount = readByteArray[tuple.Item2.Index];
                if (tuple.Item2.Count != newCount)
                {
                    dresssphereMapping.MappingList[(int)tuple.Item2.Index].Item1.Count = newCount;
                    dresssphereMapping.MappingList[(int)tuple.Item2.Index].Item2.Count = newCount;
                }
            }
        }

        internal static void CheckReadBytesGG(byte[] readByteArray)
        {
            foreach (var tuple in ggMapping.MappingList)
            {
                var byteIndex = tuple.Item2.Index / 8;
                var bitIndex = tuple.Item2.Index % 8;

                byte mask = (byte)(1 << (int)bitIndex);
                bool isSet = (readByteArray[byteIndex] & mask) != 0;
                if (tuple.Item2.Available != isSet)
                {
                    ggMapping.MappingList[(int)tuple.Item2.Index].Item1.Available = isSet;
                    ggMapping.MappingList[(int)tuple.Item2.Index].Item2.Available = isSet;
                }
            }
        }
    }
}
