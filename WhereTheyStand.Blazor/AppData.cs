using CreateDb;

namespace WhereTheyStand.Blazor;

public class AppData
{
    public Dictionary<Candidate, List<Donation>> candidateDict = new Dictionary<Candidate, List<Donation>>();
    public Dictionary<Candidate, List<Donation>> pacDict = new Dictionary<Organization, List<Donation>>();
}