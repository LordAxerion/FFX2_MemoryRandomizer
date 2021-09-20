using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MemoryRandomizer.Core
{
    public class Serializer
    {
        public void SaveDresssphereMapping<T>(string saveFile, T mappingList)
        {
            var jsonString = JsonConvert.SerializeObject(mappingList);
            File.WriteAllText(saveFile, jsonString);
        }

        public T LoadMapping<T>(string saveFile)
        {
            string savedMapping = File.ReadAllText(saveFile);
            return JsonConvert.DeserializeObject<T>(savedMapping);
        }
    }
}
