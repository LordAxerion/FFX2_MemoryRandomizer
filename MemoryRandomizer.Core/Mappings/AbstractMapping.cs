using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public abstract class AbstractMapping<T> where T : IRandomizable
    {
        public List<Tuple<T, T>> MappingList { get; set; }
        public List<T> RandomizableItems { get; set; }

        internal abstract void Initiate();
        internal abstract void CreateMapping();
        internal abstract void InitiateTotalChaos();
    }
}
