using System.Threading.Tasks;

namespace BlazorCanvas.Example11.Core.Components
{
    public interface IComponent
    {
        ValueTask Update(GameContext game);

        GameObject Owner { get; }
    }
}