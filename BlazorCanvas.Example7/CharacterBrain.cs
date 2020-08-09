using System.Numerics;
using System.Threading.Tasks;
using BlazorCanvas.Example7.Core;
using BlazorCanvas.Example7.Core.Components;
using BlazorCanvas.Example7.Core.Exceptions;

namespace BlazorCanvas.Example7
{
    public class CharacterBrain : BaseComponent
    {
        private readonly Transform _transform;
        private readonly AnimatedSpriteRenderComponent _animationComponent;
        private readonly AnimationsSet _animationsSet;

        public CharacterBrain(AnimationsSet animationsSet, GameObject owner) : base(owner)
        {
            _transform = owner.Components.Get<Transform>() ??
                         throw new ComponentNotFoundException<Transform>();

            _animationComponent = owner.Components.Get<AnimatedSpriteRenderComponent>() ?? 
                                  throw new ComponentNotFoundException<AnimatedSpriteRenderComponent>();
            _animationsSet = animationsSet;
        }

        public override async ValueTask Update(GameContext game)
        {
            var right = InputSystem.Instance.GetKeyState(Keys.Right);
            var left = InputSystem.Instance.GetKeyState(Keys.Left);

            if (right.State == ButtonState.States.Down)
            {
                _transform.Direction = Vector2.UnitX;
                _animationComponent.Animation = _animationsSet.GetAnimation("Run");
            }
            else if (left.State == ButtonState.States.Down)
            {
                _transform.Direction = -Vector2.UnitX;
                _animationComponent.Animation = _animationsSet.GetAnimation("Run");
            }
            else if (right.State == ButtonState.States.Up)
                _animationComponent.Animation = _animationsSet.GetAnimation("Idle");
            else if (left.State == ButtonState.States.Up)
                _animationComponent.Animation = _animationsSet.GetAnimation("Idle");
        }
    }
}