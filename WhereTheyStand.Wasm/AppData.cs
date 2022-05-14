using System.Net.Http.Json;
using System.Reflection;
using WhereTheyStand.Lib;

namespace WhereTheyStand.Wasm;

public class AppData
{
    public bool IsLoaded { get; set; }
    public DataSet CurrentDataSet { get; set; }

    public AppData(HttpClient? client)
    {
        this.client = client;
    }
    
    HttpClient client = new HttpClient();

    public async Task LoadData(string? location = "DataSets/HR1011.json")
    {
        CurrentDataSet = await client.GetFromJsonAsync<DataSet>(location) ?? new DataSet();
        IsLoaded = true;
    }
}