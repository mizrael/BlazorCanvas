using System;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorCanvas.Example4.Core.Components
{
    public class SpriteRenderComponent : BaseComponent
    {
        private readonly Transform _transform;

        public SpriteRenderComponent(Sprite sprite, GameObject owner) : base(owner)
        {
            Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));

            _transform = owner.Components.Get<Transform>() ??
                         throw new AccessViolationException("Transform component is required");
        }

        public async ValueTask Render(Canvas2DContext context)
        {
            await context.DrawImageAsync(Sprite.SpriteSheet, _transform.Position.X, _transform.Position.Y, Sprite.Size.Width, Sprite.Size.Height);
        }

        public Sprite Sprite { get; }
    }
}