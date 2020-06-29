using System;
using System.Numerics;
using System.Threading.Tasks;
using BlazorCanvas.Example5.Core;
using BlazorCanvas.Example5.Core.Components;

namespace BlazorCanvas.Example5
{
    public class LogoBrain : BaseComponent
    {
        private const float Speed = 0.25f;

        private readonly SpriteRenderComponent _renderComponent;
        private readonly Transform _transform;

        public LogoBrain(GameObject owner) : base(owner)
        {
            _transform = owner.Components.Get<Transform>() ?? throw new ArgumentOutOfRangeException(nameof(Transform));
            _renderComponent = owner.Components.Get<SpriteRenderComponent>() ?? throw new ArgumentOutOfRangeException(nameof(SpriteRenderComponent));
        }

        public override async ValueTask Update(GameContext game)
        {
            UpdatePosition(game);

            _renderComponent.DrawBoundingBox = _transform.BoundingBox.Contains(InputSystem.Instance.Coords);
        }

        private void UpdatePosition(GameContext game)
        {
            var transform = this.Owner.Components.Get<Transform>();
            var spriteRenderer = this.Owner.Components.Get<SpriteRenderComponent>();

            var dx = transform.Direction.X;
            if (transform.Position.X + spriteRenderer.Sprite.Size.Width >= game.Display.Size.Width || transform.Position.X < 0)
                dx = -transform.Direction.X;

            var dy = transform.Direction.Y;
            if (transform.Position.Y + spriteRenderer.Sprite.Size.Height >= game.Display.Size.Height ||
                transform.Position.Y < 0)
                dy = -transform.Direction.Y;

            transform.Direction = new Vector2(dx, dy);

            transform.Position += transform.Direction * Speed * game.GameTime.ElapsedTime;
        }
    }
}