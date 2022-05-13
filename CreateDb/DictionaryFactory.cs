using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using WhereTheyStand.Lib;

namespace CreateDb;

public static class DictionaryFactory
{
    /// <summary>
    /// Converts a folder of CSVs into an In-Memory Dictionary mapping Candidate to Donations
    /// </summary>
    /// <param name="pathToDonationFiles"></param>
    /// <returns></returns>
    public static (Dictionary<string, List<Donation>> pacDict, Dictionary<string, List<Donation>> userDict, Dictionary<string, Organization> pacMap, Dictionary<string, Candidate> candidateMap) GetDonationsFromFolder(string pathToDonationFiles)
    {
        var candiateAndIdFromFileName = new Regex("top-contributors-to-(.+)-(N[0-9]+).*");
        var candidateDict = new Dictionary<string, List<Donation>>();
        var pacDict = new Dictionary<string, List<Donation>>();
        var candidateMap = new Dictionary<string, Candidate>();
        var pacMap = new Dictionary<string, Organization>();
        var namesToIds = new Dictionary<string, string>();
        foreach (var candidateFile in Directory.EnumerateFiles(pathToDonationFiles))
        {
            var matches = candiateAndIdFromFileName.Matches(candidateFile);
            var candidateName = matches.First().Groups[1].Value;
            var candidateId = matches.First().Groups[2].Value;
            var candidate = new Candidate(candidateId, candidateName);
            using TextFieldParser parser = new TextFieldParser(candidateFile);
            var donationList = new List<Donation>();
            parser.Delimiters = new string[] { "," };
            parser.ReadFields(); // Skip header line
            while (true)
            {
                string[]? parts = parser.ReadFields();
                if (parts == null)
                {
                    break;
                }

                var org = new Organization(parts[0], parts[1], parts[2], parts[3]);
                var donation = new Donation(candidate, org, int.Parse(parts[4]), int.Parse(parts[5]),
                    int.Parse(parts[6]), DateTime.Parse(parts[7]), int.Parse(parts[8]));
                if (!string.IsNullOrEmpty(org.Pacid))
                {
                    if (!pacDict.ContainsKey(org.Pacid))
                    {
                        pacDict[org.Pacid] = new();
                    }
                    if (!pacMap.ContainsKey(org.Pacid))
                    {
                        pacMap.Add(org.Pacid, org);
                    }
                    pacDict[org.Pacid].Add(donation);
                    donationList.Add(donation);
                }
            }

            candidateMap.Add(candidate.id, candidate);
            candidateDict.Add(candidate.id, donationList);
        }

        return (pacDict, candidateDict, pacMap, candidateMap);
    }
}