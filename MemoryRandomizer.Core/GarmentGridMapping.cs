using FFX2MemoryReader;
using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public class GarmentGridMapping : IMapping<GarmentGrid>
    {
        private readonly byte[] initialByteArray;
        private readonly Serializer mSerializer = new Serializer();

        public List<Tuple<GarmentGrid, GarmentGrid>> MappingList { get; set; } = new List<Tuple<GarmentGrid, GarmentGrid>>();

        public List<GarmentGrid> GarmentGrids = new List<GarmentGrid>()
        {
            new GarmentGrid(0,"First Steps"),
            new GarmentGrid(1, "Vanguard"),
            new GarmentGrid(2, "Bum Rush"),
            new GarmentGrid(3, "Undying Storm"),
            new GarmentGrid(4, "Flash of Steel"),
            new GarmentGrid(5, "Protection Halo"),
            new GarmentGrid(6, "Hour of Need"),
            new GarmentGrid(7, "Unwavering Guard"),
            new GarmentGrid(8, "Valiant Lustre"),
            new GarmentGrid(9, "Highroad Winds"),
            new GarmentGrid(10, "Mounted Assault"),
            new GarmentGrid(11, "Heart of Flame"),
            new GarmentGrid(12, "Ice Queen"),
            new GarmentGrid(13, "Thunder Spawn"),
            new GarmentGrid(14, "Menace of The Deep"),
            new GarmentGrid(15, "Downtrodder"),
            new GarmentGrid(16, "Sacred Beast"),
            new GarmentGrid(17, "Tetra Master"),
            new GarmentGrid(18, "Restless Sleep"),
            new GarmentGrid(19, "Still of Night"),
            new GarmentGrid(20, "Mortal Coil"),
            new GarmentGrid(21, "Racing Giant"),
            new GarmentGrid(22, "Bitter Farewell"),
            new GarmentGrid(23, "Selene Guard"),
            new GarmentGrid(24, "Helios Guard"),
            new GarmentGrid(25, "Shining Mirror"),
            new GarmentGrid(26, "Covetous"),
            new GarmentGrid(27, "Disaster in Bloom"),
            new GarmentGrid(28, "Scourgebane"),
            new GarmentGrid(29, "Healing Wind"),
            new GarmentGrid(30, "Heart Reborn"),
            new GarmentGrid(31, "Healing Light"),
            new GarmentGrid(32, "Immortal Soul"),
            new GarmentGrid(33, "Wish Bringer"),
            new GarmentGrid(34, "Strength of One"),
            new GarmentGrid(35, "Seething Cauldron"),
            new GarmentGrid(36, "Stonehewn"),
            new GarmentGrid(37, "Enigma Plate"),
            new GarmentGrid(38, "Howling Wind"),
            new GarmentGrid(39, "Ray of Hope"),
            new GarmentGrid(40, "Pride of the Sword"),
            new GarmentGrid(41, "Samurai's Honor"),
            new GarmentGrid(42, "Blood of the Beast"),
            new GarmentGrid(43, "Chaos Maelstrom"),
            new GarmentGrid(44, "White Signet"),
            new GarmentGrid(45, "Black Tabard"),
            new GarmentGrid(46, "Mercurial Strike"),
            new GarmentGrid(47, "Tricks of the Trade"),
            new GarmentGrid(48, "Horn of Penalty"),
            new GarmentGrid(49, "Tresure Hunt"),
            new GarmentGrid(50, "Temperd Will"),
            new GarmentGrid(51, "Covenant of Growth"),
            new GarmentGrid(52, "Salvation Promised"),
            new GarmentGrid(53, "Conflagration"),
            new GarmentGrid(54, "Supreme Light"),
            new GarmentGrid(55, "Megiddo"),
            new GarmentGrid(56, "Unerring Path"),
            new GarmentGrid(57, "Font of Power"),
            new GarmentGrid(58, "Higher Power"),
            new GarmentGrid(59, "The End"),
            new GarmentGrid(60, "Intrepid"),
            new GarmentGrid(61, "Abominable"),
            new GarmentGrid(62, "Peerless"),
            new GarmentGrid(63, "Last Resort")
        };

        public List<GarmentGrid> RandomizableItems { get; set; } = new List<GarmentGrid>()
        {
            new GarmentGrid(0,"First Steps"),
            new GarmentGrid(1, "Vanguard"),
            new GarmentGrid(2, "Bum Rush"),
            new GarmentGrid(3, "Undying Storm"),
            new GarmentGrid(4, "Flash of Steel"),
            new GarmentGrid(5, "Protection Halo"),
            new GarmentGrid(6, "Hour of Need"),
            new GarmentGrid(7, "Unwavering Guard"),
            new GarmentGrid(8, "Valiant Lustre"),
            new GarmentGrid(9, "Highroad Winds"),
            new GarmentGrid(10, "Mounted Assault"),
            new GarmentGrid(11, "Heart of Flame"),
            new GarmentGrid(12, "Ice Queen"),
            new GarmentGrid(13, "Thunder Spawn"),
            new GarmentGrid(14, "Menace of The Deep"),
            new GarmentGrid(15, "Downtrodder"),
            new GarmentGrid(16, "Sacred Beast"),
            new GarmentGrid(17, "Tetra Master"),
            new GarmentGrid(18, "Restless Sleep"),
            new GarmentGrid(19, "Still of Night"),
            new GarmentGrid(20, "Mortal Coil"),
            new GarmentGrid(21, "Racing Giant"),
            new GarmentGrid(22, "Bitter Farewell"),
            new GarmentGrid(23, "Selene Guard"),
            new GarmentGrid(24, "Helios Guard"),
            new GarmentGrid(25, "Shining Mirror"),
            new GarmentGrid(26, "Covetous"),
            new GarmentGrid(27, "Disaster in Bloom"),
            new GarmentGrid(28, "Scourgebane"),
            new GarmentGrid(29, "Healing Wind"),
            new GarmentGrid(30, "Heart Reborn"),
            new GarmentGrid(31, "Healing Light"),
            new GarmentGrid(32, "Immortal Soul"),
            new GarmentGrid(33, "Wish Bringer"),
            new GarmentGrid(34, "Strength of One"),
            new GarmentGrid(35, "Seething Cauldron"),
            new GarmentGrid(36, "Stonehewn"),
            new GarmentGrid(37, "Enigma Plate"),
            new GarmentGrid(38, "Howling Wind"),
            new GarmentGrid(39, "Ray of Hope"),
            new GarmentGrid(40, "Pride of the Sword"),
            new GarmentGrid(41, "Samurai's Honor"),
            new GarmentGrid(42, "Blood of the Beast"),
            new GarmentGrid(43, "Chaos Maelstrom"),
            new GarmentGrid(44, "White Signet"),
            new GarmentGrid(45, "Black Tabard"),
            new GarmentGrid(46, "Mercurial Strike"),
            new GarmentGrid(47, "Tricks of the Trade"),
            new GarmentGrid(48, "Horn of Penalty"),
            new GarmentGrid(49, "Tresure Hunt"),
            new GarmentGrid(50, "Temperd Will"),
            new GarmentGrid(51, "Covenant of Growth"),
            new GarmentGrid(52, "Salvation Promised"),
            new GarmentGrid(53, "Conflagration"),
            new GarmentGrid(54, "Supreme Light"),
            new GarmentGrid(55, "Megiddo"),
            new GarmentGrid(56, "Unerring Path"),
            new GarmentGrid(57, "Font of Power"),
            new GarmentGrid(58, "Higher Power"),
            new GarmentGrid(59, "The End"),
            new GarmentGrid(60, "Intrepid"),
            new GarmentGrid(61, "Abominable"),
            new GarmentGrid(62, "Peerless"),
            new GarmentGrid(63, "Last Resort")
        };

        public GarmentGridMapping(byte[] initialByteArray)
        {
            this.initialByteArray = initialByteArray;
        }

        public void CreateMapping()
        {
            int i = 0;
            foreach (GarmentGrid gg in GarmentGrids)
            {

                this.RandomizableItems[i].Available = gg.Available;
                MappingList.Add(new Tuple<GarmentGrid, GarmentGrid>(gg, this.RandomizableItems[i]));
                i++;
            }
        }
        public void Initiate()
        {
            for (int i = 0; i < GarmentGrids.Count; i++)
            {
                var byteIndex = i / 8;
                var bitIndex = i % 8;
                byte mask = (byte)(1 << bitIndex);
                GarmentGrids[i].Available = (this.initialByteArray[byteIndex] & mask) != 0;
            }
        }

        public void InitiateTotalChaos()
        {
            throw new NotImplementedException();
        }

        public void Randomize(ProcessMemoryReader mReader, ByteArrayHandler byteArrayHandler, byte[] memoryBytesGG, byte[] memoryBytesDS = null)
        {
            byteArrayHandler.CheckReadBytesGG(ref memoryBytesGG);
            byte[] newByteArrayGG = new byte[0x8];
            byteArrayHandler.CreateByteArrayGG(ref newByteArrayGG);

            int error = mReader.WriteMemory((IntPtr)((uint)mReader.ReadProcess.Modules[0].BaseAddress + IMapping<GarmentGrid>.startOfGGSaves), newByteArrayGG, out _);
            mReader.CheckError(error);

            mSerializer.SaveMapping(SaveManager.GGSaveFileName, this.MappingList);
        }
    }
}
