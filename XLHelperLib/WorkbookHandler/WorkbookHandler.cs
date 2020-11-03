using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XLHelperLib.WorkbookHandler
{
    public class _WorkbookHandler
    {
        public _WorkbookHandler(string _folderPath, string _clientName)
        {
            FolderPath = _folderPath;
            ClientName = _clientName;
        }
        //Path specifying where is the folder where we will save our clients folders with their training XLsheets
        //Example: @"C:\Users\User\Desktop\c#_trainingTracker\
        private string folderPath;
        public string FolderPath
        {
            get { return folderPath; }
            set { folderPath = value; }
        }

        private string clientName;

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }
        ///<summary>
        ///Gets a new workbook instance referencing current years training sessions
        ///</summary>
        public XLWorkbook getWorkbook()
        {
            /*
            //Return the XLWorkbook with a path to Excel file given a year
            Directory.CreateDirectory(getWorkbookFolder(DateTime.Now));
            try {
                return new XLWorkbook(getWorkbookPath(DateTime.Now));
            }
            catch
            {
                var workbook = new XLWorkbook();
                workbook.AddWorksheet("Default");
                workbook.SaveAs(getWorkbookPath(DateTime.Now));
                return new XLWorkbook(getWorkbookPath(DateTime.Now));
            }*/
            return getWorkbook(DateTime.Now);
            
        }

        public XLWorkbook getWorkbook(int year)
        {
            return getWorkbook(new DateTime(year, 1, 1));
        }

        ///<summary>
        ///Gets a new workbook instance given a years date
        ///</summary>
        public XLWorkbook getWorkbook(DateTime year)
        {
            //Return the XLWorkbook with a path to Excel file given a year
            Directory.CreateDirectory(getWorkbookFolder(year));
            try
            {
                return new XLWorkbook(getWorkbookPath(year));
            }
            catch
            {
                var workbook = new XLWorkbook();
                workbook.AddWorksheet("Default");
                workbook.SaveAs(getWorkbookPath(year));
                return new XLWorkbook(getWorkbookPath(year));
            }
        }

        public string getWorkbookPath(DateTime Year)
        {
            return getWorkbookFolder(Year) + getWorkbookFilename(Year);
        }
        public string getWorkbookFolder(DateTime Year)
        {
            return FolderPath + @"\" + ClientName + @"\" + Year.Year + @"\";
        }
        public string getWorkbookFilename(DateTime Year)
        {
            return $"{Year.Year}_{ClientName}.xlsx";
        }

    }
}
