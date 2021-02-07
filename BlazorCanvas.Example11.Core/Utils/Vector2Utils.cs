using System;
using System.Numerics;

namespace BlazorCanvas.Example11.Core.Utils
{
    public static class Vector2Utils
    {
        /// <summary>
        /// https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Math/Vector2.cs#L223
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Vector2 ClampMagnitude(ref Vector2 vector, float maxLength)
        {
            float sqrMagnitude = vector.LengthSquared();
            if (sqrMagnitude > maxLength * maxLength)
            {
                float mag = (float)Math.Sqrt(sqrMagnitude);

                //these intermediate variables force the intermediate result to be
                //of float precision. without this, the intermediate result can be of higher
                //precision, which changes behavior.
                float normalized_x = vector.X / mag;
                float normalized_y = vector.Y / mag;
                return new Vector2(normalized_x * maxLength,
                    normalized_y * maxLength);
            }
            return vector;
        }

        public static Vector2 Rotate(ref Vector2 v, float degrees)
        {
            var radians = degrees * MathUtils.Deg2Rad;
            var sin = MathF.Sin(radians);
            var cos = MathF.Cos(radians);
            
            var tx = v.X;
            var ty = v.Y;

            return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
        }
    }
}