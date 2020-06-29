using System;
using System.Numerics;
using System.Threading.Tasks;
using BlazorCanvas.Example4.Core;
using BlazorCanvas.Example4.Core.Components;

namespace BlazorCanvas.Example4
{
    public class LogoBrain : BaseComponent
    {
        private const float Speed = 0.25f;

        public LogoBrain(GameObject owner) : base(owner)
        {
        }

        public override async ValueTask Update(GameContext game)
        {
            var transform = this.Owner.Components.Get<Transform>();
            var spriteRenderer = this.Owner.Components.Get<SpriteRenderComponent>();

            var dx = transform.Direction.X;
            if (transform.Position.X + spriteRenderer.Sprite.Size.Width >= game.Display.Size.Width || transform.Position.X < 0)
                dx = -transform.Direction.X;

            var dy = transform.Direction.Y;
            if (transform.Position.Y + spriteRenderer.Sprite.Size.Height >= game.Display.Size.Height || transform.Position.Y < 0)
                dy = -transform.Direction.Y;

            var px = transform.Position.X;
            //if (transform.Position.X < 0)
            //    px = 0;
            //else if (transform.Position.X + spriteRenderer.Sprite.Size.Width >= game.Display.Size.Width)
            //    px = game.Display.Size.Width - spriteRenderer.Sprite.Size.Width;

            var py = transform.Position.Y;
            //if (transform.Position.Y < 0)
            //    py = 0;
            //else if (transform.Position.Y + spriteRenderer.Sprite.Size.Height >= game.Display.Size.Height)
            //    py = game.Display.Size.Height - spriteRenderer.Sprite.Size.Height;

            transform.Direction = new Vector2(dx, dy);

            transform.Position = transform.Direction * Speed * game.GameTime.ElapsedTime + new Vector2(px, py);
        }
    }
}