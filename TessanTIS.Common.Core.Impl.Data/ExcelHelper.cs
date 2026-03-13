using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using TessanTIS.Common.Core.Abstraction;
using TessanTIS.Common.Core.Impl.Configuration;

namespace TessanTIS.Common.Core.Impl.Data
{
    public class ExcelHelper : IDataHelper
    {
        private readonly ILoggerHelper logger;

        public ExcelHelper()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            logger = LoggerHelper.GetInstance();
        }

        public IDictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
        public IDictionary<int, IDictionary<string, string>> Datas { get; set; } =
            new Dictionary<int, IDictionary<string, string>>();

        public void LoadData(string filePath, string spreadsheet)
        {
            if (File.Exists(filePath))
            {
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        var result = reader.AsDataSet(
                            new ExcelDataSetConfiguration()
                            {
                                UseColumnDataType = true,
                                ConfigureDataTable = (_) =>
                                    new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            }
                        );
                        DataTable table = result.Tables[spreadsheet];
                        if (table != null)
                        {
                            for (int i = 1; i <= table.Rows.Count; i++)
                            {
                                Datas.Add(i, new Dictionary<string, string>());
                                foreach (DataColumn column in table.Columns)
                                {
                                    Datas[i]
                                        .Add(
                                            column.ColumnName,
                                            table
                                                .Rows[i - 1][
                                                    table.Columns.IndexOf(column.ColumnName)
                                                ]
                                                .ToString()
                                        );
                                }
                            }
                            foreach (DataColumn column in table.Columns)
                            {
                                Data.Add(
                                    column.ColumnName,
                                    table
                                        .Rows[0][table.Columns.IndexOf(column.ColumnName)]
                                        .ToString()
                                );
                            }
                        }
                        else
                        {
                            logger.Warning($"{spreadsheet} doesn't exist in the file {filePath}");
                        }
                    }
                    // The result of each spreadsheet is in result.Tables
                }
            }
            else
            {
                logger.Warning($"{filePath} doesn't exist!!");
            }
        }

        public void GenerateExcel(DataSet ds, string FileName)
        {
            var folderpath = $"{ConfigurationHelper.Config["DataConfig:ExcelReportFolderPath"]}";
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            string filepath = folderpath + FileName;
            if (File.Exists(filepath))
            {
                //dleting file if exists
                File.Delete(filepath);
            }
            using (
                var workbook = SpreadsheetDocument.Create(
                    filepath,
                    DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook
                )
            )
            {
                var workbookPart = workbook.AddWorkbookPart();
                workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
                workbook.WorkbookPart.Workbook.Sheets =
                    new DocumentFormat.OpenXml.Spreadsheet.Sheets();
                WorkbookStylesPart wbsp = workbookPart.AddNewPart<WorkbookStylesPart>();
                //add styles to sheet
                wbsp.Stylesheet = CreateStylesheet();
                wbsp.Stylesheet.Save();
                foreach (System.Data.DataTable table in ds.Tables)
                {
                    var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                    var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                    sheetPart.Worksheet = new Worksheet();
                    sheetPart.Worksheet.AppendChild(sheetData);
                    DocumentFormat.OpenXml.Spreadsheet.Sheets sheets =
                        workbook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
                    string reationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);
                    uint sheetId = 1;
                    if (sheets.Elements<Sheet>().Any())
                    {
                        sheetId =
                            sheets
                                .Elements<Sheet>()
                                .Max(s => s.SheetId.Value) + 1;
                        DocumentFormat.OpenXml.Spreadsheet.Sheet sheet =
                            new DocumentFormat.OpenXml.Spreadsheet.Sheet()
                            {
                                Id = reationshipId,
                                SheetId = sheetId,
                                Name = table.TableName
                            };
                        sheets.AppendChild(sheet);

                        DocumentFormat.OpenXml.Spreadsheet.Row headerRow =
                            new DocumentFormat.OpenXml.Spreadsheet.Row();

                        List<String> columns = new List<string>();
                        foreach (System.Data.DataColumn column in table.Columns)
                        {
                            columns.Add(column.ColumnName);
                            DocumentFormat.OpenXml.Spreadsheet.Cell cell =
                                new DocumentFormat.OpenXml.Spreadsheet.Cell();
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(
                                column.ColumnName
                            );
                            cell.StyleIndex = (UInt32Value)2U;
                            if (!column.ColumnName.ToString().Equals("AsExpected"))
                                headerRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(headerRow);

                        foreach (System.Data.DataRow dsrow in table.Rows)
                        {
                            DocumentFormat.OpenXml.Spreadsheet.Row newRow =
                                new DocumentFormat.OpenXml.Spreadsheet.Row();
                            foreach (String col in columns)
                            {
                                DocumentFormat.OpenXml.Spreadsheet.Cell cell =
                                    new DocumentFormat.OpenXml.Spreadsheet.Cell();
                                cell.DataType = DocumentFormat
                                    .OpenXml
                                    .Spreadsheet
                                    .CellValues
                                    .String;
                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(
                                    dsrow[col].ToString()
                                );
                                if (dsrow[col].ToString().Contains("\r\n"))
                                    cell.StyleIndex = (UInt32Value)3U;
                                if (dsrow["ASExpected"].ToString().Equals("NOK"))
                                {
                                    if (dsrow[col].ToString().Contains("\r\n"))
                                        cell.StyleIndex = (UInt32Value)4U;
                                    else
                                        cell.StyleIndex = (UInt32Value)1U;
                                }
                                if (!col.ToString().Equals("AsExpected"))
                                    newRow.AppendChild(cell);
                            }
                            sheetData.AppendChild(newRow);
                        }
                    }
                }
            }
        }

        private static Stylesheet CreateStylesheet()
        {
            Stylesheet stylesheet1 = new Stylesheet()
            {
                MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" }
            };
            stylesheet1.AddNamespaceDeclaration(
                "mc",
                "http://schemas.openxmlformats.org/markup-compatibility/2006"
            );
            stylesheet1.AddNamespaceDeclaration(
                "x14ac",
                "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac"
            );

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)1U, KnownFonts = true };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 11D };
            Color color1 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            font1.AppendChild(fontSize1);
            font1.AppendChild(color1);
            font1.AppendChild(fontName1);
            font1.AppendChild(fontFamilyNumbering1);
            font1.AppendChild(fontScheme1);

            Font font2 = new Font();
            Bold bold1 = new Bold();
            FontSize fontSize2 = new FontSize() { Val = 11D };
            Color color2 = new Color() { Theme = (UInt32Value)1U };
            FontName fontName2 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            font2.AppendChild(bold1);
            font2.AppendChild(fontSize2);
            font2.AppendChild(color2);
            font2.AppendChild(fontName2);
            font2.AppendChild(fontFamilyNumbering2);
            font2.AppendChild(fontScheme2);

            fonts1.AppendChild(font1);
            fonts1.AppendChild(font2);

            Fills fills1 = new Fills() { Count = (UInt32Value)5U };

            //FillId=0
            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };
            fill1.AppendChild(patternFill1);

            //FillId=1
            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };
            fill2.AppendChild(patternFill2);

            //FillId=2,RED
            Fill fill3 = new Fill();
            PatternFill patternFill3 = new PatternFill() { PatternType = PatternValues.Gray125 };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "FFFF00000" };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill3.AppendChild(foregroundColor1);
            patternFill3.AppendChild(backgroundColor1);
            fill3.AppendChild(patternFill3);

            //FillId=3,BLUE
            Fill fill4 = new Fill();
            PatternFill patternFill4 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor2 = new ForegroundColor() { Rgb = "FF0070C0" };
            BackgroundColor backgroundColor2 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill4.AppendChild(foregroundColor2);
            patternFill4.AppendChild(backgroundColor2);
            fill4.AppendChild(patternFill4);

            //FillId=4,YELLO
            Fill fill5 = new Fill();
            PatternFill patternFill5 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor3 = new ForegroundColor() { Rgb = "FFFFFF00" };
            BackgroundColor backgroundColor3 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill5.AppendChild(foregroundColor3);
            patternFill5.AppendChild(backgroundColor3);
            fill5.AppendChild(patternFill5);

            fills1.AppendChild(fill1);
            fills1.AppendChild(fill2);
            fills1.AppendChild(fill3);
            fills1.AppendChild(fill4);
            fills1.AppendChild(fill5);

            Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            Border border1 = new Border();
            LeftBorder leftBorder1 = new LeftBorder();
            RightBorder rightBorder1 = new RightBorder();
            TopBorder topBorder1 = new TopBorder();
            BottomBorder bottomBorder1 = new BottomBorder();
            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.AppendChild(leftBorder1);
            border1.AppendChild(rightBorder1);
            border1.AppendChild(topBorder1);
            border1.AppendChild(bottomBorder1);
            border1.AppendChild(diagonalBorder1);

            borders1.AppendChild(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)5U };
            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)5U };
            CellFormat cellFormat2 = new CellFormat()
            {
                NumberFormatId = (UInt32Value)0U,
                FontId = (UInt32Value)0U,
                FillId = (UInt32Value)0U,
                BorderId = (UInt32Value)0U,
                FormatId = (UInt32Value)0U
            };
            CellFormat cellFormat3 = new CellFormat()
            {
                NumberFormatId = (UInt32Value)0U,
                FontId = (UInt32Value)0U,
                FillId = (UInt32Value)2U,
                BorderId = (UInt32Value)0U,
                FormatId = (UInt32Value)0U,
                ApplyFill = true
            };
            CellFormat cellFormat4 = new CellFormat()
            {
                NumberFormatId = (UInt32Value)0U,
                FontId = (UInt32Value)1U,
                FillId = (UInt32Value)0U,
                BorderId = (UInt32Value)0U,
                FormatId = (UInt32Value)0U,
                ApplyFill = true,
                ApplyFont = true,
                ApplyBorder = true
            };
            CellFormat cellFormat5 = new CellFormat()
            {
                NumberFormatId = (UInt32Value)0U,
                FontId = (UInt32Value)0U,
                FillId = (UInt32Value)0U,
                BorderId = (UInt32Value)0U,
                FormatId = (UInt32Value)0U,
                ApplyFill = true,
                Alignment = new Alignment()
            };
            CellFormat cellFormat6 = new CellFormat()
            {
                NumberFormatId = (UInt32Value)0U,
                FontId = (UInt32Value)0U,
                FillId = (UInt32Value)2U,
                BorderId = (UInt32Value)0U,
                FormatId = (UInt32Value)0U,
                ApplyFill = true,
                Alignment = new Alignment()
            };

            cellFormat5.Alignment.WrapText = new BooleanValue(true);
            cellFormat6.Alignment.WrapText = new BooleanValue(true);
            cellFormats1.AppendChild(cellFormat2);
            cellFormats1.AppendChild(cellFormat3);
            cellFormats1.AppendChild(cellFormat4);
            cellFormats1.AppendChild(cellFormat5);
            cellFormats1.AppendChild(cellFormat6);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            DocumentFormat.OpenXml.Spreadsheet.CellStyle cellStyle1 =
                new DocumentFormat.OpenXml.Spreadsheet.CellStyle()
                {
                    Name = "Normal",
                    FormatId = (UInt32Value)0U,
                    BuiltinId = (UInt32Value)0
                };

            cellStyles1.AppendChild(cellStyle1);

            stylesheet1.AppendChild(fonts1);
            stylesheet1.AppendChild(fills1);
            stylesheet1.AppendChild(borders1);
            stylesheet1.AppendChild(cellStyleFormats1);
            stylesheet1.AppendChild(cellFormats1);
            stylesheet1.AppendChild(cellStyles1);

            return stylesheet1;
        }
    }
}
