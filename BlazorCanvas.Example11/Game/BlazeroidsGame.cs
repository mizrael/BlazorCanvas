using System;
using System.Drawing;
using System.Threading.Tasks;
using Blazor.Extensions;
using BlazorCanvas.Example11.Core;
using BlazorCanvas.Example11.Core.Assets;
using BlazorCanvas.Example11.Core.Components;
using BlazorCanvas.Example11.Game.Components;

namespace BlazorCanvas.Example11.Game
{
    public class BlazeroidsGame : GameContext
    {
        private readonly BECanvasComponent _canvas;
        private readonly IAssetsResolver _assetsResolver;
        
        public BlazeroidsGame(BECanvasComponent canvas, IAssetsResolver assetsResolver)
        {
            _canvas = canvas;
            _assetsResolver = assetsResolver;
        }

        protected override async ValueTask Init()
        {   
            this.AddService(new InputService());

            var collisionService = new CollisionService(this, new Size(64, 64));
            this.AddService(collisionService);
            
            var sceneGraph = new SceneGraph(this);
            this.AddService(sceneGraph);

            var player = BuildPlayer();
            sceneGraph.Root.AddChild(player);

            var rand = new Random();
            for (var i = 0; i != 6; ++i)
                AddAsteroid(sceneGraph, rand);

            var context = await _canvas.CreateCanvas2DAsync();
            var renderService = new RenderService(this, context);
            this.AddService(renderService);
        }

        private GameObject BuildPlayer()
        {
            var player = new GameObject();

            var spriteSheet = _assetsResolver.Get<SpriteSheet>("assets/sheet.json");
            var sprite = spriteSheet.Get("playerShip2_green.png");

            var playerTransform = player.Components.Add<TransformComponent>();

            playerTransform.Local.Position.X = _canvas.Width / 2;
            playerTransform.Local.Position.Y = _canvas.Height / 2;

            var playerSpriteRenderer = player.Components.Add<SpriteRenderComponent>();
            playerSpriteRenderer.Sprite = sprite;

            var bbox = player.Components.Add<BoundingBoxComponent>();
            bbox.SetSize(sprite.Bounds.Size);

            var rigidBody = player.Components.Add<MovingBody>();
            rigidBody.MaxSpeed = 400f;

            player.Components.Add<PlayerBrain>();
            
            return player;
        }

        private void AddAsteroid(SceneGraph sceneGraph, Random rand)
        {
            var asteroid = new GameObject();

            var spriteSheet = _assetsResolver.Get<SpriteSheet>("assets/sheet.json");
            var sprite = spriteSheet.Get("meteorBrown_big1.png");
            
            var transform = asteroid.Components.Add<TransformComponent>();

            var offset = .25f;
            var tx = ((float)rand.NextDouble() + offset) * 2 - 1;
            Console.WriteLine($"{tx} {_canvas.Width}");
            transform.Local.Position.X = tx * (float)_canvas.Width/2.5f + _canvas.Width/2;
            var ty = ((float)rand.NextDouble() + offset) * 2 - 1;
            transform.Local.Position.Y = ty * (float)_canvas.Height/2.5f + _canvas.Height/2;

            var spriteRenderer = asteroid.Components.Add<SpriteRenderComponent>();
            spriteRenderer.Sprite = sprite;
            
            var bbox = asteroid.Components.Add<BoundingBoxComponent>();
            bbox.SetSize(sprite.Bounds.Size);

            asteroid.Components.Add<AsteroidBrain>();

            sceneGraph.Root.AddChild(asteroid);
        }

    }
}