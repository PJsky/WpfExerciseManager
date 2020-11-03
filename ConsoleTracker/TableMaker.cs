using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTracker
{
    public class TableMakerrrr
    {
        public void CreateTable() {

            using (var workbook = new XLWorkbook(@"C:\Users\RAMAND\Desktop\c#_trainingTracker\trainingX.xlsx"))
            {
                IXLWorksheet worksheet;
                try {
                    worksheet = workbook.Worksheet("New");
                }
                catch {
                    worksheet = workbook.Worksheets.Add("New");
                }

                var lastRowUsed = worksheet.LastRowUsed();
                if (lastRowUsed != null)
                    CreateTableHeaders(worksheet, lastRowUsed.RowNumber()+2);
                else
                    CreateTableHeaders(worksheet, 1);

                worksheet.Columns().AdjustToContents();
                //workbook.SaveAs(@"C:\Users\RAMAND\Desktop\c#_trainingTracker\trainingX.xlsx");
                workbook.Save();
            }
        }

        private void CreateTableHeaders(IXLWorksheet worksheet, int row) {
            //Create date above the table and style it
            var DateCell = worksheet.Cell(row, 1);
            DateCell.Value = DateTime.Now.Date;
            worksheet.Range(DateCell, DateCell.CellRight(4)).Merge();
            DateCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //Creating row with table column names
            CreateRow(worksheet, row + 1, 
                        new string[] { "Exercise", "Weight (kg)", 
                        "Reps", "Sets", "Volume" });

            //Style table heading
            var TableHeadings = worksheet.Range(row, 1, row + 1, 5);
            TableHeadings.Style.Font.Bold = true;
            SetAllBorders(TableHeadings, XLBorderStyleValues.Medium);
        }

        private void CreateRow(IXLWorksheet worksheet, int row, string[] rowData)
        {
            for (int i = 1; i <= rowData.Length; i++)
                worksheet.Cell(row, i).Value = rowData[i-1];
        }

        private void SetAllBorders(IXLRange range, XLBorderStyleValues borderStyle)
        {
            range.Style.Border.OutsideBorder = borderStyle;
            range.Style.Border.InsideBorder = borderStyle;
        }
    }
}
