using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorCanvas.Example9.Core.Animations;

namespace BlazorCanvas.Example9.Core.Components
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
            _animationComponent = owner.Components.Get<AnimatedSpriteRenderComponent>();

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

    public class AnimationState
    {
        private readonly List<Transition> _transitions;
        private readonly AnimationCollection.Animation _animation;

        public AnimationState(AnimationCollection.Animation animation)
        {
            _animation = animation ?? throw new ArgumentNullException(nameof(animation));
            _transitions = new List<Transition>();
        }

        public void AddTransition(AnimationState to, IEnumerable<Func<AnimationController, bool>> conditions)
        {
            _transitions.Add(new Transition(to, conditions));
        }

        public async ValueTask Update(AnimationController controller)
        {
            var transition = _transitions.FirstOrDefault(t => t.Check(controller));
            if(null != transition)
                controller.SetCurrentState(transition.To);
        }

        public void Enter(AnimatedSpriteRenderComponent animationComponent)
        {
            animationComponent.Animation = _animation;
        }

        private class Transition
        {
            private readonly IEnumerable<Func<AnimationController, bool>> _conditions;

            public Transition(AnimationState to, IEnumerable<Func<AnimationController, bool>> conditions)
            {
                To = to;
                _conditions = conditions ?? Enumerable.Empty<Func<AnimationController, bool>>();
            }

            public bool Check(AnimationController controller)
            {
                return _conditions.Any(c => c(controller));
            }

            public AnimationState To { get; }
        }
    }
}