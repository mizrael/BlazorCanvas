using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example11.Core.Assets;

namespace BlazorCanvas.Example11.Core.Components
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
            if (!this.Owner.Enabled)
                return;
            
            await context.SaveAsync();

            await context.TranslateAsync(_transform.World.Position.X, _transform.World.Position.Y);
            await context.RotateAsync(_transform.World.Rotation);
            await context.ScaleAsync(_transform.World.Scale.X, _transform.World.Scale.Y);
            
            await context.DrawImageAsync(Sprite.ElementRef, 
                Sprite.Bounds.X, Sprite.Bounds.Y,
                Sprite.Bounds.Width, Sprite.Bounds.Height,
                Sprite.Origin.X, Sprite.Origin.Y,
                -Sprite.Bounds.Width, -Sprite.Bounds.Height);

            await context.RestoreAsync();
        }

        public SpriteBase Sprite { get; set; }
    }
}