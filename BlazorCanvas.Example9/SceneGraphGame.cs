using System;
using System.Numerics;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example9.Core;
using BlazorCanvas.Example9.Core.Animations;
using BlazorCanvas.Example9.Core.Components;

namespace BlazorCanvas.Example9
{
    public class SceneGraphGame : GameContext
    {
        private readonly Canvas2DContext _context;
        private readonly SceneGraph _sceneGraph;

        private SceneGraphGame(Canvas2DContext context)
        {
            _context = context;
            _sceneGraph = new SceneGraph();
        }

        public static async ValueTask<SceneGraphGame> Create(BECanvasComponent canvas, AnimationCollection planet1Animations)
        {
            var context = await canvas.CreateCanvas2DAsync();
            var game = new SceneGraphGame(context);

            CreatePlanets(canvas, planet1Animations, game._sceneGraph);

            var fpsCounter = new GameObject();
            fpsCounter.Components.Add(new FPSCounterComponent(fpsCounter));
            game._sceneGraph.Root.AddChild(fpsCounter);

            return game;
        }

        private static void CreatePlanets(BECanvasComponent canvas, AnimationCollection planet1Animations,
            SceneGraph sceneGraph)
        {
            var planetAnim = planet1Animations.GetAnimation("planet1");

            var planet = new GameObject();
            var planetTransform = new TransformComponent(planet);
            planetTransform.Local.Position.X = (canvas.Width - planetAnim.FrameSize.Width) / 2;
            planetTransform.Local.Position.Y = (canvas.Height - planetAnim.FrameSize.Height) / 2;
            planet.Components.Add(planetTransform);
            planet.Components.Add(new AnimatedSpriteRenderComponent(planet)
            {
                Animation = planetAnim
            });
            sceneGraph.Root.AddChild(planet);

            var satellite = new GameObject();
            var satelliteTransform = new TransformComponent(satellite);
            satelliteTransform.Local.Position.X = 100;
            satelliteTransform.Local.Position.Y = 100;
            satelliteTransform.Local.Scale = new Vector2(0.5f);
            satellite.Components.Add(satelliteTransform);
            satellite.Components.Add(new OrbitComponent(satellite));
            satellite.Components.Add(new AnimatedSpriteRenderComponent(satellite)
            {
                Animation = planetAnim
            });
            planet.AddChild(satellite);
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