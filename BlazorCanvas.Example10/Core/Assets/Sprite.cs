using System;
using System.Drawing;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace BlazorCanvas.Example10.Core.Assets
{
    public class Sprite : IAsset
    {
        public Sprite(string name, ElementReference source, Size size, byte[] data, ImageFormat format)
        {
            Name = name;
            Source = source;
            Size = size;
            Data = data;
            Format = format;

            
        }

        public string Name { get; }
        public ElementReference Source { get; set; }
        public Size Size { get; }
        public byte[] Data { get; }
        public ImageFormat Format { get; }
        public Point Origin { get; set; }
    }
}