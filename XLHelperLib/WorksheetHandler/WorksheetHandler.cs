using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;

namespace XLHelperLib.WorksheetHandler
{
    public class _WorksheetHandler
    {
        /// <summary>
        /// Upon creation of instance it creates a worksheet insie a given workbook with a given sheet name
        /// </summary>
        public _WorksheetHandler(XLWorkbook _workbook, string _sheetName)
        {
            Workbook = _workbook;
            SheetName = _sheetName;
            worksheet = GetOrCreateWorksheet();
        }
        public _WorksheetHandler(XLWorkbook _workbook)
        {
            Workbook = _workbook;
        }
        public _WorksheetHandler()
        {
            Workbook = new XLWorkbook();
        }

        public XLWorkbook Workbook { get; set; }
        private string sheetName;
        public string SheetName
        {
            get { return sheetName; }
            set 
            { 
                sheetName = value;
                worksheet = GetOrCreateWorksheet();
            }
        }
        private IXLWorksheet worksheet;
        /// <summary>
        /// Worksheet is a readonly property. Please change sheetName to trigger worksheet change.
        /// Created this way to forbid using the longer less readable version of worksheet change.
        /// </summary>
        public IXLWorksheet Worksheet
        {
            get { return worksheet; }
        }

        public IXLWorksheet GetOrCreateWorksheet()
        {
            IXLWorksheet worksheet;
            try
            {
                worksheet = Workbook.Worksheet(SheetName);
            }
            catch
            {
                worksheet = Workbook.Worksheets.Add(SheetName);
            }
            worksheet.Workbook.Save();
            return worksheet;
        }

        public void Adjust()
        {
            Worksheet.Columns("A", "Z").AdjustToContents();
        }

        public string[] GetSheetNames()
        {
            var worksheets = Workbook.Worksheets;
            string[] worksheetNames = new string[worksheets.Count];
            for (int i = 0; i < worksheets.Count; i++)
                worksheetNames[i] = worksheets.Worksheet(i+1).Name;
            return worksheetNames;
        }
        


    }
}
