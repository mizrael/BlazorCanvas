using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example10.Core;
using BlazorCanvas.Example10.Core.Assets;
using BlazorCanvas.Example10.Core.Components;

namespace BlazorCanvas.Example10
{
    public class AssetsGame : GameContext
    {
        private readonly Canvas2DContext _context;
        private readonly SceneGraph _sceneGraph;

        private AssetsGame(Canvas2DContext context)
        {
            _context = context;
            _sceneGraph = new SceneGraph();
        }

        public static async ValueTask<AssetsGame> Create(BECanvasComponent canvas)
        {
            var context = await canvas.CreateCanvas2DAsync();
            var game = new AssetsGame(context);

            var fpsCounter = new GameObject();
            fpsCounter.Components.Add(new FPSCounterComponent(fpsCounter));
            game._sceneGraph.Root.AddChild(fpsCounter);

            var player = new GameObject();
            var playerTransform = new TransformComponent(player);
            playerTransform.Local.Position.X = canvas.Width / 2;
            playerTransform.Local.Position.Y = canvas.Height / 2;
            player.Components.Add(playerTransform);
            var playerSprite = AssetsService.Instance.Get<Sprite>("/assets/playerShip2_green.png");
            player.Components.Add(new SpriteRenderComponent(playerSprite, player));
            game._sceneGraph.Root.AddChild(player);

            return game;
        }

        protected override async ValueTask Update()
        {
            await _sceneGraph.Update(this);
        }

        protected override async ValueTask Render()
        {
            await _context.ClearRectAsync(0, 0, this.Display.Size.Width, this.Display.Size.Height);
            
            await Render(_sceneGraph.Root);
        }

        private async ValueTask Render(GameObject node)
        {
            if (null == node)
                return;

            foreach(var component in node.Components)
                if (component is IRenderable renderable)
                    await renderable.Render(this, _context);

            foreach (var child in node.Children)
                await Render(child);
        }
    }
}