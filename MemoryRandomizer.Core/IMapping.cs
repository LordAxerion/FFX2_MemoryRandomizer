using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public interface IMapping<T>
    {
        List<Tuple<T, T>> MappingList { get; set; }

        void CreateMapping();
        void Initiate(byte[] initialByteArray);
    }
}
