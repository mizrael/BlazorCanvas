using System.Drawing;
using Microsoft.AspNetCore.Components;

namespace BlazorCanvas.Example5.Core
{
    public class Sprite
    {
        public Size Size { get; set; }
        public ElementReference SpriteSheet { get; set; }
    }
}