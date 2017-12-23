using System;
using System.Collections.Generic;

namespace PlanetDefenders
{
    public static class Extensions
    {
        public static T Random<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var random = new Random();
            var count = list.Count;

            while (count > 1)
            {
                count--;
                var index = random.Next(count + 1);
                var value = list[index];
                list[index] = list[count];
                list[count] = value;
            }
        }
    }
}