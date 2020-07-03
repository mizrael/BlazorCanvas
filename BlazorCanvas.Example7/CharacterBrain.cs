using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlazorCanvas.Example7.Core;
using BlazorCanvas.Example7.Core.Components;
using BlazorCanvas.Example7.Core.Exceptions;

namespace BlazorCanvas.Example7
{
    public class CharacterBrain : BaseComponent
    {
        private readonly AnimatedSpriteRenderComponent _animationComponent;

        public CharacterBrain(AnimationsSet animationsSet, GameObject owner) : base(owner)
        {
            _animationComponent = owner.Components.Get<AnimatedSpriteRenderComponent>() ?? throw new ComponentNotFoundException<AnimatedSpriteRenderComponent>();
        }

        public override async ValueTask Update(GameContext game)
        {
            var keyState = InputSystem.Instance.GetKeyState(Keys.Right);

            if (keyState.State == ButtonState.States.Down && !keyState.WasPressed)
                _animationComponent.Animation = _animationComponent.Animation.Set.GetAnimation("Run");
            
            else if (keyState.State == ButtonState.States.Up && keyState.WasPressed)
                _animationComponent.Animation = _animationComponent.Animation.Set.GetAnimation("Idle");
        }
    }
}