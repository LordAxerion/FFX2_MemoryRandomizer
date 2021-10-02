using FFX2MemoryReader;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public class DresssphereMapping : IMapping<Dresssphere>
    {
        private readonly byte[] initialByteArray;
        private readonly Serializer mSerializer = new Serializer();

        public List<Tuple<Dresssphere, Dresssphere>> MappingList { get; set; } = new List<Tuple<Dresssphere, Dresssphere>>();

        public List<Dresssphere> Dresspheres = new List<Dresssphere>()
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

        public List<Dresssphere> RandomizableItems { get; set; } = new List<Dresssphere>()
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


        public DresssphereMapping(byte[] initialByteArray)
        {
            this.initialByteArray = initialByteArray;
        }

        public List<RandomizableItem> RandomizableDresspheresTotalChaos { get; set; } = new List<RandomizableItem>()
        {
            new RandomizableItem("Gunner", 1, true, RandoItemType.Dresssphere, 0x4fbd),
            new RandomizableItem("Gun Mage", 2, true, RandoItemType.Dresssphere, 0x4fbe), 
            new RandomizableItem("Alchemist", 3, true, RandoItemType.Dresssphere, 0x4fbf),
            new RandomizableItem("Warrior", 4, true, RandoItemType.Dresssphere, 0x4fc0),
            new RandomizableItem("Samurai", 5, true, RandoItemType.Dresssphere, 0x4fc1),
            new RandomizableItem("Dark Knight", 6, true, RandoItemType.Dresssphere, 0x4fc2),
            new RandomizableItem("Berserker", 7, true, RandoItemType.Dresssphere, 0x4fc3),
            new RandomizableItem("Songstress", 8, true, RandoItemType.Dresssphere, 0x4fc4),
            new RandomizableItem("Black Mage", 9, true, RandoItemType.Dresssphere, 0x4fc5),
            new RandomizableItem("White Mage", 10, true, RandoItemType.Dresssphere, 0x4fc6),
            new RandomizableItem("Thief", 11, true, RandoItemType.Dresssphere, 0x4fc7),
            new RandomizableItem("Trainer", 12, true, RandoItemType.Dresssphere, 0x4fc8),
            new RandomizableItem("Lady Luck", 13, true, RandoItemType.Dresssphere, 0x4fc9),
            new RandomizableItem("Mascot", 14, true, RandoItemType.Dresssphere, 0x4fca),
            new RandomizableItem("Floral Fallal", 15, true, RandoItemType.Dresssphere, 0x4fcb),
            new RandomizableItem("Machina Maw", 18, true, RandoItemType.Dresssphere, 0x4fce),
            new RandomizableItem("Full Throttle", 21, true, RandoItemType.Dresssphere, 0x4fd1),
            new RandomizableItem("Psychic", 28, true, RandoItemType.Dresssphere, 0x4fd8),
            new RandomizableItem("Festivalist", 29, true, RandoItemType.Dresssphere, 0x4fd9)
        };

        public void CreateMapping()
        {
            int i = 0;
            foreach (Dresssphere ds in Dresspheres)
            {
                if (ds.Available)
                {
                    this.RandomizableItems[i].Count = ds.Count;
                    MappingList.Add(new Tuple<Dresssphere, Dresssphere>(ds, this.RandomizableItems[i]));
                    i++;
                }
                else
                {
                    MappingList.Add(new Tuple<Dresssphere, Dresssphere>(ds, ds));
                }
            }
        }
        public void Initiate()
        {
            int i = 0;
            foreach (byte b in this.initialByteArray)
            {
                Dresspheres[i].Count = Convert.ToUInt32(this.initialByteArray[i]);
                i++;
            }
        }
        // this v can be merged with this ^ when we change all Items to RandomizableItem instead of Dressphere/GarmentGrid
        public void InitiateTotalChaos()
        {
            foreach (var item in RandomizableDresspheresTotalChaos)
            {
                item.Count = initialByteArray[item.Index];
            }
        }

        public void Randomize(ProcessMemoryReader mReader, ByteArrayHandler byteArrayHandler, byte[] memoryBytesDS, byte[] memoryBytesGG = null)
        {
            // check byteArray for changes -> apply changes to mapping 
            byteArrayHandler.CheckReadBytesDS(ref memoryBytesDS);
            // Write mapping data to memory
            byte[] newByteArrayDS = new byte[0x1E];
            byteArrayHandler.CreateByteArrayDS(ref newByteArrayDS);

            int error = mReader.WriteMemory((IntPtr)((uint)mReader.ReadProcess.Modules[0].BaseAddress + IMapping<Dresssphere>.startofDresssphereSaves), newByteArrayDS, out _);
            mReader.CheckError(error);

            mSerializer.SaveMapping(SaveManager.DresssphereSaveFileName, this.MappingList);
        }
    }
}
