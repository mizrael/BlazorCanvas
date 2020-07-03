using System.Numerics;
using System.Threading.Tasks;
using BlazorCanvas.Example8.Core;
using BlazorCanvas.Example8.Core.Components;
using BlazorCanvas.Example8.Core.Exceptions;

namespace BlazorCanvas.Example8
{
    public class CharacterBrain : BaseComponent
    {
        private readonly Transform _transform;
        private readonly AnimatedSpriteRenderComponent _animationComponent;

        public CharacterBrain(AnimationsSet animationsSet, GameObject owner) : base(owner)
        {
            _transform = owner.Components.Get<Transform>() ??
                         throw new ComponentNotFoundException<Transform>();

            _animationComponent = owner.Components.Get<AnimatedSpriteRenderComponent>() ?? 
                                  throw new ComponentNotFoundException<AnimatedSpriteRenderComponent>();
        }

        public override async ValueTask Update(GameContext game)
        {
            var right = InputSystem.Instance.GetKeyState(Keys.Right);
            var left = InputSystem.Instance.GetKeyState(Keys.Left);
            var space = InputSystem.Instance.GetKeyState(Keys.Space);
            var up = InputSystem.Instance.GetKeyState(Keys.Up);

            if (space.State == ButtonState.States.Down)
            {
                _animationComponent.Animation = _animationComponent.Animation.Set.GetAnimation("Attack1");
            }
            else if (up.State == ButtonState.States.Down)
            {
                _animationComponent.Animation = _animationComponent.Animation.Set.GetAnimation("Jump");
            }
            else if (right.State == ButtonState.States.Down)
            {
                _transform.Direction = Vector2.UnitX;
                _animationComponent.Animation = _animationComponent.Animation.Set.GetAnimation("Run");
            }
            else if (left.State == ButtonState.States.Down)
            {
                _transform.Direction = -Vector2.UnitX;
                _animationComponent.Animation = _animationComponent.Animation.Set.GetAnimation("Run");
            }
            else 
                _animationComponent.Animation = _animationComponent.Animation.Set.GetAnimation("Idle");
        }
    }
}