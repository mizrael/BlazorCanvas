using System.Threading.Tasks;
using BlazorCanvas.Example9.Core.Components;

namespace BlazorCanvas.Example9.Core
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