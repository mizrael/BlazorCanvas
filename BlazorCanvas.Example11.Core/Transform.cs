using System;
using System.Numerics;

namespace BlazorCanvas.Example11.Core
{
    public class Transform
    {
        public Vector2 Position;

        public Vector2 Scale;
        
        public float Rotation;

        public Vector2 GetDirection()
        {
            return new Vector2(-MathF.Sin(Rotation), MathF.Cos(Rotation));
        } 

        public void Clone(Transform source)
        {
            this.Position = source.Position;
            this.Scale = source.Scale;
            this.Rotation = source.Rotation;
        }

        public void Reset()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
        }

        public static Transform Identity() => new Transform()
        {
            Position = Vector2.Zero,
            Scale = Vector2.One,
            Rotation = 0f
        };
    }
}