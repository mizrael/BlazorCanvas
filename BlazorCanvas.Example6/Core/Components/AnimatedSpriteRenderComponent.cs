using System;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorCanvas.Example6.Core.Components
{
    public class AnimatedSpriteRenderComponent : BaseComponent
    {
        private readonly Transform _transform;

        private int _currFrameIndex = 0;
        private int _currFramePosX = 0;
        private float _lastUpdate = 0f;

        public AnimatedSpriteRenderComponent(GameObject owner) : base(owner)
        {
            _transform = owner.Components.Get<Transform>() ??
                         throw new AccessViolationException("Transform component is required");
        }

        public async ValueTask Render(GameContext game, Canvas2DContext context)
        {
            if (null == Animation)
                return;
            
            if (game.GameTime.TotalTime - _lastUpdate > 1000f / Animation.Fps)
            {
                ++_currFrameIndex;
                _lastUpdate = game.GameTime.TotalTime;
                _currFramePosX = (_currFrameIndex % Animation.FramesCount) * Animation.FrameSize.Width;
            }

            await context.DrawImageAsync(Animation.ImageRef, _currFramePosX, 0,
                Animation.FrameSize.Width, Animation.FrameSize.Height,
                _transform.Position.X, _transform.Position.Y,
                Animation.FrameSize.Width, Animation.FrameSize.Height);
        }

        public AnimationsSet.Animation Animation { get; set; }
    }
}