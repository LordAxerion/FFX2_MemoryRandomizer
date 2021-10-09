using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    internal class GGByteArrayHandler : IByteArrayHandler<GarmentGrid>
    {
        private GarmentGridMapping ggMapping;

        internal GGByteArrayHandler(GarmentGridMapping garmentGridMapping)
        {
            this.ggMapping = garmentGridMapping;
        }

        public void CreateByteArray(out byte[] _, out byte[] newByteArray)
        {
            _ = null;
            newByteArray = new byte[0x8];

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

        public void CheckReadBytes(ref byte[] _, ref byte[] readByteArray)
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
