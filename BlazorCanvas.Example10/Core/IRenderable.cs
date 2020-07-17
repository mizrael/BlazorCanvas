using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorCanvas.Example10.Core
{
    public interface IRenderable
    {
        ValueTask Render(GameContext game, Canvas2DContext context);
    }
}