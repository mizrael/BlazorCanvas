using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using BlazorCanvas.Example10.Core.Assets.Loaders;
using Microsoft.Extensions.Logging;

namespace BlazorCanvas.Example10.Core.Assets
{
    public class AssetsResolver : IAssetsService
    {
        private readonly ConcurrentDictionary<string, IAsset> _assets;
        private readonly IAssetLoaderFactory _assetLoaderFactory;
        private readonly ILogger<AssetsResolver> _logger;

        public AssetsResolver(IAssetLoaderFactory assetLoaderFactory, ILogger<AssetsResolver> logger)
        {
            _assetLoaderFactory = assetLoaderFactory;
            _logger = logger;
            _assets = new ConcurrentDictionary<string, IAsset>();
        }

        public async ValueTask<TA> Load<TA>(string path) where TA : IAsset
        {
            _logger.LogInformation($"loading asset from path: {path}");

            var loader = _assetLoaderFactory.Get<TA>();
            var asset = await loader.Load(path);

            if (null == asset)
                throw new TypeLoadException($"unable to load asset type '{typeof(TA)}' from path '{path}'"); 
            
            _assets.AddOrUpdate(path, k => asset, (k, v) => asset);
            return asset;
        }

        public TA Get<TA>(string name) where TA : class, IAsset => _assets[name] as TA;
    }
}