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
            var game = new LogoGame();

            game._context = await canvas.CreateCanvas2DAsync();

            game._blazorLogo = new GameObject();
            game._blazorLogo.Components.Add(new Transform(game._blazorLogo)
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
            game._blazorLogo.Components.Add(new SpriteRenderComponent(sprite, game._blazorLogo));

            game._blazorLogo.Components.Add(new LogoBrain(game._blazorLogo));

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