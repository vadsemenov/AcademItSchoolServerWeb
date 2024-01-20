using System.Collections.Generic;

namespace ExcelExport;

public static class PersonExtensions
{
    public static List<Person> GetRandomPersons() =>
        new()
        {
            new(15, "Vasiliy", "Vasiliev", "899993399"),
            new(20, "Ivan", "Ivanov", "834787787"),
            new(24, "Petr", "Petrov", "8445554545"),
            new(30, "Sergey", "Smirnov", "8232332323")
        };
}