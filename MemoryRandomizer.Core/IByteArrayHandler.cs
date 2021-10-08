using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    internal interface IByteArrayHandler<T>
    {
        void CreateByteArray(out byte[] newByteArrayDS, out byte[] newByteArrayGG);
        void CheckReadBytes(ref byte[] readByteArrayDS, ref byte[] readByteArrayGG);
    }
}
