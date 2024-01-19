namespace CountriesJson.DTO;

public class Country
{
    public string Name { get; set; } = null!;

    public string[] TopLevelDomain { get; set; } = null!;

    public string SubRegion { get; set; } = null!;

    public string Region { get; set; } = null!;

    public int Population { get; set; }

    public Currency[] Currencies { get; set; } = null!;
}