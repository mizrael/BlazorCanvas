using System.Numerics;
using System.Threading.Tasks;
using BlazorCanvas.Example8.Core;
using BlazorCanvas.Example8.Core.Animations;
using BlazorCanvas.Example8.Core.Components;
using BlazorCanvas.Example8.Core.Exceptions;

namespace BlazorCanvas.Example8
{
    public class CharacterBrain : BaseComponent
    {
        private readonly Transform _transform;
        private readonly AnimationController _animationController;

        public CharacterBrain(AnimationCollection animationCollection, GameObject owner) : base(owner)
        {
            _transform = owner.Components.Get<Transform>() ??
                         throw new ComponentNotFoundException<Transform>();

            _animationController = owner.Components.Get<AnimationController>() ?? 
                                  throw new ComponentNotFoundException<AnimationController>();
        }

        public override async ValueTask Update(GameContext game)
        {
            var right = InputSystem.Instance.GetKeyState(Keys.Right);
            var left = InputSystem.Instance.GetKeyState(Keys.Left);
            var space = InputSystem.Instance.GetKeyState(Keys.Space);
            var up = InputSystem.Instance.GetKeyState(Keys.Up);

            var isAttacking = (space.State == ButtonState.States.Down);
            var isJumping = (up.State == ButtonState.States.Down);
            var speed = 0f;

            if (right.State == ButtonState.States.Down)
            {
                _transform.Direction = Vector2.UnitX;
                speed = 1f;
            }

            if (left.State == ButtonState.States.Down)
            {
                _transform.Direction = -Vector2.UnitX;
                speed = 1f;
            }

            _animationController.SetBool("attacking", isAttacking);
            _animationController.SetBool("jumping", isJumping);
            _animationController.SetFloat("speed", speed);
        }
    }
}