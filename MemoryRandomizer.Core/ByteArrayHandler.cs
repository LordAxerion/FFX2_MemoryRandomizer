using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    internal class ByteArrayHandler
    {
        private DresssphereMapping dresssphereMapping;
        private GarmentGridMapping ggMapping;
        private RandomizableItemMapping riMapping;

        internal ByteArrayHandler(DresssphereMapping dresssphereMapping, GarmentGridMapping garmentGridMapping, RandomizableItemMapping randomizableItemMapping)
        {
            this.dresssphereMapping = dresssphereMapping;
            this.ggMapping = garmentGridMapping;
            this.riMapping = randomizableItemMapping;
        }


        internal void CreateByteArrayDS(ref byte[] newByteArray)
        {
            foreach (Tuple<Dresssphere, Dresssphere> ds in dresssphereMapping.MappingList)
            {
                newByteArray[ds.Item2.Index] = Convert.ToByte(ds.Item2.Count);
                // Console.WriteLine($"{ds.Item1.Name}, {ds.Item1.Count} -> {ds.Item2.Name}, {ds.Item2.Count}");
            }
        }

        internal void CreateByteArrayDSTC(ref byte[] newByteArray)
        {
            foreach (var item in dresssphereMapping.RandomizableDresspheresTotalChaos)
            {
                newByteArray[item.Index] = Convert.ToByte(item.Count);
                // Console.WriteLine($"{ds.Item1.Name}, {ds.Item1.Count} -> {ds.Item2.Name}, {ds.Item2.Count}");
            }
        }

        internal void CreateByteArrayBoth(ref byte[] newByteArrayDS, ref byte[] newByteArrayGG)
        {
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

        internal void CreateByteArrayGG(ref byte[] newByteArray)
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

        internal void CheckReadBytesDS(ref byte[] readByteArray)
        {
            var tempList = Copy(dresssphereMapping.MappingList);
            foreach (var tuple in tempList)
            {
                uint newCount = readByteArray[tuple.Item2.Index];
                if (tuple.Item2.Count != newCount)
                {
                    int diff = (int)(newCount - tuple.Item2.Count);
                    dresssphereMapping.MappingList[(int)tuple.Item2.Index].Item1.Count += (uint)diff;
                    dresssphereMapping.MappingList[(int)tuple.Item2.Index].Item2.Count += (uint)diff;
                }
            }
        }
        internal void CheckReadBytesDSTotalChaos(byte[] readByteArray)
        {
            var tempList = Copy(dresssphereMapping.RandomizableDresspheresTotalChaos);
            foreach (var item in tempList)
            {
                uint newCount = readByteArray[item.Index];
                if (item.Count != newCount)
                {
                    int diff = (int)(newCount - item.Count);
                    int newDs = Randomizer.GetInt(0, 18);
                    dresssphereMapping.RandomizableDresspheresTotalChaos[newDs].Count += diff;
                }
            }
        }

        internal void CheckReadBytesBoth(ref byte[] readByteArrayDS, ref byte[] readByteArrayGG)
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

        internal void CheckReadBytesGG(ref byte[] readByteArray)
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
        #region Helper
        private List<RandomizableItem> Copy(List<RandomizableItem> listToCopy)
        {
            var newList = new List<RandomizableItem>();
            foreach (var item in listToCopy)
            {
                newList.Add(new RandomizableItem(item));
            }
            return newList;
        }
        private List<Tuple<Dresssphere, Dresssphere>> Copy(List<Tuple<Dresssphere, Dresssphere>> listToCopy)
        {
            var newList = new List<Tuple<Dresssphere, Dresssphere>>();
            foreach (var item in listToCopy)
            {
                newList.Add(new Tuple<Dresssphere, Dresssphere>(new Dresssphere(item.Item1), new Dresssphere(item.Item2)));
            }
            return newList;
        }
        #endregion Helper
    }
}
