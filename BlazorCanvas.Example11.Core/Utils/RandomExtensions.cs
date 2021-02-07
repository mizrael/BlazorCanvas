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
    }

}