using System;
using System.Drawing;
using System.Threading.Tasks;
using Blazor.Extensions;
using BlazorCanvas.Example11.Core;
using BlazorCanvas.Example11.Core.Assets;
using BlazorCanvas.Example11.Core.Components;
using BlazorCanvas.Example11.Game.Components;
using BlazorCanvas.Example11.Core.Utils;

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

            for (var i = 0; i != 6; ++i)
                AddAsteroid(sceneGraph);

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

        private void AddAsteroid(SceneGraph sceneGraph)
        {
            var asteroid = new GameObject();

            var spriteSheet = _assetsResolver.Get<SpriteSheet>("assets/sheet.json");
            var sprite = spriteSheet.Get("meteorBrown_big1.png");
            
            var transform = asteroid.Components.Add<TransformComponent>();

            var offset = .25f;
            
            var w = (float)_canvas.Width;
            var rx = MathUtils.Random.NextDouble(offset, 1.0);
            var tx = (float)MathUtils.Normalize(rx, 0, 1, -1, 1);
            transform.Local.Position.X = tx * w/2f + w/2f;

            var h = (float)_canvas.Height;
            var ry = MathUtils.Random.NextDouble(offset, 1.0);
            var ty = (float)MathUtils.Normalize(ry, 0, 1, -1, 1);
            transform.Local.Position.Y = ty * h/2f + h/2f;

            var spriteRenderer = asteroid.Components.Add<SpriteRenderComponent>();
            spriteRenderer.Sprite = sprite;
            
            var bbox = asteroid.Components.Add<BoundingBoxComponent>();
            bbox.SetSize(sprite.Bounds.Size);

            asteroid.Components.Add<AsteroidBrain>();

            sceneGraph.Root.AddChild(asteroid);
        }

    }
}