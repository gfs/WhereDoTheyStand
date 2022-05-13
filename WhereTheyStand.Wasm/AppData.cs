using Newtonsoft.Json;
using System.Reflection;
using WhereTheyStand.Lib;

namespace WhereTheyStand.Wasm;

public class AppData
{
    public bool IsLoaded { get; set; }
    public Dictionary<string, List<Donation>> candidateDict = new();
    public Dictionary<string, List<Donation>> pacDict = new();
    public Dictionary<string, Organization> pacMap = new();
    public Dictionary<string, Candidate> candidateMap = new();

    public AppData()
    {
        
    }
    
    public async Task LoadData()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var candidateText = await new StreamReader(assembly.GetManifestResourceStream("WhereTheyStand.Wasm.Candidates.json")).ReadToEndAsync();
        var candidateMapText = await new StreamReader(assembly.GetManifestResourceStream("WhereTheyStand.Wasm.CandiateMap.json")).ReadToEndAsync();
        var pacText = await new StreamReader(assembly.GetManifestResourceStream("WhereTheyStand.Wasm.SortedOrgs.json")).ReadToEndAsync();
        var pacMapText = await new StreamReader(assembly.GetManifestResourceStream("WhereTheyStand.Wasm.PacMap.json")).ReadToEndAsync();

        candidateDict = JsonConvert.DeserializeObject<Dictionary<string, List<Lib.Donation>>>(candidateText);
        pacDict = JsonConvert.DeserializeObject<Dictionary<string, List<Lib.Donation>>>(pacText);
        candidateMap = JsonConvert.DeserializeObject<Dictionary<string, Lib.Candidate>>(candidateMapText);
        pacMap = JsonConvert.DeserializeObject<Dictionary<string, Lib.Organization>>(pacMapText);
        IsLoaded = true;
    }
}