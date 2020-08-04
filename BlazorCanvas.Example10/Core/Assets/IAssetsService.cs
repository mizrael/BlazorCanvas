namespace BlazorCanvas.Example10.Core.Assets
{
    public interface IAssetsService
    {
        TA Load<TA>(string path) where TA : IAsset;
    }
}