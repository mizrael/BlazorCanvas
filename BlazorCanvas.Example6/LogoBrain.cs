using BlazorCanvas.Example6.Core;
using BlazorCanvas.Example6.Core.Components;

namespace BlazorCanvas.Example6
{
    public class LogoBrain : BaseComponent
    {
        public LogoBrain(AnimationsSet animationsSet, GameObject owner) : base(owner)
        {
            var renderComponent = owner.Components.Get<AnimatedSpriteRenderComponent>();
            renderComponent.Animation = animationsSet.GetAnimation("Run");
        }
    }
}