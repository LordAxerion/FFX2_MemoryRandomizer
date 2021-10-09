using FFX2MemoryReader;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public class RandomizableItemMapping : AbstractMapping<RandomizableItem>
    {
        private const string SAVE_FILE = RandomizableItem.SAVE_FILE;
        private readonly byte[] initialByteArrayDS;
        private readonly byte[] initialByteArrayGG;

        internal override string SaveFile => SAVE_FILE;

        public static List<RandomizableItem> Items = new List<RandomizableItem>()
        {
            /*new RandomizableItem("First Steps", 0, false, true),
            new RandomizableItem("Vanguard", 1, false, true),
            new RandomizableItem("Bum Rush", 2, false, true),
            new RandomizableItem("Undying Storm", 3, false, true),
            new RandomizableItem("Flash of Steel", 4, false, true),
            new RandomizableItem("Protection Halo", 5, false, true),
            new RandomizableItem("Hour of Need", 6, false, true),
            new RandomizableItem("Unwavering Guard", 7, false, true),
            new RandomizableItem("Valiant Lustre", 8, false, true),
            new RandomizableItem("Highroad Winds", 9, false, true),
            new RandomizableItem("Mounted Assault", 10, false, true),
            new RandomizableItem("Heart of Flame", 11, false, true),
            new RandomizableItem("Ice Queen", 12, false, true),
            new RandomizableItem("Thunder Spawn", 13, false, true),
            new RandomizableItem("Menace of The Deep", 14, false, true),
            new RandomizableItem("Downtrodder", 15, false, true),
            new RandomizableItem("Sacred Beast", 16, false, true),
            new RandomizableItem("Tetra Master", 17, false, true),
            new RandomizableItem("Restless Sleep", 18, false, true),
            new RandomizableItem("Still of Night", 19, false, true),
            new RandomizableItem("Mortal Coil", 20, false, true),
            new RandomizableItem("Racing Giant", 21, false, true),
            new RandomizableItem("Bitter Farewell", 22, false, true),
            new RandomizableItem("Selene Guard", 23, false, true),
            new RandomizableItem("Helios Guard", 24, false, true),
            new RandomizableItem("Shining Mirror", 25, false, true),
            new RandomizableItem("Covetous", 26, false, true),
            new RandomizableItem("Disaster in Bloom", 27, false, true),
            new RandomizableItem("Scourgebane", 28, false, true),
            new RandomizableItem("Healing Wind", 29, false, true),
            new RandomizableItem("Heart Reborn", 30, false, true),
            new RandomizableItem("Healing Light", 31, false, true),
            new RandomizableItem("Immortal Soul", 32, false, true),
            new RandomizableItem("Wish Bringer", 33, false, true),
            new RandomizableItem("Strength of One", 34, false, true),
            new RandomizableItem("Seething Cauldron", 35, false, true),
            new RandomizableItem("Stonehewn", 36, false, true),
            new RandomizableItem("Enigma Plate", 37, false, true),
            new RandomizableItem("Howling Wind", 38, false, true),
            new RandomizableItem("Ray of Hope", 39, false, true),
            new RandomizableItem("Pride of the Sword", 40, false, true),
            new RandomizableItem("Samurai's Honor", 41, false, true),
            new RandomizableItem("Blood of the Beast", 42, false, true),
            new RandomizableItem("Chaos Maelstrom", 43, false, true),
            new RandomizableItem("White Signet", 44, false, true),
            new RandomizableItem("Black Tabard", 45, false, true),
            new RandomizableItem("Mercurial Strike", 46, false, true),
            new RandomizableItem("Tricks of the Trade", 47, false, true),
            new RandomizableItem("Horn of Penalty", 48, false, true),
            new RandomizableItem("Tresure Hunt", 49, false, true),
            new RandomizableItem("Temperd Will", 50, false, true),
            new RandomizableItem("Covenant of Growth", 51, false, true),
            new RandomizableItem("Salvation Promised", 52, false, true),
            new RandomizableItem("Conflagration", 53, false, true),
            new RandomizableItem("Supreme Light", 54, false, true),
            new RandomizableItem("Megiddo", 55, false, true),
            new RandomizableItem("Unerring Path", 56, false, true),
            new RandomizableItem("Font of Power", 57, false, true),
            new RandomizableItem("Higher Power", 58, false, true),
            new RandomizableItem("The End", 59, false, true),
            new RandomizableItem("Intrepid", 60, false, true),
            new RandomizableItem("Abominable", 61, false, true),
            new RandomizableItem("Peerless", 62, false, true),
            new RandomizableItem("Last Resort", 63, false, true),
            // Dressspheres
            new RandomizableItem("Nothing", 0, true, false), //64
            new RandomizableItem("Gunner", 1, true, true), //65
            new RandomizableItem("Gun Mage", 2, true, true), //66
            new RandomizableItem("Alchemist", 3, true, true), //67
            new RandomizableItem("Warrior", 4, true, true), //68
            new RandomizableItem("Samurai", 5, true, true), //69
            new RandomizableItem("Dark Knight", 6, true, true), //70
            new RandomizableItem("Berserker", 7, true, true), //71
            new RandomizableItem("Songstress", 8, true, true), //72
            new RandomizableItem("Black Mage", 9, true, true), //73
            new RandomizableItem("White Mage", 10, true, true), //74
            new RandomizableItem("Thief", 11, true, true), //75
            new RandomizableItem("Trainer", 12, true, true), //76
            new RandomizableItem("Lady Luck", 13, true, true), //77
            new RandomizableItem("Mascot", 14, true, true), //78
            new RandomizableItem("Floral Fallal", 15, true, true), //79
            new RandomizableItem("Right Pistil", 16, true, false), //80
            new RandomizableItem("Left Pistil", 17, true, false), //81
            new RandomizableItem("Machina Maw", 18, true, true), //82
            new RandomizableItem("Smasher R", 19, true, false), //83
            new RandomizableItem("Crusher L", 20, true, false), //84
            new RandomizableItem("Full Throttle", 21, true, true), //85
            new RandomizableItem("Dextral Wing", 22, true, false), //86
            new RandomizableItem("Sinistral Wing", 23, true, false), //87
            new RandomizableItem("Trainer 2", 24, true, false), //88
            new RandomizableItem("Trainer 3", 25, true, false), //89
            new RandomizableItem("Mascot 2", 26, true, false), //90
            new RandomizableItem("Mascot 3", 27, true, false), //91
            new RandomizableItem("Psychic", 28, true, true), //92
            new RandomizableItem("Festivalist", 29, true, true) //93*/
        };
        internal override List<RandomizableItem> RandomizableItems { get; set; } = new List<RandomizableItem>()
        {
            /*new RandomizableItem("First Steps", 0, false, true),
            new RandomizableItem("Vanguard", 1, false, true),
            new RandomizableItem("Bum Rush", 2, false, true),
            new RandomizableItem("Undying Storm", 3, false, true),
            new RandomizableItem("Flash of Steel", 4, false, true),
            new RandomizableItem("Protection Halo", 5, false, true),
            new RandomizableItem("Hour of Need", 6, false, true),
            new RandomizableItem("Unwavering Guard", 7, false, true),
            new RandomizableItem("Valiant Lustre", 8, false, true),
            new RandomizableItem("Highroad Winds", 9, false, true),
            new RandomizableItem("Mounted Assault", 10, false, true),
            new RandomizableItem("Heart of Flame", 11, false, true),
            new RandomizableItem("Ice Queen", 12, false, true),
            new RandomizableItem("Thunder Spawn", 13, false, true),
            new RandomizableItem("Menace of The Deep", 14, false, true),
            new RandomizableItem("Downtrodder", 15, false, true),
            new RandomizableItem("Sacred Beast", 16, false, true),
            new RandomizableItem("Tetra Master", 17, false, true),
            new RandomizableItem("Restless Sleep", 18, false, true),
            new RandomizableItem("Still of Night", 19, false, true),
            new RandomizableItem("Mortal Coil", 20, false, true),
            new RandomizableItem("Racing Giant", 21, false, true),
            new RandomizableItem("Bitter Farewell", 22, false, true),
            new RandomizableItem("Selene Guard", 23, false, true),
            new RandomizableItem("Helios Guard", 24, false, true),
            new RandomizableItem("Shining Mirror", 25, false, true),
            new RandomizableItem("Covetous", 26, false, true),
            new RandomizableItem("Disaster in Bloom", 27, false, true),
            new RandomizableItem("Scourgebane", 28, false, true),
            new RandomizableItem("Healing Wind", 29, false, true),
            new RandomizableItem("Heart Reborn", 30, false, true),
            new RandomizableItem("Healing Light", 31, false, true),
            new RandomizableItem("Immortal Soul", 32, false, true),
            new RandomizableItem("Wish Bringer", 33, false, true),
            new RandomizableItem("Strength of One", 34, false, true),
            new RandomizableItem("Seething Cauldron", 35, false, true),
            new RandomizableItem("Stonehewn", 36, false, true),
            new RandomizableItem("Enigma Plate", 37, false, true),
            new RandomizableItem("Howling Wind", 38, false, true),
            new RandomizableItem("Ray of Hope", 39, false, true),
            new RandomizableItem("Pride of the Sword", 40, false, true),
            new RandomizableItem("Samurai's Honor", 41, false, true),
            new RandomizableItem("Blood of the Beast", 42, false, true),
            new RandomizableItem("Chaos Maelstrom", 43, false, true),
            new RandomizableItem("White Signet", 44, false, true),
            new RandomizableItem("Black Tabard", 45, false, true),
            new RandomizableItem("Mercurial Strike", 46, false, true),
            new RandomizableItem("Tricks of the Trade", 47, false, true),
            new RandomizableItem("Horn of Penalty", 48, false, true),
            new RandomizableItem("Tresure Hunt", 49, false, true),
            new RandomizableItem("Temperd Will", 50, false, true),
            new RandomizableItem("Covenant of Growth", 51, false, true),
            new RandomizableItem("Salvation Promised", 52, false, true),
            new RandomizableItem("Conflagration", 53, false, true),
            new RandomizableItem("Supreme Light", 54, false, true),
            new RandomizableItem("Megiddo", 55, false, true),
            new RandomizableItem("Unerring Path", 56, false, true),
            new RandomizableItem("Font of Power", 57, false, true),
            new RandomizableItem("Higher Power", 58, false, true),
            new RandomizableItem("The End", 59, false, true),
            new RandomizableItem("Intrepid", 60, false, true),
            new RandomizableItem("Abominable", 61, false, true),
            new RandomizableItem("Peerless", 62, false, true),
            new RandomizableItem("Last Resort", 63, false, true),
            // Dressspheres
            new RandomizableItem("Gunner", 1, true, true), //64
            new RandomizableItem("Gun Mage", 2, true, true), //65
            new RandomizableItem("Alchemist", 3, true, true), //66
            new RandomizableItem("Warrior", 4, true, true), //67
            new RandomizableItem("Samurai", 5, true, true), //68
            new RandomizableItem("Dark Knight", 6, true, true), //69
            new RandomizableItem("Berserker", 7, true, true), //70
            new RandomizableItem("Songstress", 8, true, true), //71
            new RandomizableItem("Black Mage", 9, true, true), //72
            new RandomizableItem("White Mage", 10, true, true), //73
            new RandomizableItem("Thief", 11, true, true), //74
            new RandomizableItem("Trainer", 12, true, true), //75
            new RandomizableItem("Lady Luck", 13, true, true), //76
            new RandomizableItem("Mascot", 14, true, true), //77
            new RandomizableItem("Floral Fallal", 15, true, true), //78
            new RandomizableItem("Machina Maw", 18, true, true), //79
            new RandomizableItem("Full Throttle", 21, true, true), //80
            new RandomizableItem("Psychic", 28, true, true), //81
            new RandomizableItem("Festivalist", 29, true, true) //82*/
        };

        public RandomizableItemMapping(ProcessMemoryReader mReader, byte[] initialByteArrayDS, byte[] initialByteArrayGG) : base(mReader)
        {
            this.initialByteArrayDS = initialByteArrayDS;
            this.initialByteArrayGG = initialByteArrayGG;
        }

        internal override void CreateMapping()
        {
            int i = 0;
            foreach (RandomizableItem item in Items)
            {
                if (item.Available)
                {
                    RandomizableItems[i].GotIt = item.GotIt;
                    RandomizableItems[i].Count = item.Count;
                    this.MappingList.Add(new Tuple<RandomizableItem, RandomizableItem>(item, RandomizableItems[i]));
                    i++;
                }
                else
                {
                    this.MappingList.Add(new Tuple<RandomizableItem, RandomizableItem>(item, item));
                }
            }
        }

        internal override void Initiate()
        {
            foreach (RandomizableItem item in Items)
            {
                if (item.ItemType == RandoItemType.Dresssphere)
                {
                    item.Count = this.initialByteArrayDS[item.Index];
                    if(item.Count != 0)
                    {
                        item.GotIt = true;
                    }
                }
                else
                {
                    byte mask = (byte)(1 << item.BitIndex);
                    item.GotIt = (this.initialByteArrayGG[item.ByteIndex] & mask) != 0;
                    if (item.GotIt)
                    {
                        item.Count = 1;
                    }
                }
            }
        }

        internal override void InitiateTotalChaos()
        {
            throw new NotImplementedException();
        }

        protected override void Save()
        {
            throw new NotImplementedException();
        }

        protected override void WriteMemory(byte[] memoryBytesDS, byte[] memoryBytesGG)
        {
            throw new NotImplementedException();
        }
    }
}
