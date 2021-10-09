using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    internal class RIByteArrayHandler : IByteArrayHandler<RandomizableItem>
    {
        private RandomizableItemMapping riMapping;

        internal RIByteArrayHandler(RandomizableItemMapping randomizableItemMapping)
        {
            this.riMapping = randomizableItemMapping;
        }

        public void CreateByteArray(out byte[] newByteArrayDS, out byte[] newByteArrayGG)
        {
            newByteArrayDS = new byte[0x1E];
            newByteArrayGG = new byte[0x8];

            foreach (Tuple<RandomizableItem, RandomizableItem> tuple in riMapping.MappingList)
            {
                if (tuple.Item2.ItemType == RandoItemType.Dresssphere)
                {
                    newByteArrayDS[tuple.Item2.Index] = Convert.ToByte(tuple.Item2.Count);
                }
                else if (tuple.Item2.GotIt)
                {
                    var currentByte = newByteArrayGG[tuple.Item2.ByteIndex];
                    var mask = 1 << tuple.Item2.BitIndex;
                    newByteArrayGG[tuple.Item2.ByteIndex] = (byte)(currentByte ^ (byte)mask);
                }
                Console.WriteLine($"{tuple.Item1.Name}, {tuple.Item1.Count} -> {tuple.Item2.Name}, {tuple.Item2.Count}");
            }
        }

        public void CheckReadBytes(ref byte[] readByteArrayDS, ref byte[] readByteArrayGG)
        {
            foreach (var tuple in riMapping.MappingList)
            {
                Console.WriteLine($"{tuple.Item1.Name}, {tuple.Item1.Count} -> {tuple.Item2.Name}, {tuple.Item2.Count}");
                if (tuple.Item2.ItemType == RandoItemType.Dresssphere)
                {
                    int newCount = readByteArrayDS[tuple.Item2.Index];
                    bool gotIt = newCount > 0;
                    if (tuple.Item2.GotIt != gotIt)
                    {
                        riMapping.MappingList[tuple.Item2.Index + 64].Item1.Count = newCount;
                        riMapping.MappingList[tuple.Item2.Index + 64].Item2.Count = newCount;
                        riMapping.MappingList[tuple.Item2.Index + 64].Item1.GotIt = gotIt;
                        riMapping.MappingList[tuple.Item2.Index + 64].Item2.GotIt = gotIt;
                    }
                }
                else
                {
                    byte mask = (byte)(1 << tuple.Item2.BitIndex);
                    bool isSet = (readByteArrayGG[tuple.Item2.ByteIndex] & mask) != 0;
                    if (tuple.Item2.GotIt != isSet)
                    {
                        riMapping.MappingList[tuple.Item2.Index].Item1.GotIt = isSet;
                        riMapping.MappingList[tuple.Item2.Index].Item2.GotIt = isSet;
                        riMapping.MappingList[tuple.Item2.Index].Item1.Count = 1;
                        riMapping.MappingList[tuple.Item2.Index].Item2.Count = 1;
                    }
                }
                Console.WriteLine($"{tuple.Item1.Name}, {tuple.Item1.Count} -> {tuple.Item2.Name}, {tuple.Item2.Count}");
            }
        }
    }
}
