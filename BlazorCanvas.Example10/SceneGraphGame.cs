using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example10.Core;
using BlazorCanvas.Example10.Core.Components;
using BlazorCanvas.Example10.Core.Content;

namespace BlazorCanvas.Example10
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

        public static async ValueTask<SceneGraphGame> Create(BECanvasComponent canvas, IDictionary<string, AnimationCollection> animations)
        {
            var context = await canvas.CreateCanvas2DAsync();
            var game = new SceneGraphGame(context);

            CreatePlanets(canvas, animations, game._sceneGraph);

            var fpsCounter = new GameObject();
            fpsCounter.Components.Add(new FPSCounterComponent(fpsCounter));
            game._sceneGraph.Root.AddChild(fpsCounter);

            return game;
        }

        private static void CreatePlanets(BECanvasComponent canvas, 
            IDictionary<string, AnimationCollection> animations,
            SceneGraph sceneGraph)
        {
            var sun = new GameObject();
            var sunTransform = new TransformComponent(sun);
            sunTransform.Local.Position.X = canvas.Width / 2;
            sunTransform.Local.Position.Y = canvas.Height / 2;
            sunTransform.Local.Scale = new Vector2(1.5f);
            sun.Components.Add(sunTransform);
            sun.Components.Add(new AnimatedSpriteRenderComponent(sun)
            {
                Animation = animations["planet2"].GetAnimation("planet2")
            });
            sceneGraph.Root.AddChild(sun);

            var planet = new GameObject();
            planet.Components.Add(new TransformComponent(planet));
            planet.Components.Add(new OrbitComponent(planet, new Vector2(300f, 200f), 0.0015f));
            planet.Components.Add(new AnimatedSpriteRenderComponent(planet)
            {
                Animation = animations["planet1"].GetAnimation("planet1")
            });
            sun.AddChild(planet);

            var satellite = new GameObject();
            var satelliteTransform = new TransformComponent(satellite);
            satelliteTransform.Local.Scale = new Vector2(0.65f);
            satellite.Components.Add(satelliteTransform);
            satellite.Components.Add(new OrbitComponent(satellite, new Vector2(100f, 100f), 0.0035f));
            satellite.Components.Add(new AnimatedSpriteRenderComponent(satellite)
            {
                Animation = animations["planet2"].GetAnimation("planet2")
            });
            planet.AddChild(satellite);

            var planet2 = new GameObject();
            planet2.Components.Add(new TransformComponent(planet2));
            planet2.Components.Add(new OrbitComponent(planet2, new Vector2(600f, 200f), 0.0020f));
            planet2.Components.Add(new AnimatedSpriteRenderComponent(planet2)
            {
                Animation = animations["planet1"].GetAnimation("planet1")
            });
            sun.AddChild(planet2);
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