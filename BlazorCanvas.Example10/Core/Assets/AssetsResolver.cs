using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorCanvas.Example10.Core.Assets
{
    public class AssetsResolver : IAssetsService
    {
        private readonly ConcurrentDictionary<string, IAsset> _assets;
        private readonly HttpClient _httpClient;

        public AssetsResolver(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _assets = new ConcurrentDictionary<string, IAsset>();
        }

        public async ValueTask<TA> Load<TA>(string path) where TA : IAsset
        {
            IAsset asset = null;

            if (typeof(TA) == typeof(Sprite))
            {
                var bytes = await _httpClient.GetByteArrayAsync(path);
                await using var stream = new MemoryStream(bytes);
                using var image = await SixLabors.ImageSharp.Image.LoadAsync(stream);
                var size = new Size(image.Width, image.Height);
                
                var elementRef = new ElementReference(Guid.NewGuid().ToString());
                asset = new Sprite(path, elementRef, size, bytes, ImageFormatUtils.FromPath(path));
            }

            if (null == asset)
                throw new TypeLoadException($"invalid asset type: {typeof(TA)}"); 
            
            _assets.AddOrUpdate(path, k => asset, (k, v) => asset);
            return (TA) asset;
        }

        public TA Get<TA>(string name) where TA : class, IAsset => _assets[name] as TA;
    }
}