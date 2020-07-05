using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example8.Core.Animations;
using BlazorCanvas.Example8.Core.Exceptions;

namespace BlazorCanvas.Example8.Core.Components
{
    public class AnimatedSpriteRenderComponent : BaseComponent
    {
        private readonly Transform _transform;

        private int _currFrameIndex = 0;
        private int _currFramePosX = 0;
        private float _lastUpdate = 0f;
        private bool _completed = false;
        private AnimationCollection.Animation _animation;

        public AnimatedSpriteRenderComponent(GameObject owner) : base(owner)
        {
            _transform = owner.Components.Get<Transform>() ??
                         throw new ComponentNotFoundException<Transform>();
        }

        public async ValueTask Render(GameContext game, Canvas2DContext context)
        {
            if (null == Animation)
                return;
            
            if (game.GameTime.TotalTime - _lastUpdate > 1000f / Animation.Fps)
            {
                if (_currFrameIndex >= Animation.FramesCount)
                {
                    _completed = true;
                    _currFrameIndex = 0;
                }
                    
                _lastUpdate = game.GameTime.TotalTime;
                _currFramePosX = _currFrameIndex * Animation.FrameSize.Width;
                ++_currFrameIndex;
            }

            var dx = -(_transform.Direction.X-1f) * Animation.FrameSize.Width / 2f;
            await context.SetTransformAsync(_transform.Direction.X, 0, 0, 1, dx, 0);

            await context.DrawImageAsync(Animation.ImageRef, _currFramePosX, 0,
                Animation.FrameSize.Width, Animation.FrameSize.Height,
                _transform.Position.X, _transform.Position.Y,
                Animation.FrameSize.Width, Animation.FrameSize.Height);
        }

        public AnimationCollection.Animation Animation
        {
            get => _animation;
            set
            {
                if (_animation == value)
                    return;
                _completed = false;
                _currFrameIndex = 0;
                _animation = value;
            }
        }
    }
}