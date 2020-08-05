using System;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example10.Core.Assets;

namespace BlazorCanvas.Example10.Core.Components
{
    public class SpriteRenderComponent : BaseComponent, IRenderable
    {
        private readonly TransformComponent _transform;

        private SpriteRenderComponent(GameObject owner) : base(owner)
        {
            _transform = owner.Components.Get<TransformComponent>();
        }

        public async ValueTask Render(GameContext game, Canvas2DContext context)
        {
            await context.SaveAsync();

            await context.TranslateAsync(_transform.World.Position.X, _transform.World.Position.Y);
            await context.RotateAsync(_transform.World.Rotation);
            
            await context.DrawImageAsync(Sprite.Source, -Sprite.Origin.X, -Sprite.Origin.Y ,
                Sprite.Size.Width, Sprite.Size.Height);
            
            await context.RestoreAsync();
        }

        public Sprite Sprite { get; set; }
    }
}