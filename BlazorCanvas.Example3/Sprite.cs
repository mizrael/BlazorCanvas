using System.Drawing;
using Microsoft.AspNetCore.Components;

namespace BlazorCanvas.Example3
{
    public class Sprite
    {
        public Size Size { get; set; }
        public ElementReference SpriteSheet { get; set; }
    }
}