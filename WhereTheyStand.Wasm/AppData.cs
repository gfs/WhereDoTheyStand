using WhereTheyStand.Lib;

namespace WhereTheyStand.Wasm;

public class AppData
{
    public Dictionary<string, List<Donation>> candidateDict = new();
    public Dictionary<string, List<Donation>> pacDict = new();
    public Dictionary<string, Organization> pacMap = new();
    public Dictionary<string, Candidate> candidateMap = new();
}