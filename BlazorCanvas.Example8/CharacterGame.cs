using System;
using System.Numerics;
using System.Threading.Tasks;
using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorCanvas.Example8.Core;
using BlazorCanvas.Example8.Core.Animations;
using BlazorCanvas.Example8.Core.Components;

namespace BlazorCanvas.Example8
{
    public class CharacterGame : GameContext
    {
        private Canvas2DContext _context;
        private GameObject _warrior;

        private CharacterGame()
        {
        }

        public static async ValueTask<CharacterGame> Create(BECanvasComponent canvas, AnimationCollection animationCollection)
        {
            var warrior = new GameObject();

            var animation = animationCollection.GetAnimation("Idle");

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

            InitAnimationController(animationCollection, warrior);

            warrior.Components.Add(new CharacterBrain(animationCollection, warrior));

            var game = new CharacterGame {_context = await canvas.CreateCanvas2DAsync(), _warrior = warrior};

            return game;
        }

        private static void InitAnimationController(AnimationCollection animationCollection, GameObject warrior)
        {
            var animationController = new AnimationController(warrior);
            animationController.SetFloat("speed", 0f);
            animationController.SetBool("attacking", false);
            animationController.SetBool("jumping", false);

            warrior.Components.Add(animationController);

            var idle = new AnimationState(animationCollection.GetAnimation("Idle"));
            animationController.AddState(idle);

            var run = new AnimationState(animationCollection.GetAnimation("Run"));
            animationController.AddState(run);

            var jump = new AnimationState(animationCollection.GetAnimation("Jump"));
            animationController.AddState(jump);

            var attack = new AnimationState(animationCollection.GetAnimation("Attack1"));
            animationController.AddState(attack);

            idle.AddTransition(run,new Func<AnimationController, bool>[]
            {
                ctrl => ctrl.GetFloat("speed") > .1f
            });
            idle.AddTransition(attack, new Func<AnimationController, bool>[]
            {
                ctrl => ctrl.GetBool("attacking")
            });
            idle.AddTransition(jump, new Func<AnimationController, bool>[]
            {
                ctrl => ctrl.GetBool("jumping")
            });

            run.AddTransition(idle, new Func<AnimationController, bool>[]
            {
                ctrl => ctrl.GetFloat("speed") < .1f
            });
            run.AddTransition(attack, new Func<AnimationController, bool>[]
            {
                ctrl => ctrl.GetBool("attacking")
            });

            attack.AddTransition(idle, new Func<AnimationController, bool>[]
            {
                ctrl => !ctrl.GetBool("attacking")
            });

            jump.AddTransition(idle, new Func<AnimationController, bool>[]
            {
                ctrl => !ctrl.GetBool("jumping")
            });
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