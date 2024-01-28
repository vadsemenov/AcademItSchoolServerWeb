using System.Diagnostics;

namespace ExcelExport;

public class Program
{
    public static void Main(string[] args)
    {
        var persons = PersonExtensions.GetRandomPersons();

        var excelFile = new FileInfo("Persons.xlsx");

        var excelService = new EPPlusService(excelFile);

        excelService.AddPersonsToExcelDocument(persons);
        excelService.SaveDocument();

        Process.Start(new ProcessStartInfo
        {
            FileName = excelFile.FullName,
            UseShellExecute = true
        });
    }
}