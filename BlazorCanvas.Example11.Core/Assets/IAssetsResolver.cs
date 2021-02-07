using System.Threading.Tasks;

namespace BlazorCanvas.Example11.Core.Assets
{
    public interface IAssetsResolver
    {
        ValueTask<TA> Load<TA>(string path) where TA : IAsset;
        TA Get<TA>(string path) where TA : class, IAsset;
    }
}