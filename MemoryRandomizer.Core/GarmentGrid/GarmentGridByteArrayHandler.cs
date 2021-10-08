using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    internal class GarmentGridByteArrayHandler : IByteArrayHandler<GarmentGrid>
    {
        private GarmentGridMapping mapping;

        internal GarmentGridByteArrayHandler(GarmentGridMapping garmentGridMapping)
        {
            this.mapping = garmentGridMapping;
        }


        public void CreateByteArray(out byte[] _, out byte[] newByteArrayGG)
        {
            _ = null;
            newByteArrayGG = new byte[0x8];

            foreach (Tuple<GarmentGrid, GarmentGrid> gg in mapping.MappingList)
            {
                if (gg.Item2.Available)
                {
                    var index = gg.Item2.Index;
                    var byteIndex = index / 8;
                    var bitIndex = index % 8;

                    var currentByte = newByteArrayGG[byteIndex];

                    var mask = 1 << (int)bitIndex;
                    newByteArrayGG[byteIndex] = (byte)(currentByte ^ (byte)mask);
                    // Console.WriteLine($"{ds.Item1.Name}, {ds.Item1.Count} -> {ds.Item2.Name}, {ds.Item2.Count}");
                }
            }
        }

        public void CheckReadBytes(ref byte[] _, ref byte[] readByteArrayGG)
        {
            foreach (var tuple in mapping.MappingList)
            {
                var byteIndex = tuple.Item2.Index / 8;
                var bitIndex = tuple.Item2.Index % 8;

                byte mask = (byte)(1 << (int)bitIndex);
                bool isSet = (readByteArrayGG[byteIndex] & mask) != 0;
                if (tuple.Item2.Available != isSet)
                {
                    mapping.MappingList[(int)tuple.Item2.Index].Item1.Available = isSet;
                    mapping.MappingList[(int)tuple.Item2.Index].Item2.Available = isSet;
                }
            }
        }
    }
}
