using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace BlazorCanvas.Example6.Core
{
    public class AnimationsSet
    {
        private IDictionary<string, Animation> _animations;

        public AnimationsSet(string name, IEnumerable<Animation> animations)
        {
            
            this.Name = name;
            _animations = (animations ?? Enumerable.Empty<Animation>()).ToDictionary(a => a.Name);
        }

        public string Name { get; }

        public Animation GetAnimation(string name) => string.IsNullOrWhiteSpace(name) || !_animations.ContainsKey(name) ? null : _animations[name];

        public class Animation
        {
            public Animation(string name, int fps, int framesCount, Size frameSize, 
                ElementReference imageRef, string imageData, Size imageSize)
            {
                Name = name;
                this.Fps = fps;
                FrameSize = frameSize;
                ImageRef = imageRef;
                ImageData = imageData;
                FramesCount = framesCount;
                ImageSize = imageSize;
            }

            public string Name { get; }
            public int Fps { get; }
            public int FramesCount { get; }
            public Size FrameSize { get; }
            public Size ImageSize { get; }
            public ElementReference ImageRef { get; set; }
            public string ImageData { get; }
        }
    }
}