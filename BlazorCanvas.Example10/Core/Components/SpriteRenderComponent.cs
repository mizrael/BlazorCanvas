using System;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example10.Core.Assets;

namespace BlazorCanvas.Example10.Core.Components
{
    public class SpriteRenderComponent : BaseComponent, IRenderable
    {
        private readonly TransformComponent _transform;

        public SpriteRenderComponent(Sprite sprite, GameObject owner) : base(owner)
        {
            Sprite = sprite ?? throw new ArgumentNullException(nameof(sprite));

            _transform = owner.Components.Get<TransformComponent>() ??
                         throw new AccessViolationException("Transform component is required");
        }

        public async ValueTask Render(GameContext game, Canvas2DContext context)
        {
            await context.DrawImageAsync(Sprite.Source, _transform.World.Position.X, _transform.World.Position.Y,
                Sprite.Size.Width, Sprite.Size.Height);
        }

        public Sprite Sprite { get; }
    }
}