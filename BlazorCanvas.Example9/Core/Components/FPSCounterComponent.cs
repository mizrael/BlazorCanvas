using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorCanvas.Example9.Core.Components
{
    public class FPSCounterComponent : BaseComponent, IRenderable
    {
        public FPSCounterComponent(GameObject owner) : base(owner)
        {
        }

        public async ValueTask Render(GameContext game, Canvas2DContext context)
        {
            var fps = 1000f / game.GameTime.ElapsedMilliseconds;

            await context.SetFillStyleAsync("green");
            await context.FillRectAsync(10, 50, 400, 100);

            await context.SetFontAsync("24px verdana");
            await context.StrokeTextAsync($"Total game time (s): {game.GameTime.TotalMilliseconds / 1000}", 20, 80);
            await context.StrokeTextAsync($"Frame time (ms): {game.GameTime.ElapsedMilliseconds}", 20, 110);
            await context.StrokeTextAsync($"FPS: {fps:###}", 20, 140);
        }

    }
}