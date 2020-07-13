using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example9.Core.Animations;
using BlazorCanvas.Example9.Core.Exceptions;

namespace BlazorCanvas.Example9.Core.Components
{
    public class AnimatedSpriteRenderComponent : BaseComponent, IRenderable
    {
        private readonly TransformComponent _transform;

        private int _currFramePosX = 0;
        private int _currFramePosY = 0;
        private int _currFrameIndex = 0;
        private long _lastUpdate = 0;
        
        private AnimationCollection.Animation _animation;

        public AnimatedSpriteRenderComponent(GameObject owner) : base(owner)
        {
            _transform = owner.Components.Get<TransformComponent>() ??
                         throw new ComponentNotFoundException<TransformComponent>();
        }

        public async ValueTask Render(GameContext game, Canvas2DContext context)
        {
            if (null == Animation)
                return;
            
            if (game.GameTime.TotalMilliseconds - _lastUpdate > 1000f/Animation.Fps)
            {
                _lastUpdate = game.GameTime.TotalMilliseconds;

                _currFramePosX += Animation.FrameSize.Width;
                if (_currFramePosX >= Animation.ImageSize.Width)
                {
                    _currFramePosX = 0;
                    _currFramePosY += Animation.FrameSize.Height;
                }

                if (_currFramePosY >= Animation.ImageSize.Height)
                    _currFramePosY = 0;

                _currFrameIndex++;
                if(_currFrameIndex >= Animation.FramesCount)
                    _currFrameIndex = _currFramePosX = _currFramePosY = 0;
            }

            await context.SaveAsync();

            var flip = (_transform.Transform.Direction.X < 0f);

            await context.TranslateAsync(_transform.Transform.Position.X + (flip ? Animation.FrameSize.Width : 0f), _transform.Transform.Position.Y);
            await context.ScaleAsync(_transform.Transform.Direction.X, 1f);

            await context.DrawImageAsync(Animation.ImageRef, 
                _currFramePosX, _currFramePosY,
                Animation.FrameSize.Width, Animation.FrameSize.Height,
                0,0, 
                Animation.FrameSize.Width, Animation.FrameSize.Height);

            await context.RestoreAsync();
        }

        public AnimationCollection.Animation Animation
        {
            get => _animation;
            set
            {
                if (_animation == value)
                    return;
                _currFramePosX = _currFramePosY = 0;
                _animation = value;
            }
        }
    }
}