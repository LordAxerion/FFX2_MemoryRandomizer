using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MemoryRandomizer
{
    public class Serializer
    {
        public void SaveDresssphereMapping(string saveFile)
        {
            var jsonString = JsonConvert.SerializeObject(DresssphereMapping.MappingList);
            File.WriteAllText(saveFile, jsonString);
        }

        public T LoadMapping<T>(string saveFile)
        {
            string savedMapping = File.ReadAllText(saveFile);
            return JsonConvert.DeserializeObject<T>(savedMapping);
        }
    }
}
