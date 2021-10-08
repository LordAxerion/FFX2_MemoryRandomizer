using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public class Dresssphere
    {
        public ushort Address { get; set; }
        public string Name { get; set; }
        public uint Index { get; set; }
        // Some Dressspheres are in memory, but they will always stay on 0
        // We want to know them but not randomize them
        public bool Available { get; set; }
        public uint Count { get; set; }

        [JsonConstructor]
        public Dresssphere(uint index, ushort address, string name, bool available, uint count = 0)
        {
            Index = index;
            Address = address;
            Name = name;
            Available = available;
            Count = count;
        }

        public Dresssphere(Dresssphere ds)
        {
            Index = ds.Index;
            Address = ds.Address;
            Name = ds.Name;
            Available = ds.Available;
            Count = ds.Count;
        }
    }
}
