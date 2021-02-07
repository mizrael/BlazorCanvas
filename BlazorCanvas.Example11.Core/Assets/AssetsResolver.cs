using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using BlazorCanvas.Example11.Core.Assets.Loaders;

namespace BlazorCanvas.Example11.Core.Assets
{
    public class AssetsResolver : IAssetsResolver
    {
        private readonly ConcurrentDictionary<string, IAsset> _assets;
        private readonly IAssetLoaderFactory _assetLoaderFactory;

        public AssetsResolver(IAssetLoaderFactory assetLoaderFactory)
        {
            _assetLoaderFactory = assetLoaderFactory;
            _assets = new ConcurrentDictionary<string, IAsset>();
        }

        public async ValueTask<TA> Load<TA>(string path) where TA : IAsset
        {
            var loader = _assetLoaderFactory.Get<TA>();
            var asset = await loader.Load(path);

            if (null == asset)
                throw new TypeLoadException($"unable to load asset type '{typeof(TA)}' from path '{path}'"); 
            
            _assets.AddOrUpdate(path, k => asset, (k, v) => asset);
            return asset;
        }

        public TA Get<TA>(string path) where TA : class, IAsset => _assets[path] as TA;
    }
}