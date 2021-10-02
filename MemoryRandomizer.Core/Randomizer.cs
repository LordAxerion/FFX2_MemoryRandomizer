using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryRandomizer.Core
{
    public static class Randomizer
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static int GetInt(int min, int max)
        {
            return rng.Next(min, max);
        }
    }
}
