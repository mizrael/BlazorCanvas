using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example4.Core;
using BlazorCanvas.Example4.Core.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorCanvas.Example4
{
    public class LogoGame : GameContext
    {
        private Canvas2DContext _context;
        private GameObject _blazorLogo;

        private LogoGame()
        {
        }

        public static async ValueTask<LogoGame> Create(BECanvasComponent canvas, ElementReference spritesheet)
        {
            var blazorLogo = new GameObject();
            blazorLogo.Components.Add(new Transform(blazorLogo)
            {
                Position = Vector2.Zero,
                Direction = Vector2.One
            });

            var sprite = new Sprite()
            {
                Origin = Point.Empty,
                Size = new Size(200, 200),
                SpriteSheet = spritesheet
            };
            blazorLogo.Components.Add(new SpriteRenderComponent(sprite, blazorLogo));

            blazorLogo.Components.Add(new LogoBrain(blazorLogo));

            var game = new LogoGame {_context = await canvas.CreateCanvas2DAsync(), _blazorLogo = blazorLogo};

            return game;
        }

        protected override async ValueTask Update()
        {
            await _blazorLogo.Update(this);
        }

        protected override async ValueTask Render()
        {
            await _context.ClearRectAsync(0, 0, this.Display.Size.Width, this.Display.Size.Height);

            var spriteRenderer = _blazorLogo.Components.Get<SpriteRenderComponent>();
            await spriteRenderer.Render(_context);
        }
    }
}