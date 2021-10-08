using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    internal class GarmentGridByteArrayHandler
    {
        private GarmentGridMapping mapping;

        internal GarmentGridByteArrayHandler(GarmentGridMapping garmentGridMapping)
        {
            this.mapping = garmentGridMapping;
        }


        internal void CreateByteArray(ref byte[] newByteArray)
        {
            foreach (Tuple<GarmentGrid, GarmentGrid> gg in mapping.MappingList)
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

        internal void CheckReadBytes(ref byte[] readByteArray)
        {
            foreach (var tuple in mapping.MappingList)
            {
                var byteIndex = tuple.Item2.Index / 8;
                var bitIndex = tuple.Item2.Index % 8;

                byte mask = (byte)(1 << (int)bitIndex);
                bool isSet = (readByteArray[byteIndex] & mask) != 0;
                if (tuple.Item2.Available != isSet)
                {
                    mapping.MappingList[(int)tuple.Item2.Index].Item1.Available = isSet;
                    mapping.MappingList[(int)tuple.Item2.Index].Item2.Available = isSet;
                }
            }
        }
    }
}
