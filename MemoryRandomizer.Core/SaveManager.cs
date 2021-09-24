using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public static class SaveManager
    {
        public const string DresssphereSaveFileName = "DresssphereRando.txt";
        public const string BothSaveFileName = "FullRando.txt";
        public const string GGSaveFileName = "GGRando.txt";

        private static Serializer mSerializer = new Serializer();

        public static bool ReadSaveFile<T>(IMapping<T> mapping, string fileName)
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

        public static bool ReadSaveFile(RandomizableItemMapping mapping, string fileName)
        {
            try
            {
                RandomizableItemMapping.MappingList.Clear();
                RandomizableItemMapping.MappingList = mSerializer.LoadMapping<List<Tuple<RandomizableItem, RandomizableItem>>>(fileName);
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Could not read RandomizableItem save file {fileName}: {exc}");
                Console.WriteLine("Initiating new mapping...");
                return false;
            }

        }
    }
}
