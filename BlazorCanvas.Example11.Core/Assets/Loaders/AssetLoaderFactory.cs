using System;
using System.Collections.Generic;

namespace BlazorCanvas.Example11.Core.Assets.Loaders
{
    public class AssetLoaderFactory : IAssetLoaderFactory
    {
        private readonly IDictionary<Type, object> _loaders;

        public AssetLoaderFactory()
        {
            _loaders = new Dictionary<Type, object>();
        }

        public void Register<TA>(IAssetLoader<TA> loader) where TA : IAsset
        {
            var type = typeof(TA);
            if (!_loaders.ContainsKey(type)) _loaders.Add(type, null);
            _loaders[type] = loader;
        }

        public IAssetLoader<TA> Get<TA>() where TA : IAsset
        {
            var type = typeof(TA);
            if(!_loaders.ContainsKey(type))
                throw new ArgumentOutOfRangeException($"invalid asset type: {type.FullName}");

            return _loaders[type] as IAssetLoader<TA>;
        }
    }
}