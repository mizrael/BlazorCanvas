using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorCanvas.Example8.Core.Animations;

namespace BlazorCanvas.Example8.Core.Components
{
    public class AnimationState
    {
        private readonly List<Transition> _transitions;
        private readonly AnimationCollection.Animation _animation;

        public AnimationState(AnimationCollection.Animation animation)
        {
            _animation = animation ?? throw new ArgumentNullException(nameof(animation));
            _transitions = new List<Transition>();
        }

        public void AddTransition(AnimationState to, IEnumerable<Func<AnimationController, bool>> conditions) =>
            _transitions.Add(new Transition(to, conditions));

        public async ValueTask Update(AnimationController controller)
        {
            var transition = _transitions.FirstOrDefault(t => t.Check(controller));
            if(null != transition)
                controller.SetCurrentState(transition.To);
        }

        public void Enter(AnimatedSpriteRenderComponent animationComponent) =>
            animationComponent.Animation = _animation;

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