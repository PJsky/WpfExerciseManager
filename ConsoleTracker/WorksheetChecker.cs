using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTracker
{
    public class WorksheetChecker
    {

        public static void CheckWorksheets() 
        {
            using (var workbook = new XLWorkbook(@"C:\Users\RAMAND\Desktop\c#_trainingTracker\trainingX.xlsx"))
            {
                var worksheets = workbook.Worksheets.Count;
                Console.WriteLine($"number of worksheets: {worksheets}");
                Console.WriteLine("Worksheets names:");
                for (int i = 1; i <= worksheets; i++)
                {
                    var worksheetName = workbook.Worksheet(i);
                    Console.WriteLine(worksheetName);
                }
            }
        }

        public static IXLWorksheet GetOrCreateWorksheet(XLWorkbook workbook, string SheetName)
        {
            IXLWorksheet worksheet;
            try
            {
                worksheet = workbook.Worksheet(SheetName);
            }
            catch
            {
                worksheet = workbook.Worksheets.Add(SheetName);
            }
            return worksheet;
        }

    }
}
