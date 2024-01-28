using Newtonsoft.Json;
using CountriesJson.DTO;

namespace CountriesJson
{
    public class Program
    {
        private static readonly HttpClient HttpClient = new();

        private const string Url = @"https://restcountries.com/v2/region/americas";

        public static void Main(string[] args)
        {
            HttpClient.Timeout = new TimeSpan(0, 0, 10, 0);

            var countriesJson = HttpClient.GetStringAsync(Url);

            var countries = JsonConvert.DeserializeObject<List<Country>>(countriesJson.Result);

            if (countries == null)
            {
                return;
            }

            var countriesPopulation = countries
                .Sum(c => c.Population);

            Console.WriteLine($"Общая численность населения: {countriesPopulation} человек");

            var currencies = countries
                .SelectMany(c => c.Currencies.Select(cu => cu.Name))
                .Distinct()
                .ToList();

            Console.WriteLine("Список всех валют:");
            foreach (var currency in currencies)
            {
                Console.WriteLine(currency);
            }

            Console.Read();
        }
    }
}
