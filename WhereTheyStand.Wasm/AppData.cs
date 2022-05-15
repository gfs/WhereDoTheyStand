using System.Diagnostics;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using WhereTheyStand.Lib;

namespace WhereTheyStand.Wasm;

public class AppData
{
    public bool IsLoaded { get; set; }
    public DataSet CurrentDataSet { get; set; } = new DataSet();
    public Stopwatch stopwatch { get; set; } = new();
    public AppData(HttpClient? client)
    {
        stopwatch.Start();
        this.client = client;
    }
    
    HttpClient client = new HttpClient();

    public async Task LoadData(string? location = "DataSets/HR1011.json")
    {
        var fetched = JsonSerializer.Deserialize<SerializableDataSet>(await client.GetStringAsync(location));
        if (fetched is null)
        {
            Console.WriteLine("Failed to fetch dataset.");
        }
        else
        {
            CurrentDataSet = new DataSet(fetched);
            IsLoaded = true;
        }
    }
}