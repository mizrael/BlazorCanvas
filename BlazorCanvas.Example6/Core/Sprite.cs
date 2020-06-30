using System.Drawing;
using Microsoft.AspNetCore.Components;

namespace BlazorCanvas.Example6.Core
{
    public class Sprite
    {
        public Size Size { get; set; }
        public Point Origin { get; set; }
        public ElementReference SpriteSheet { get; set; }
    }

    //public class AnimatedSprite
    //{
    //    public Size Size { get; set; }
    //    public string Name { get; set; }
    //    public int FPS { get; set; }
    //}

    //public class AnimationFrames
    //{
    //    public string Name { get; set; }
    //    public Bitmap
    //}
}