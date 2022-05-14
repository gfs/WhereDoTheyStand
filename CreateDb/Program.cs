// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using CreateDb;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

var hr1011 =
    DataSetFactory.GetDonationsFromFolder(
        "C:\\Users\\Gstoc\\Desktop\\WhereTheyStand\\WhereTheyStand\\CreateDb\\HR1011",
        new DateOnly(2022, 5, 12),
        "Donations to Co-Sponsors of HR 1011 (2021).");

File.WriteAllText("HR1011.json",JsonConvert.SerializeObject(hr1011));