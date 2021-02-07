using System;

namespace BlazorCanvas.Example11.Core.Utils
{
    public static class RandomExtensions
    {
        public static double NextDouble(
            this Random random,
            double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        /// returns a random number between two intervals
        public static double NextDouble(
            this Random random,
            double minValue1, double maxValue1,
            double minValue2, double maxValue2)
        {
            return (1 == (random.Next() & 1)) ?
                random.NextDouble(minValue1, maxValue1) :
                random.NextDouble(minValue2, maxValue2);
        }
    }

}