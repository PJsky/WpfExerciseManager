using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;
using XLHelperLib.WorkbookHandler;

namespace ConsoleTracker
{
    public class SheetReader
    {
        public static void ReadToConsole() 
        {
            var workbookHandler = new _WorkbookHandler(@"C:\Users\RAMAND\Desktop\c#_trainingTracker","NewClient");
            using (var workbook = workbookHandler.getWorkbook())
            {
                var worksheet = WorksheetChecker.GetOrCreateWorksheet(workbook, "New");
                var lastRowUsed = worksheet.LastRowUsed() != null? worksheet.LastRowUsed().RowNumber() : 1;
                for(int i=1; i<=lastRowUsed;i++)
                {
                    Console.WriteLine();
                    ReadRowToConsole(worksheet, i);
                }
            }
        }

        private static void ReadRowToConsole(IXLWorksheet worksheet, int row)
        {
            for (int i = 1; i <= 5; i++)
                if (!worksheet.Cell(row, i).IsEmpty())
                    Console.Write(worksheet.Cell(row, i).Value + "; ");
        }

        public static void ReadDates()
        {
            using (var workbook = new XLWorkbook(@"C:\Users\RAMAND\Desktop\c#_trainingTracker\trainingX.xlsx"))
            {
                var worksheet = WorksheetChecker.GetOrCreateWorksheet(workbook, "New");
                var lastRowUsed = worksheet.LastRowUsed() != null ? worksheet.LastRowUsed().RowNumber() : 1;
                for (int i = 1; i <= lastRowUsed; i++)
                    if (worksheet.Cell(i, 1).Value.GetType() == typeof(DateTime))
                        Console.WriteLine(worksheet.Cell(i, 1).Value);
            }
        }

        public static DateTime GetSheetsLastDate()
        {
            return DateTime.Now;
        }
    }
}
