using System;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorCanvas.Example5.Core.Components
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
            await context.DrawImageAsync(Sprite.SpriteSheet, _transform.Position.X, _transform.Position.Y,
                Sprite.Size.Width, Sprite.Size.Height);

            if (DrawBoundingBox)
            {
                await context.BeginPathAsync();
                await context.SetStrokeStyleAsync($"rgb(255,255,0)");
                await context.SetLineWidthAsync(3);
                await context.StrokeRectAsync(_transform.BoundingBox.X, _transform.BoundingBox.Y,
                    _transform.BoundingBox.Width,
                    _transform.BoundingBox.Height);
            }
        }

        public Sprite Sprite { get; }

        public bool DrawBoundingBox { get; set; } = false;
    }
}