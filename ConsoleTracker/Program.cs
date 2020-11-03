using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using ExeTrackerLogic.ExcersizeReader;
using ExeTrackerLogic.Models;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using XLHelperLib.WorkbookHandler;
using XLHelperLib.WorksheetHandler;
using XLHelperLib.TableManager;
using ExcelToModelLogic.TrainingDataAccess;

namespace ConsoleTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            #region TxtWriteRead
            string baseText;
            using (StreamReader fileReader = new StreamReader(@"C:\Users\RAMAND\Desktop\c#_trainingTracker\training1.txt")) {
                baseText = fileReader.ReadToEnd();

            }
            using (StreamWriter file = new StreamWriter(@"C:\Users\RAMAND\Desktop\c#_trainingTracker\training1.txt"))
            {
                file.WriteLine("abcdef");

            }
            #endregion
            #region XLground
            /*using (var workbook = new XLWorkbook())
            {
                #region Writing to XL
                var worksheet = workbook.Worksheets.Add("Sample Sheet");
                worksheet.Cell("A1").Value = "Hello World!";
                worksheet.Cell("B5").Value = "llo World!gdsg";
                worksheet.Cell("C3").FormulaA1 = "=MID(B5, 7, 5)";
                worksheet.Columns("A", "D").AdjustToContents();
                workbook.SaveAs(@"C:\Users\RAMAND\Desktop\c#_trainingTracker\trainingX.xlsx");
                #endregion

                #region Reading from XL
                var newWorksheet = workbook.Worksheet("Sample Sheet");
                Console.WriteLine($"{newWorksheet.Cell("C3").Value}");
                #endregion
                
            }*/

            //WorksheetChecker.CheckWorksheets();
            //var TM = new TableMaker();
            //TM.CreateTable();
            //SheetReader.ReadToConsole();
            //SheetReader.ReadDates();
            #endregion

            var folderPath = @"C:\Users\RAMAND\Desktop\c#_trainingTracker";
            var workbookHandler = new _WorkbookHandler(folderPath, "MP");
            var workbook = workbookHandler.getWorkbook();
            var worksheetHandler = new _WorksheetHandler(workbook, DateTime.Now.AddMonths(0).Month.ToString());
            worksheetHandler.Worksheet.Clear();
            var tableMaker = new TableMaker(workbook, worksheetHandler.Worksheet);
            var tableReader = new TableReader();
            List<string[]> tableRows = new List<string[]>();
            tableRows.Add(new string[] { "BP", "100", "10", "10", "10000", "HPush" });
            tableRows.Add(new string[] { "Cable Row", "100", "10", "10", "10000", "HPull" });
            tableRows.Add(new string[] { "OHP", "100", "10", "10", "10000", "VPush" });
            tableMaker.CreateTable(new string[] { "Exercise", "Weight", "Sets", "Reps", "Volume", "Type" },null,tableRows);
            TrainingDataMapper.AddTraining(new TrainingModel(DateTime.Now), workbook);
            List<ExerciseModel> exerciseList = new List<ExerciseModel>();
            exerciseList.Add(new ExerciseModel("A", 1, 1, 1));
            exerciseList.Add(new ExerciseModel("B", 2, 2, 2));
            exerciseList.Add(new ExerciseModel("C", 3, 3, 3));
            TrainingModel training = new TrainingModel(DateTime.Now.AddDays(1), exerciseList);
            //tableMaker.InsertTable(tableReader.GetTableStart(worksheetHandler.Worksheet, 7), new string[] { "Exercise", "Weight", "Sets", "Reps", "Volume", "Type" }, null, tableRows);
            TrainingDataMapper.UpdateTraining(training, workbook, 2);
            TrainingDataMapper.DeleteTraining(worksheetHandler.Worksheet, 1);
            //TrainingDataMapper.GetAllTrainings(folderPath, "kk");

            //var trg = TrainingDataMapper.GetTraining(worksheetHandler.Worksheet, 1);
            //worksheetHandler.SheetName = "3";
            //var trgs = TrainingDataMapper.GetMonthTrainings(worksheetHandler.Worksheet);
            //var yTrgs = TrainingDataMapper.GetYearTrainings(workbook);
            //TrainingDataMapper.DeleteTraining(worksheetHandler.Worksheet, 28);

            worksheetHandler.Adjust();
            workbook.Save();
            Console.WriteLine("Finished");

    }
}
}
