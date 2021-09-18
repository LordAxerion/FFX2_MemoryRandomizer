using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer
{
    class GarmentGrid
    {
        public string Name { get; set; }
        public uint Index { get; set; }
        public bool Available { get; set; }

        public GarmentGrid (uint index, string name, bool available = false)
        {
            Name = name;
            Index = index;
            Available = available;
        }
    }
}
