using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Newtonsoft.Json;
using System.Reflection;
using WhereTheyStand.Wasm;

namespace WhereTheyStand.Wasm
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine(string.Join(',', assembly.GetManifestResourceNames()));
            var candidateText = new StreamReader(assembly.GetManifestResourceStream("WhereTheyStand.Wasm.Candidates.json")).ReadToEnd();
            var candidateMap = new StreamReader(assembly.GetManifestResourceStream("WhereTheyStand.Wasm.CandiateMap.json")).ReadToEnd();
            var pacText = new StreamReader(assembly.GetManifestResourceStream("WhereTheyStand.Wasm.SortedOrgs.json")).ReadToEnd();
            var pacMap = new StreamReader(assembly.GetManifestResourceStream("WhereTheyStand.Wasm.PacMap.json")).ReadToEnd();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            var appData = new AppData();

            appData.candidateDict = JsonConvert.DeserializeObject<Dictionary<string, List<Lib.Donation>>>(candidateText);
            appData.pacDict = JsonConvert.DeserializeObject<Dictionary<string, List<Lib.Donation>>>(pacText);
            appData.candidateMap = JsonConvert.DeserializeObject<Dictionary<string, Lib.Candidate>>(candidateMap);
            appData.pacMap = JsonConvert.DeserializeObject<Dictionary<string, Lib.Organization>>(pacMap);

            builder.Services.AddSingleton<AppData>(appData);
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}