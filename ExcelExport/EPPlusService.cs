using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelExport;

public class EPPlusService : IDisposable
{
    private readonly ExcelPackage _excelDocument;

    private readonly ExcelWorksheet _excelWorksheet;

    public EPPlusService(FileInfo excelFile)
    {
        if (excelFile.Exists)
        {
            excelFile.Delete();
        }

        _excelDocument = new ExcelPackage(excelFile);

        _excelWorksheet = _excelDocument.Workbook.Worksheets.Add("Persons");
    }

    private void AddHeaderToPersonsTable()
    {
        _excelWorksheet.Cells[1, 1].Value = "#";
        _excelWorksheet.Cells[1, 2].Value = "First Name";
        _excelWorksheet.Cells[1, 3].Value = "Second Name";
        _excelWorksheet.Cells[1, 4].Value = "Age";
        _excelWorksheet.Cells[1, 5].Value = "Phone Number";
    }

    public void AddPersonsToExcelDocument(List<Person> persons)
    {
        AddHeaderToPersonsTable();

        var rowIndex = 1;

        foreach (var person in persons)
        {
            _excelWorksheet.Cells[rowIndex + 1, 1].Value = rowIndex;
            _excelWorksheet.Cells[rowIndex + 1, 2].Value = person.FirstName;
            _excelWorksheet.Cells[rowIndex + 1, 3].Value = person.SecondName;
            _excelWorksheet.Cells[rowIndex + 1, 4].Value = person.Age;
            _excelWorksheet.Cells[rowIndex + 1, 5].Value = person.PhoneNumber;

            rowIndex++;
        }

        FormatPersonsTable(rowIndex);
    }

    private void FormatPersonsTable(int rowsCount)
    {
        var headerStyle = _excelDocument.Workbook.Styles.CreateNamedStyle("HeaderStyle");

        headerStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
        headerStyle.Style.Fill.BackgroundColor.SetColor(Color.AntiqueWhite);
        headerStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        headerStyle.Style.Font.Size = 12;
        headerStyle.Style.Font.Bold = true;

        headerStyle.Style.Border.Top.Style = ExcelBorderStyle.Thick;
        headerStyle.Style.Border.Left.Style = ExcelBorderStyle.Thick;
        headerStyle.Style.Border.Right.Style = ExcelBorderStyle.Thick;
        headerStyle.Style.Border.Bottom.Style = ExcelBorderStyle.Thick;


        var bodyStyle = _excelDocument.Workbook.Styles.CreateNamedStyle("MainStyle");

        bodyStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
        bodyStyle.Style.Fill.BackgroundColor.SetColor(Color.Beige);
        bodyStyle.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        bodyStyle.Style.Font.Size = 12;
        bodyStyle.Style.Font.Italic = true;

        bodyStyle.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        bodyStyle.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        bodyStyle.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        bodyStyle.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;


        using var header = _excelWorksheet.Cells[1, 1, 1, 5];
        header.StyleName = headerStyle.Name;

        using var body = _excelWorksheet.Cells[2, 1, rowsCount, 5];
        body.StyleName = bodyStyle.Name;

        using var table = _excelWorksheet.Cells[1, 1, rowsCount, 5];
        table.AutoFitColumns();
    }

    public void SaveDocument()
    {
        _excelDocument.Save();
    }

    public void Dispose()
    {
        _excelDocument?.Dispose();
    }
}