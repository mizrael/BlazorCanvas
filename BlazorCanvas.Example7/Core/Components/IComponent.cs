using System.Threading.Tasks;

namespace BlazorCanvas.Example7.Core.Components
{
    public interface IComponent
    {
        ValueTask Update(GameContext game);

        public GameObject Owner { get; }
    }
}