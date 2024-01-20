using System.Diagnostics;
using System.IO;

namespace ExcelExport;

public class Program
{
    public static void Main(string[] args)
    {
        var persons = PersonExtension.GetRandomPersons();

        var excelFile = new FileInfo("Persons.xlsx");

        var excelService = new EPPlusService(excelFile);

        excelService.AddPersonsToExcelDocument(persons);
        excelService.SaveDocument();

        Process.Start(excelFile.FullName);
    }
}