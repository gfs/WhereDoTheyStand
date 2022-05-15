// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using CreateDb;
using Microsoft.VisualBasic.FileIO;
using WhereTheyStand.Lib;
using JsonSerializer = System.Text.Json.JsonSerializer;

var hr1011 =
    DataSetFactory.GetDonationsFromFolder(
        "C:\\Users\\Gstoc\\Documents\\GitHub\\WhereDoTheyStand\\CreateDb\\HR1011",
        new DateOnly(2022, 5, 12),
        "Donations to Co-Sponsors of HR 1011 (2021).");

var serialized = JsonSerializer.Serialize(hr1011.ToSerializableDataSet());
var deserialized = JsonSerializer.Deserialize<SerializableDataSet>(serialized);
var resultSet = new DataSet(deserialized);
File.WriteAllText("HR1011.json", serialized);