using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public static class SaveManager
    {
        private static Serializer mSerializer = new Serializer();

        public static bool ReadSaveFile<T>(AbstractMapping<T> mapping, string fileName) where T : IRandomizable
        {
            try
            {
                mapping.MappingList.Clear();
                mapping.MappingList = mSerializer.LoadMapping<List<Tuple<T, T>>>(fileName);
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Could not read {typeof(T).Name} save file {fileName}: {exc}");
                Console.WriteLine("Initiating new mapping...");
                return false;
            }
        }
    }
}
