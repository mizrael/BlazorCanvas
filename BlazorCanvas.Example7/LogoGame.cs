using System.Numerics;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example7.Core;
using BlazorCanvas.Example7.Core.Components;

namespace BlazorCanvas.Example7
{
    public class LogoGame : GameContext
    {
        private Canvas2DContext _context;
        private GameObject _warrior;

        private LogoGame()
        {
        }

        public static async ValueTask<LogoGame> Create(BECanvasComponent canvas, AnimationsSet animationsSet)
        {
            var warrior = new GameObject();

            var animation = animationsSet.GetAnimation("Idle");

            warrior.Components.Add(new Transform(warrior)
            {
                Position = Vector2.Zero,
                Direction = Vector2.One,
                Size = animation.FrameSize
            });

            warrior.Components.Add(new AnimatedSpriteRenderComponent(warrior)
            {
                Animation = animation
            });

            warrior.Components.Add(new CharacterBrain(animationsSet, warrior));

            var game = new LogoGame {_context = await canvas.CreateCanvas2DAsync(), _warrior = warrior};

            return game;
        }

        protected override async ValueTask Update()
        {
            await _warrior.Update(this);
        }

        protected override async ValueTask Render()
        {
            await _context.ClearRectAsync(0, 0, this.Display.Size.Width, this.Display.Size.Height);

            var spriteRenderer = _warrior.Components.Get<AnimatedSpriteRenderComponent>();
            await spriteRenderer.Render(this, _context);
        }
    }
}