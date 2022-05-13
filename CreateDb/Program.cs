// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using CreateDb;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

var hr1011 =
    DictionaryFactory.GetDonationsFromFolder(
        "C:\\Users\\Gstoc\\Desktop\\WhereTheyStand\\WhereTheyStand\\CreateDb\\HR1011");

File.WriteAllText("SortedOrgs.json",JsonConvert.SerializeObject(hr1011.pacDict));
File.WriteAllText("Candidates.json", JsonConvert.SerializeObject(hr1011.userDict));
File.WriteAllText("CandiateMap.json", JsonConvert.SerializeObject(hr1011.candidateMap));
File.WriteAllText("PacMap.json", JsonConvert.SerializeObject(hr1011.pacMap));