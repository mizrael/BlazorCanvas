using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example6.Core;
using BlazorCanvas.Example6.Core.Components;

namespace BlazorCanvas.Example6
{
    public class LogoGame : GameContext
    {
        private Canvas2DContext _context;
        private GameObject _blazorLogo;

        private LogoGame()
        {
        }

        public static async ValueTask<LogoGame> Create(BECanvasComponent canvas, AnimationsSet animationsSet)
        {
            var blazorLogo = new GameObject();

            var animation = animationsSet.GetAnimation("Idle");
            blazorLogo.Components.Add(new Transform(blazorLogo)
            {
                Position = Vector2.Zero,
                Direction = Vector2.One,
                Size = animation.FrameSize
            });

            blazorLogo.Components.Add(new AnimatedSpriteRenderComponent(blazorLogo));

            blazorLogo.Components.Add(new LogoBrain(animationsSet, blazorLogo));

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

            var spriteRenderer = _blazorLogo.Components.Get<AnimatedSpriteRenderComponent>();
            await spriteRenderer.Render(this, _context);
        }
    }
}