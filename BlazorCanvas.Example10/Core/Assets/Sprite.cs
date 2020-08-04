using System.Drawing;
using Microsoft.AspNetCore.Components;

namespace BlazorCanvas.Example10.Core.Assets
{
    public class Sprite : IAsset
    {
        public Sprite(string name, ElementReference source, Size size, Point origin)
        {
            Name = name;
            Source = source;
            Size = size;
            Origin = origin;
        }

        public string Name { get; }
        public ElementReference Source { get; set; }
        public Size Size { get; }
        public Point Origin { get; }
    }
}