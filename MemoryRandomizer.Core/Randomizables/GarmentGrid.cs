﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public class GarmentGrid : IRandomizable
    {
        public const string SAVE_FILE = "GGRando.txt";

        public string SaveFile => SAVE_FILE;
        public string Name { get; set; }
        public int Index { get; set; }
        public bool Available { get; set; }

        public GarmentGrid (int index, string name, bool available = false)
        {
            Name = name;
            Index = index;
            Available = available;
        }
    }
}
