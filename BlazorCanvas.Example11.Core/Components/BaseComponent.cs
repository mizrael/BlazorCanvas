using System;
using System.Threading.Tasks;

namespace BlazorCanvas.Example11.Core.Components
{
    public abstract class BaseComponent : IComponent
    {
        protected BaseComponent(GameObject owner)
        {
            this.Owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }
        
        //TODO: add an OnStart method

        public virtual async ValueTask Update(GameContext game)
        {
        }

        public GameObject Owner { get; }
    }
}