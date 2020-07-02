using System.Threading.Tasks;
using BlazorCanvas.Example7.Core.Components;

namespace BlazorCanvas.Example7.Core
{
    public class GameObject
    {
        public ComponentsCollection Components { get; } = new ComponentsCollection();

        public async ValueTask Update(GameContext game)
        {
            foreach (var component in this.Components)
                await component.Update(game);
        }
    }
}