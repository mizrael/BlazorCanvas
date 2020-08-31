using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorCanvas.Example8.Core.Exceptions;

namespace BlazorCanvas.Example8.Core.Components
{
    public class AnimationController : BaseComponent
    {
        private readonly IList<AnimationState> _states;
        private AnimationState _defaultState;
        private AnimationState _currentState;
        private readonly AnimatedSpriteRenderComponent _animationComponent;
        private readonly IDictionary<string, float> _floatParams;
        private readonly IDictionary<string, bool> _boolParams;

        public AnimationController(GameObject owner) : base(owner)
        {
            _states = new List<AnimationState>();
            _animationComponent = owner.Components.Get<AnimatedSpriteRenderComponent>() ??
                                  throw new ComponentNotFoundException<AnimatedSpriteRenderComponent>();

            _floatParams = new Dictionary<string, float>();
            _boolParams = new Dictionary<string, bool>();
        }


        public void AddState(AnimationState state)
        {
            if (!_states.Any())
                _defaultState = state;
            _states.Add(state);
        }

        public override async ValueTask Update(GameContext game)
        {
            if (null == _currentState)
            {
                _currentState = _defaultState;
                _currentState.Enter(_animationComponent);
            }

            await _currentState.Update(this);
        }

        public void SetCurrentState(AnimationState state)
        {
            _currentState = state;
            _currentState?.Enter(_animationComponent);
        }

        public float GetFloat(string name) => _floatParams[name];

        public void SetFloat(string name, float value)
        {
            if(!_floatParams.ContainsKey(name))
                _floatParams.Add(name, 0f);
            _floatParams[name] = value;
        }

        public void SetBool(string name, in bool value)
        {
            if (!_boolParams.ContainsKey(name))
                _boolParams.Add(name, false);
            _boolParams[name] = value;
        }

        public bool GetBool(string name) => _boolParams[name];
    }
}