namespace CountriesJson;

public static class HttpService
{
    public static async Task<string> GetCountriesJson(string url)
    {
        var httpClient = new HttpClient();

        using Stream stream = await httpClient.GetStreamAsync(url);
        var reader = new StreamReader(stream);

        var json = await reader.ReadToEndAsync();

        return json;
    }
}