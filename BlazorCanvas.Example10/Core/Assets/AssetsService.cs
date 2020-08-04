using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.AspNetCore.Components;

namespace BlazorCanvas.Example10.Core.Assets
{
    public class AssetsService : IAssetsService
    {
        private readonly Dictionary<string, IAsset> _assets;

        private AssetsService()
        {
            _assets = new Dictionary<string, IAsset>();
        }

        private static readonly Lazy<AssetsService> _instance = new Lazy<AssetsService>(new AssetsService());
        public static AssetsService Instance => _instance.Value;
        
        public TA Load<TA>(string path) where TA : IAsset
        {
            IAsset asset = null;

            if (typeof(TA) == typeof(Sprite))
            {
                var elementRef = new ElementReference(Guid.NewGuid().ToString());
                asset = new Sprite(path, elementRef, new Size(112,75), Point.Empty);
            }

            if (null == asset)
                throw new TypeLoadException($"invalid asset type: {typeof(TA)}"); 
            
            _assets.Add(path, asset);
            return (TA) asset;
        }

        public TA Get<TA>(string name) where TA : class, IAsset => _assets[name] as TA;
    }
}