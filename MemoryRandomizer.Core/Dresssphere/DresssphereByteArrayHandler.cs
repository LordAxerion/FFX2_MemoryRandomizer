using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    internal class DresssphereByteArrayHandler
    {
        private DresssphereMapping mapping;

        internal DresssphereByteArrayHandler(DresssphereMapping dresssphereMapping)
        {
            this.mapping = dresssphereMapping;
        }


        internal void CreateByteArray(ref byte[] newByteArray)
        {
            foreach (Tuple<Dresssphere, Dresssphere> ds in mapping.MappingList)
            {
                newByteArray[ds.Item2.Index] = Convert.ToByte(ds.Item2.Count);
                // Console.WriteLine($"{ds.Item1.Name}, {ds.Item1.Count} -> {ds.Item2.Name}, {ds.Item2.Count}");
            }
        }

        internal void CreateByteArrayTotalChaos(ref byte[] newByteArray)
        {
            foreach (var item in mapping.RandomizableDresspheresTotalChaos)
            {
                newByteArray[item.Index] = Convert.ToByte(item.Count);
                // Console.WriteLine($"{ds.Item1.Name}, {ds.Item1.Count} -> {ds.Item2.Name}, {ds.Item2.Count}");
            }
        }

        internal void CheckReadBytes(ref byte[] readByteArray)
        {
            var tempList = Copy(mapping.MappingList);
            foreach (var tuple in tempList)
            {
                uint newCount = readByteArray[tuple.Item2.Index];
                if (tuple.Item2.Count != newCount)
                {
                    int diff = (int)(newCount - tuple.Item2.Count);
                    if (diff > 0)
                    {
                        mapping.MappingList[(int)tuple.Item2.Index].Item1.Count += (uint)diff;
                        mapping.MappingList[(int)tuple.Item2.Index].Item2.Count += (uint)diff;
                    }
                }
            }
        }
        internal void CheckReadBytesTotalChaos(byte[] readByteArray)
        {
            var tempList = Copy(mapping.RandomizableDresspheresTotalChaos);
            foreach (var item in tempList)
            {
                uint newCount = readByteArray[item.Index];
                if (item.Count != newCount)
                {
                    int diff = (int)(newCount - item.Count);
                    int newDs = Randomizer.GetInt(0, 18);
                    if (diff > 0)
                        mapping.RandomizableDresspheresTotalChaos[newDs].Count += diff;
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
