using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorCanvas.Example10.Core.Assets;
using BlazorCanvas.Example10.Core.Assets.Loaders;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorCanvas.Example10
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IAssetsService, AssetsResolver>();
            builder.Services.AddSingleton<IAssetLoader<Sprite>, SpriteAssetLoader>();
            builder.Services.AddSingleton<IAssetLoaderFactory>(ctx =>
            {
                var factory = new AssetLoaderFactory();
                var spriteLoader = ctx.GetService<IAssetLoader<Sprite>>();
                factory.Register(spriteLoader);
                return factory;
            });

            await builder.Build().RunAsync();
        }
    }
}
