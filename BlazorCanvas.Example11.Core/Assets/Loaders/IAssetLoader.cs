using System.Threading.Tasks;

namespace BlazorCanvas.Example11.Core.Assets.Loaders
{
    public interface IAssetLoader<TA> where TA : IAsset
    {
        ValueTask<TA> Load(string path);
    }
}