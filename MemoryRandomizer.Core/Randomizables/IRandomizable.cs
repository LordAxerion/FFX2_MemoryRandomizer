using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public interface IRandomizable
    {
        string SaveFile { get; }
        string Name { get; }
        uint Index { get; }
        bool Available { get; }
    }
}
