using System;
using System.Threading.Tasks;

namespace BlazorCanvas.Example10.Core.Components
{
    public abstract class BaseComponent : IComponent
    {
        protected BaseComponent(GameObject owner)
        {
            this.Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            this.Owner.Components?.Add(this);
        }

        public virtual async ValueTask Update(GameContext game)
        {
        }

        public GameObject Owner { get; }
    }
}