using System.Threading.Tasks;

namespace BlazorCanvas.Example10.Core.Assets
{
    public interface IAssetsService
    {
        ValueTask<TA> Load<TA>(string path) where TA : IAsset;
        TA Get<TA>(string name) where TA : class, IAsset;
    }
}