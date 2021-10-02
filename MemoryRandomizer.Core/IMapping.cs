using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public interface IMapping<T>
    {
        List<Tuple<T, T>> MappingList { get; set; }
        List<T> RandomizableItems { get; set; }

        void Initiate();
        void CreateMapping();
        void InitiateTotalChaos();
    }
}
