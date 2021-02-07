using System.Threading.Tasks;

namespace BlazorCanvas.Example11.Core
{
    public interface IGameService
    {
        ValueTask Step();
    }
}