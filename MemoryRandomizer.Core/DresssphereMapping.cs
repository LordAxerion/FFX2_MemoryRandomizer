using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public class DresssphereMapping
    {
        public static List<Tuple<Dresssphere, Dresssphere>> MappingList = new List<Tuple<Dresssphere, Dresssphere>>();

        public static List<Dresssphere> Dresspheres = new List<Dresssphere>()
        {
            new Dresssphere(0, 0x4fbc, "Nothing", false),
            new Dresssphere(1, 0x4fbd, "Gunner", true),
            new Dresssphere(2, 0x4fbe, "Gun Mage", true),
            new Dresssphere(3, 0x4fbf, "Alchemist", true),
            new Dresssphere(4, 0x4fc0, "Warrior", true),
            new Dresssphere(5, 0x4fc1, "Samurai", true),
            new Dresssphere(6, 0x4fc2, "Dark Knight", true),
            new Dresssphere(7, 0x4fc3, "Berserker", true),
            new Dresssphere(8, 0x4fc4, "Songstress", true),
            new Dresssphere(9, 0x4fc5, "Black Mage", true),
            new Dresssphere(10, 0x4fc6, "White Mage", true),
            new Dresssphere(11, 0x4fc7, "Thief", true),
            new Dresssphere(12, 0x4fc8, "Trainer", true),
            new Dresssphere(13, 0x4fc9, "Lady Luck", true),
            new Dresssphere(14, 0x4fca, "Mascot", true),
            new Dresssphere(15, 0x4fcb, "Floral Fallal", true),
            new Dresssphere(16, 0x4fcc, "Right Pistil", false),
            new Dresssphere(17, 0x4fcd, "Left Pistil", false),
            new Dresssphere(18, 0x4fce, "Machina Maw", true),
            new Dresssphere(19, 0x4fcf, "Smasher R", false),
            new Dresssphere(20, 0x4fd0, "Crusher L", false),
            new Dresssphere(21, 0x4fd1, "Full Throttle", true),
            new Dresssphere(22, 0x4fd2, "Dextral Wing", false),
            new Dresssphere(23, 0x4fd3, "Sinistral Wing", false),
            new Dresssphere(24, 0x4fd4, "Trainer 2", false),
            new Dresssphere(25, 0x4fd5, "Trainer 3", false),
            new Dresssphere(26, 0x4fd6, "Mascot 2", false),
            new Dresssphere(27, 0x4fd7, "Mascot 3", false),
            new Dresssphere(28, 0x4fd8, "Psychic", true),
            new Dresssphere(29, 0x4fd9, "Festivalist", true)
        };

        public static List<Dresssphere> RandomizableDresspheres = new List<Dresssphere>()
        {
            new Dresssphere(1, 0x4fbd, "Gunner", true),
            new Dresssphere(2, 0x4fbe, "Gun Mage", true),
            new Dresssphere(3, 0x4fbf, "Alchemist", true),
            new Dresssphere(4, 0x4fc0, "Warrior", true),
            new Dresssphere(5, 0x4fc1, "Samurai", true),
            new Dresssphere(6, 0x4fc2, "Dark Knight", true),
            new Dresssphere(7, 0x4fc3, "Berserker", true),
            new Dresssphere(8, 0x4fc4, "Songstress", true),
            new Dresssphere(9, 0x4fc5, "Black Mage", true),
            new Dresssphere(10, 0x4fc6, "White Mage", true),
            new Dresssphere(11, 0x4fc7, "Thief", true),
            new Dresssphere(12, 0x4fc8, "Trainer", true),
            new Dresssphere(13, 0x4fc9, "Lady Luck", true),
            new Dresssphere(14, 0x4fca, "Mascot", true),
            new Dresssphere(15, 0x4fcb, "Floral Fallal", true),
            new Dresssphere(18, 0x4fce, "Machina Maw", true),
            new Dresssphere(21, 0x4fd1, "Full Throttle", true),
            new Dresssphere(28, 0x4fd8, "Psychic", true),
            new Dresssphere(29, 0x4fd9, "Festivalist", true)
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
        public static void InitiateDressspheres(byte[] initialByteArray)
        {
            int i = 0;
            foreach (byte b in initialByteArray)
            {
                Dresspheres[i].Count = Convert.ToUInt32(initialByteArray[i]);
                i++;
            }
        }
    }
}
