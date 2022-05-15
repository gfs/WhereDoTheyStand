using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace WhereTheyStand.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };
            var appData = new AppData(httpClient);

            builder.Services.AddSingleton<AppData>(appData);
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            await builder.Build().RunAsync();
        }
    }
}