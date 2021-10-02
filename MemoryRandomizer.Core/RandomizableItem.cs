using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public class RandomizableItem
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public int ByteIndex { get; set; }
        public int BitIndex { get; set; }
        public bool GotIt { get; set; }
        public int Count { get; set; }
        public bool Available { get; set; }
        public int Offset { get; set; }
        public int MaxCount { get; set; }

        public RandoItemType ItemType{ get; set;}

        [JsonConstructor]
        public RandomizableItem(string name, int index, bool available, RandoItemType itemType, int offset = 0x0, int maxCount = 999, bool gotIt = false, int count = 0)
        {
            Name = name;
            Index = index;
            ByteIndex = index / 8;
            BitIndex = index % 8;
            GotIt = gotIt;
            Count = count;
            Available = available;
            Offset = offset;
            MaxCount = maxCount;
        }

        public RandomizableItem(RandomizableItem item)
        {
            Name = item.Name;
            Index = item.Index;
            ByteIndex = item.ByteIndex;
            BitIndex = item.BitIndex;
            GotIt = item.GotIt;
            Count = item.Count;
            Available = item.Available;
            Offset = item.Offset;
            MaxCount = item.MaxCount;
        }
    }
}
