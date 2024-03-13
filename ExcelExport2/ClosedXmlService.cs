using ClosedXML.Excel;

namespace ExcelExport2;

public class ClosedXmlService : IDisposable
{
    private readonly XLWorkbook _excelWorkBook;

    private readonly IXLWorksheet _excelWorksheet;

    private const int PersonTableColumnsCount = 5;

    private readonly string _filePath;

    public ClosedXmlService(FileSystemInfo excelFile)
    {
        if (excelFile.Exists)
        {
            excelFile.Delete();
        }

        _filePath = excelFile.FullName;

        _excelWorkBook = new XLWorkbook();

        _excelWorksheet = _excelWorkBook.Worksheets.Add("Persons");
    }

    private void AddHeaderToPersonsTable()
    {
        _excelWorksheet.Cell(1, 1).Value = "#";
        _excelWorksheet.Cell(1, 2).Value = "First Name";
        _excelWorksheet.Cell(1, 3).Value = "Second Name";
        _excelWorksheet.Cell(1, 4).Value = "Age";
        _excelWorksheet.Cell(1, 5).Value = "Phone Number";
    }

    public void AddPersonsToExcelDocument(List<Person>? persons, int rowIndex = 2)
    {
        if (persons == null)
        {
            throw new ArgumentNullException(nameof(persons));
        }

        AddHeaderToPersonsTable();

        foreach (var person in persons)
        {
            _excelWorksheet.Cell(rowIndex, 1).Value = (rowIndex - 1).ToString();
            _excelWorksheet.Cell(rowIndex, 2).Value = person.FirstName;
            _excelWorksheet.Cell(rowIndex, 3).Value = person.SecondName;
            _excelWorksheet.Cell(rowIndex, 4).Value = person.Age;
            _excelWorksheet.Cell(rowIndex, 5).Value = person.PhoneNumber;

            rowIndex++;
        }

        FormatPersonsTable(rowIndex);
    }

    private void FormatPersonsTable(int rowsCount)
    {
        var header = _excelWorksheet.Range(1, 1, 1, PersonTableColumnsCount);

        header.Style.Fill.PatternType = XLFillPatternValues.Solid;
        header.Style.Fill.SetBackgroundColor(XLColor.AntiFlashWhite);
        header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        header.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        header.Style.Font.FontSize = 12;
        header.Style.Font.Bold = true;

        header.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        header.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

        var body = _excelWorksheet.Range(2, 1, rowsCount, PersonTableColumnsCount);

        body.Style.Fill.PatternType = XLFillPatternValues.Solid;
        body.Style.Fill.SetBackgroundColor(XLColor.Beige);
        body.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        body.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        body.Style.Font.FontSize = 12;
        body.Style.Font.Italic = true;

        body.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        body.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

        _excelWorksheet.Columns().AdjustToContents();
    }

    public void SaveDocument()
    {
        _excelWorkBook.SaveAs(_filePath);
    }

    public void Dispose()
    {
        _excelWorkBook.Dispose();

        GC.SuppressFinalize(this);
    }
}