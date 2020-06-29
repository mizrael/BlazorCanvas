using System.Threading.Tasks;

namespace BlazorCanvas.Example4.Core.Components
{
    public interface IComponent
    {
        ValueTask Update(GameContext game);

        public GameObject Owner { get; }
    }
}