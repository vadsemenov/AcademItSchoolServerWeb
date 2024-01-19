using Newtonsoft.Json;
using System.Net.Http;
using CountriesJson.DTO;
using static System.Net.WebRequestMethods;

namespace CountriesJson
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var url = @"https://restcountries.com/v2/region/americas";

            var countriesJson = HttpService.GetCountriesJson(url).WaitAsync(new TimeSpan(0, 0, 1, 0));

            var countries = JsonConvert.DeserializeObject<List<Country>>(countriesJson.Result);

            if (countries == null)
            {
                return;
            }

            var countriesPopulation = countries?
                .Select(c => c.Population)
                .Sum();

            Console.WriteLine($"Общая численость населения: {countriesPopulation} человек");

            var currencies = countries
                .Select(c => c.Currencies.Select(cu => cu.Name))
                .SelectMany(c => c)
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
