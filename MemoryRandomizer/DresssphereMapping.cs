using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer
{
    class DresssphereMapping
    {
        public static List<Tuple<Dresssphere, Dresssphere>> MappingList = new List<Tuple<Dresssphere, Dresssphere>>();

        public static List<Dresssphere> Dresspheres = new List<Dresssphere>()
        {
            new Dresssphere(0, 0x4fbc, false, "Nothing", false),
            new Dresssphere(1, 0x4fbd, false, "Gunner", true),
            new Dresssphere(2, 0x4fbe, false, "Gun Mage", true),
            new Dresssphere(3, 0x4fbf, false, "Alchemist", true),
            new Dresssphere(4, 0x4fc0, false, "Warrior", true),
            new Dresssphere(5, 0x4fc1, false, "Samurai", true),
            new Dresssphere(6, 0x4fc2, false, "Dark Knight", true),
            new Dresssphere(7, 0x4fc3, false, "Berserker", true),
            new Dresssphere(8, 0x4fc4, false, "Songstress", true),
            new Dresssphere(9, 0x4fc5, false, "Black Mage", true),
            new Dresssphere(10, 0x4fc6, false, "White Mage", true),
            new Dresssphere(11, 0x4fc7, false, "Thief", true),
            new Dresssphere(12, 0x4fc8, false, "Trainer", true),
            new Dresssphere(13, 0x4fc9, false, "Lady Luck", true),
            new Dresssphere(14, 0x4fca, false, "Mascot", true),
            new Dresssphere(15, 0x4fcb, false, "Floral Fallal", true),
            new Dresssphere(16, 0x4fcc, false, "Right Pistil", false),
            new Dresssphere(17, 0x4fcd, false, "Left Pistil", false),
            new Dresssphere(18, 0x4fce, false, "Machina Maw", true),
            new Dresssphere(19, 0x4fcf, false, "Smasher R", false),
            new Dresssphere(20, 0x4fd0, false, "Crusher L", false),
            new Dresssphere(21, 0x4fd1, false, "Full Throttle", true),
            new Dresssphere(22, 0x4fd2, false, "Dextral Wing", false),
            new Dresssphere(23, 0x4fd3, false, "Sinistral Wing", false),
            new Dresssphere(24, 0x4fd4, false, "Trainer 2", false),
            new Dresssphere(25, 0x4fd5, false, "Trainer 3", false),
            new Dresssphere(26, 0x4fd6, false, "Mascot 2", false),
            new Dresssphere(27, 0x4fd7, false, "Mascot 3", false),
            new Dresssphere(28, 0x4fd8, false, "Psychic", true),
            new Dresssphere(29, 0x4fd9, false, "Festivalist", true)
        };

        public static List<Dresssphere> RandomizableDresspheres = new List<Dresssphere>()
        {
            new Dresssphere(1, 0x4fbd, false, "Gunner", true),
            new Dresssphere(2, 0x4fbe, false, "Gun Mage", true),
            new Dresssphere(3, 0x4fbf, false, "Alchemist", true),
            new Dresssphere(4, 0x4fc0, false, "Warrior", true),
            new Dresssphere(5, 0x4fc1, false, "Samurai", true),
            new Dresssphere(6, 0x4fc2, false, "Dark Knight", true),
            new Dresssphere(7, 0x4fc3, false, "Berserker", true),
            new Dresssphere(8, 0x4fc4, false, "Songstress", true),
            new Dresssphere(9, 0x4fc5, false, "Black Mage", true),
            new Dresssphere(10, 0x4fc6, false, "White Mage", true),
            new Dresssphere(11, 0x4fc7, false, "Thief", true),
            new Dresssphere(12, 0x4fc8, false, "Trainer", true),
            new Dresssphere(13, 0x4fc9, false, "Lady Luck", true),
            new Dresssphere(14, 0x4fca, false, "Mascot", true),
            new Dresssphere(15, 0x4fcb, false, "Floral Fallal", true),
            new Dresssphere(18, 0x4fce, false, "Machina Maw", true),
            new Dresssphere(21, 0x4fd1, false, "Full Throttle", true),
            new Dresssphere(28, 0x4fd8, false, "Psychic", true),
            new Dresssphere(29, 0x4fd9, false, "Festivalist", true)
        };

        public static void CreateMapping()
        {
            int i = 0;
            foreach (Dresssphere ds in Dresspheres)
            {
                if (ds.Available)
                {
                    RandomizableDresspheres[i].Count = ds.Count;
                    MappingList.Add(new Tuple<Dresssphere, Dresssphere>(ds, RandomizableDresspheres[i]));
                    i++;
                }
                else
                {
                    MappingList.Add(new Tuple<Dresssphere, Dresssphere>(ds, ds));
                }
            }
        }
    }
}
