using ClosedXML.Excel;
using XLHelperLib.Helpers;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace XLHelperLib.TableManager
{
    public class TableMaker
    {
        public XLWorkbook Workbook { get; set; }
        public IXLWorksheet Worksheet { get; set; }

        public TableMaker(IXLWorksheet _worksheet)
        {
            Worksheet = _worksheet;
            Workbook = _worksheet.Workbook;
        }
        public TableMaker(XLWorkbook _workbook, IXLWorksheet _worksheet)
        {
            Workbook = _workbook;
            Worksheet = _worksheet;
        }

        public void CreateTable(string[] headers, DateTime? date = null, List<string[]> tableRows = null) {
            var rowToCreateTableAt = Worksheet.LastRowUsed() == null ? 1 : Worksheet.LastRowUsed().RowNumber()+2;
            CreateTableHeaders(rowToCreateTableAt, headers, date);
            for (int i = 0; i < tableRows.Count; i++)
                CreateRow(rowToCreateTableAt + 2 + i, tableRows[i]);
        }
        /// <summary>
        /// Creates table at a given position
        /// </summary>
        /// <param name="rowToCreateTableAt">First row of a table before which you want to create table </param>
        public void InsertTable(int rowToCreateTableAt, string[] headers, DateTime? date = null, List<string[]> tableRows = null)
        {
            Worksheet.Row(rowToCreateTableAt).InsertRowsAbove(tableRows.Count + 3);
            CreateTableHeaders(rowToCreateTableAt, headers, date);
            for (int i = 0; i < tableRows.Count; i++)
                CreateRow(rowToCreateTableAt + 2 + i, tableRows[i]);
        }

        private void CreateTableHeaders(int row, string[] headers, DateTime? date = null)
        {
            //Won't work with less than 1 header
            if (headers.Length < 1) throw new Exception("Need to pass in at least 1 header");
            
            //Assign current date if date paramater was null
            //date = date ?? DateTime.Now;
            DateTime notNullDate = date == null ? DateTime.Now : (DateTime) date;

            //Create date row above the table and style it
            var dateCell = Worksheet.Cell(row, 1);
            //dateCell.SetDataType(XLDataType.Text);
            dateCell.SetValue<string>(notNullDate.ToShortDateString());
            Worksheet.Range(dateCell, dateCell.CellRight(headers.Length-1)).Merge();
            dateCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            //Create row with table column names
            CreateRow(row + 1, headers);

            //Style all
            var tableHeaders = Worksheet.Range(row, 1, row + 1, headers.Length);
            tableHeaders.Style.Font.Bold = true;
            BorderHelper.SetAllBorders(tableHeaders, XLBorderStyleValues.Medium);
        }

        public void CreateRow(int row, string[] rowData)
        {
            for (int i = 0; i < rowData.Length; i++)
                Worksheet.Cell(row, i+1).Value = rowData[i];
        }
    }
}
