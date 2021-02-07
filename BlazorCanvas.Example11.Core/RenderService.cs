using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorCanvas.Example11.Core
{
    public class RenderService : IGameService
    {
        private readonly GameContext _game;
        private readonly Canvas2DContext _context;

        public RenderService(GameContext game, Canvas2DContext context)
        {
            _game = game;
            _context = context;
        }

        public async ValueTask Step()
        {
            var sceneGraph = _game.GetService<SceneGraph>();
            
            await _context.ClearRectAsync(0, 0, _game.Display.Size.Width, _game.Display.Size.Height);

            await _context.BeginBatchAsync();
            await Render(sceneGraph.Root, _game);
            await _context.EndBatchAsync();
        }

        private async ValueTask Render(GameObject node, GameContext game)
        {
            if (null == node)
                return;

            foreach (var component in node.Components)
                if (component is IRenderable renderable)
                    await renderable.Render(game, _context);

            foreach (var child in node.Children)
                await Render(child, game);
        }
    }
}