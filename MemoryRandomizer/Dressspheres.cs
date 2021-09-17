using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer
{
    class Dressspheres
    {
        public ushort Address { get; set; }
        public bool Received { get; set; }
        public string Name { get; set; }

        public Dressspheres(ushort address, bool received, string name)
        {
            Address = address;
            Received = received;
            Name = name;
        }
    }
}
