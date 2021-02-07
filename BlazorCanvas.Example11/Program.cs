using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorCanvas.Example11.Core.Assets;
using BlazorCanvas.Example11.Core.Assets.Loaders;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorCanvas.Example11
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IAssetsResolver, AssetsResolver>();
            builder.Services.AddSingleton<IAssetLoader<Sprite>, SpriteAssetLoader>();
            builder.Services.AddSingleton<IAssetLoader<SpriteSheet>, SpriteSheetAssetLoader>();
            builder.Services.AddSingleton<IAssetLoaderFactory>(ctx =>
            {
                var factory = new AssetLoaderFactory();
                
                factory.Register(ctx.GetRequiredService<IAssetLoader<Sprite>>());
                factory.Register(ctx.GetRequiredService<IAssetLoader<SpriteSheet>>());

                return factory;
            });

            await builder.Build().RunAsync();
        }
    }
}
