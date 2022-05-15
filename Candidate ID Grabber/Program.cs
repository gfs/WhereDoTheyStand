using System.Text.RegularExpressions;

namespace Candidate_ID_Grabber
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            Regex Candidate_ID_Regex = new Regex("/members-of-congress/([a-z\\-]*)/summary\\?cid=(N[0-9]*)");
            Dictionary<string, string> CandidateIds = new Dictionary<string, string>();
            using HttpClient client = new HttpClient();
            // Read the file and display it line by line.
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\ryanr\Downloads\opensecrets_members_of_congress_117.txt"))
            {
                MatchCollection matches = Candidate_ID_Regex.Matches(line);
                if (matches.Count > 0)
                {
                    CandidateIds.Add(matches.First().Groups[2].Value, matches.First().Groups[1].Value);
                }
            }

            Directory.CreateDirectory("Data");

            foreach (var candidateID in CandidateIds)
            {
                var url = $"https://www.opensecrets.org/members-of-congress/contributors.csv?cid={candidateID.Key}&cycle=2022&recs=100&type=C";
                var candidateCsv = await client.GetStringAsync(url);
                // Wait 3000ms between fetches
                File.WriteAllText(Path.Combine("Data", $"{candidateID.Value}-{candidateID.Key}-{DateTime.Now:yyyy-MM-dd}.csv"), candidateCsv);
                Thread.Sleep(3000);
                
            }
            System.Console.WriteLine("There were {0} IDs.", CandidateIds.Count);

          
        }
    }
}