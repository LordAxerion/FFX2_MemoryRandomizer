using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    internal class RandomizableByteArrayHandler
    {
        private RandomizableItemMapping mapping;

        internal RandomizableByteArrayHandler(RandomizableItemMapping randomizableItemMapping)
        {
            this.mapping = randomizableItemMapping;
        }

        internal void CreateByteArray(ref byte[] newByteArrayDS, ref byte[] newByteArrayGG)
        {
            foreach (Tuple<RandomizableItem, RandomizableItem> tuple in mapping.MappingList)
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

        internal void CheckReadBytes(ref byte[] readByteArrayDS, ref byte[] readByteArrayGG)
        {
            foreach (var tuple in mapping.MappingList)
            {
                Console.WriteLine($"{tuple.Item1.Name}, {tuple.Item1.Count} -> {tuple.Item2.Name}, {tuple.Item2.Count}");
                if (tuple.Item2.ItemType == RandoItemType.Dresssphere)
                {
                    int newCount = readByteArrayDS[tuple.Item2.Index];
                    bool gotIt = newCount > 0;
                    if (tuple.Item2.GotIt != gotIt)
                    {
                        mapping.MappingList[tuple.Item2.Index + 64].Item1.Count = newCount;
                        mapping.MappingList[tuple.Item2.Index + 64].Item2.Count = newCount;
                        mapping.MappingList[tuple.Item2.Index + 64].Item1.GotIt = gotIt;
                        mapping.MappingList[tuple.Item2.Index + 64].Item2.GotIt = gotIt;
                    }
                }
                else
                {
                    byte mask = (byte)(1 << tuple.Item2.BitIndex);
                    bool isSet = (readByteArrayGG[tuple.Item2.ByteIndex] & mask) != 0;
                    if (tuple.Item2.GotIt != isSet)
                    {
                        mapping.MappingList[tuple.Item2.Index].Item1.GotIt = isSet;
                        mapping.MappingList[tuple.Item2.Index].Item2.GotIt = isSet;
                        mapping.MappingList[tuple.Item2.Index].Item1.Count = 1;
                        mapping.MappingList[tuple.Item2.Index].Item2.Count = 1;
                    }
                }
                Console.WriteLine($"{tuple.Item1.Name}, {tuple.Item1.Count} -> {tuple.Item2.Name}, {tuple.Item2.Count}");
            }
        }
    }
}
