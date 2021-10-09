using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public interface IRandomizable
    {
        public string SaveFile { get; }
        public string Name { get; }
        public uint Index { get; }
        public bool Available { get; }
    }
}
