using FFX2MemoryReader;
using MemoryRandomizer.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MemoryRandomizer
{
    class Program
    {
        private static bool randomizeDS = false;
        private static bool randomizeDSTotalChaos = false;
        private static bool randomizeGG = false;
        private static bool randomizeBoth = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Randomize and mix up Dressspheres and GarmentGrids? y/n");
            var keyIn = Console.ReadKey();
            if (keyIn.Key == ConsoleKey.Y)
            {
                randomizeBoth = true;
            }
            if (!randomizeBoth)
            {
                Console.WriteLine("Randomize Dresspheres? y/n");
                keyIn = Console.ReadKey();
                if (keyIn.Key == ConsoleKey.Y)
                {
                    randomizeDS = true;
                }
                Console.WriteLine("Randomize Dresspheres total chaos? y/n");
                keyIn = Console.ReadKey();
                if (keyIn.Key == ConsoleKey.Y)
                {
                    randomizeDSTotalChaos = true;
                }
                Console.WriteLine("Randomize GarmentGrids? y/n");
                keyIn = Console.ReadKey();
                if (keyIn.Key == ConsoleKey.Y)
                {
                    randomizeGG = true;
                }
            }

            new GameManager().Startup(randomizeDS, randomizeGG, randomizeBoth, randomizeDSTotalChaos);
        }
    }
}
