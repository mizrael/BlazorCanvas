using System.Threading.Tasks;

namespace BlazorCanvas.Example8.Core.Components
{
    public interface IComponent
    {
        ValueTask Update(GameContext game);

        public GameObject Owner { get; }
    }
}