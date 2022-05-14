using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using WhereTheyStand.Lib;

namespace CreateDb;

public static class DataSetFactory
{
    /// <summary>
    /// Converts a folder of CSVs into an DataSet
    /// </summary>
    /// <param name="pathToDonationFiles"></param>
    /// <returns></returns>
    public static DataSet GetDonationsFromFolder(string pathToDonationFiles, DateOnly dateStamp, string description)
    {
        var candiateAndIdFromFileName = new Regex("top-contributors-to-(.+)-(N[0-9]+).*");
        var dataSet = new DataSet()
        {
            CollectionDate = dateStamp.ToDateTime(new TimeOnly()),
            Description = description
        };
        int donationNumber = 0;
        foreach (var candidateFile in Directory.EnumerateFiles(pathToDonationFiles))
        {
            var matches = candiateAndIdFromFileName.Matches(candidateFile);
            var candidateNameValue = matches.First().Groups[1].Value;
            var candidateIdValue = matches.First().Groups[2].Value;
            var candidate = new Candidate(candidateIdValue, candidateNameValue);
            var candidateId = new CandidateId(candidate.id);

            using TextFieldParser parser = new TextFieldParser(candidateFile);
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
                if (!dataSet.CandidateIdToCandidate.ContainsKey(candidateId))
                {
                    dataSet.CandidateIdToCandidate[candidateId] = candidate;
                }

                if (!string.IsNullOrEmpty(org.Pacid))
                {
                    var key = new OrganizationId(org.Pacid);
                    if (!dataSet.OrganizationIdToOrganization.ContainsKey(key))
                    {
                        dataSet.OrganizationIdToOrganization[key] = org;
                    }
                    if (!dataSet.OrganizationIdToDonationIds.ContainsKey(key))
                    {
                        dataSet.OrganizationIdToDonationIds[key] = new();
                    }
                    if (!dataSet.CandidateIdToDonationIds.ContainsKey(candidateId))
                    {
                        dataSet.CandidateIdToDonationIds[candidateId] = new();
                    }

                    var donationId = new DonationId(donationNumber.ToString());
                    donationNumber++;
                    dataSet.DonationIdToDonation[donationId] = donation;
                    dataSet.OrganizationIdToDonationIds[key].Add(donationId);
                    dataSet.CandidateIdToDonationIds[candidateId].Add(donationId);
                }
            }
        }

        return dataSet;
    }
}