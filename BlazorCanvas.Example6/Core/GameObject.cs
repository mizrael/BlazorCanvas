using System.Threading.Tasks;
using BlazorCanvas.Example6.Core.Components;

namespace BlazorCanvas.Example6.Core
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