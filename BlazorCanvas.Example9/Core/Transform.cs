using System.Numerics;

namespace BlazorCanvas.Example9.Core
{
    public class Transform
    {
        public Vector2 Position = Vector2.Zero;

        public Vector2 Scale = Vector2.One;

        public Vector2 Direction = Vector2.UnitX;

        public float Rotation = 0f;

        public static readonly Transform Identity = new Transform()
        {
            Position = Vector2.Zero,
            Scale = Vector2.One,
            Direction = Vector2.UnitX,
            Rotation = 0f
        };
    }
}