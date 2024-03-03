using System.Diagnostics;

namespace ExcelExport2;

public class Program
{
    public static void Main(string[] args)
    {
        var persons = PersonExtensions.GetRandomPersons();

        var excelFile = new FileInfo("Persons.xlsx");

        var excelService = new ClosedXmlService(excelFile);

        excelService.AddPersonsToExcelDocument(persons);
        excelService.SaveDocument();

        Process.Start(new ProcessStartInfo
        {
            FileName = excelFile.FullName,
            UseShellExecute = true
        });
    }
}