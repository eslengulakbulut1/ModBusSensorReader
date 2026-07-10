using ClosedXML.Excel;
using ModbusSensorReader.Library.Models;

namespace ModbusSensorReader.Library.Services;

public class ExcelExportServices
{
    // Excel'e aktarılacak kayıtları alır ve belirtilen dosya yoluna kaydeder.
    public void Export(List<ExcelRecord> records, string filePath)
    {
        if (records == null || records.Count == 0)
            throw new ArgumentException("Aktarılacak kayıt bulunamadı.");

        using XLWorkbook workbook = new XLWorkbook();

        IXLWorksheet sheet =
            workbook.Worksheets.Add("Sensör Verileri");

        // Excel'de gösterilecek benzersiz parametreler.
        List<string> parameterNames = records
            .Select(record => record.ParameterName)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        // Her parametrenin birimini bulur.
        Dictionary<string, string> parameterUnits = records
            .GroupBy(
                record => record.ParameterName,
                StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(record => record.Unit)
                    .FirstOrDefault() ?? "",
                StringComparer.OrdinalIgnoreCase);

        int column = 1;

        // Sabit başlıklar
        sheet.Cell(1, column++).Value = "Tarih";
        sheet.Cell(1, column++).Value = "Saat";
        sheet.Cell(1, column++).Value = "Sensör";
        sheet.Cell(1, column++).Value = "Slave ID";

        // Her parametre için Ham ve Hesaplanan Değer sütunları
        foreach (string parameterName in parameterNames)
        {
            string unit = parameterUnits[parameterName];

            sheet.Cell(1, column++).Value =
                $"{parameterName} Ham";

            sheet.Cell(1, column++).Value =
                string.IsNullOrWhiteSpace(unit)
                    ? parameterName
                    : $"{parameterName} ({unit})";
        }

        // Aynı okuma turuna ait parametreleri tek satırda toplar.
        var readingGroups = records
            .GroupBy(record => new
            {
                record.ReadingTime,
                record.SensorName,
                record.SlaveId
            })
            .OrderBy(group => group.Key.ReadingTime)
            .ToList();

        int row = 2;

        foreach (var readingGroup in readingGroups)
        {
            column = 1;

            sheet.Cell(row, column++).Value =
                readingGroup.Key.ReadingTime.Date;

            sheet.Cell(row, column++).Value =
                readingGroup.Key.ReadingTime.TimeOfDay;

            sheet.Cell(row, column++).Value =
                readingGroup.Key.SensorName;

            sheet.Cell(row, column++).Value =
                readingGroup.Key.SlaveId;

            foreach (string parameterName in parameterNames)
            {
                ExcelRecord? parameterRecord =
                    readingGroup.FirstOrDefault(record =>
                        record.ParameterName.Equals(
                            parameterName,
                            StringComparison.OrdinalIgnoreCase));

                if (parameterRecord != null)
                {
                    sheet.Cell(row, column++).Value =
                        parameterRecord.RawValue;

                    sheet.Cell(row, column++).Value =
                        parameterRecord.CalculatedValue;
                }
                else
                {
                    sheet.Cell(row, column++).Value = "";
                    sheet.Cell(row, column++).Value = "";
                }
            }

            row++;
        }

        FormatWorksheet(
            sheet,
            row - 1,
            column - 1);

        workbook.SaveAs(filePath);
    }

    // Excel'e kaydedilirken hangi formatta kayıt edileceğini belirler.
    private void FormatWorksheet(IXLWorksheet sheet, int lastRow, int lastColumn)
    {
        IXLRange headerRange =
            sheet.Range(1, 1, 1, lastColumn);

        // Başlık biçimi
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Font.FontColor = XLColor.White;

        headerRange.Style.Fill.BackgroundColor =
            XLColor.FromHtml("#2F75B5");

        headerRange.Style.Alignment.Horizontal =
            XLAlignmentHorizontalValues.Center;

        headerRange.Style.Alignment.Vertical =
            XLAlignmentVerticalValues.Center;

        headerRange.Style.Border.OutsideBorder =
            XLBorderStyleValues.Thin;

        headerRange.Style.Border.InsideBorder =
            XLBorderStyleValues.Thin;

        // Veri alanı
        IXLRange dataRange =
            sheet.Range(2, 1, lastRow, lastColumn);

        dataRange.Style.Border.OutsideBorder =
            XLBorderStyleValues.Thin;

        dataRange.Style.Border.InsideBorder =
            XLBorderStyleValues.Hair;

        dataRange.Style.Alignment.Vertical =
            XLAlignmentVerticalValues.Center;

        // Alternatif satır rengi
        for (int row = 2; row <= lastRow; row++)
        {
            if (row % 2 == 0)
            {
                sheet.Range(row, 1, row, lastColumn)
                    .Style.Fill.BackgroundColor =
                    XLColor.FromHtml("#EAF2F8");
            }
        }

        // Tarih ve saat formatları
        sheet.Column(1).Style.DateFormat.Format =
            "dd.MM.yyyy";

        sheet.Column(2).Style.DateFormat.Format =
            "HH:mm:ss";

        // Sayısal değerleri ortalar
        if (lastColumn >= 4)
        {
            sheet.Range(2, 4, lastRow, lastColumn)
                .Style.Alignment.Horizontal =
                XLAlignmentHorizontalValues.Center;
        }

        sheet.SheetView.FreezeRows(1);

        sheet.Range(1, 1, lastRow, lastColumn)
            .SetAutoFilter();

        sheet.Columns().AdjustToContents();

        sheet.Row(1).Height = 26;

        // Aşırı geniş sütunları sınırlar
        foreach (IXLColumn column in sheet.ColumnsUsed())
        {
            if (column.Width > 25)
                column.Width = 25;
        }
    }
}