using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public interface IRandomizable
    {
        string Name { get; }
        int Index { get; }
        bool Available { get; }
    }
}
