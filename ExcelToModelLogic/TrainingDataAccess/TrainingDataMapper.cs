using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ExeTrackerLogic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using XLHelperLib.TableManager;
using XLHelperLib.WorkbookHandler;
using XLHelperLib.WorksheetHandler;

namespace ExcelToModelLogic.TrainingDataAccess
{
    //Allows for CRUD operations on TrainingModel within excel
    public static class TrainingDataMapper
    {
        public static TrainingModel AddTraining(TrainingModel training, XLWorkbook workbook)
        {
            var wsHandler = new _WorksheetHandler(workbook, training.TrainingDay.Month.ToString());
            var tbMaker = new TableMaker(wsHandler.Worksheet);
            List<string[]> exerciesesData = new List<string[]>();
            foreach(ExerciseModel exercise in training.Exercises)
                exerciesesData.Add(new string[] { exercise.Name, exercise.Weight.ToString(), exercise.Sets.ToString(),
                                         exercise.Reps.ToString(), exercise.ExerciseVolume.ToString(), exercise.Type});

            tbMaker.CreateTable(new string[] { "Exercise", "Weight", "Sets", "Reps", "Volume", "Type" }, training.TrainingDay.Date, exerciesesData);
            return training;
        }

        public static List<TrainingModel> GetAllTrainings(string folderPath, string clientName)
        {
            var wbHandler = new _WorkbookHandler(folderPath, clientName);
            List<TrainingModel> allTrainings = new List<TrainingModel>();
            foreach (int trainingYear in GetTrainingYears(folderPath, clientName))
                allTrainings.AddRange(GetYearTrainings(wbHandler.getWorkbook(trainingYear)));
            return allTrainings;
        }
        public static List<int> GetTrainingYears(string folderPath, string clientName)
        {
            var files = Directory.GetDirectories(folderPath + @"\" + clientName);
            List<int> allTrainingYears = new List<int>();
            foreach (var file in files)
            {
                //Console.WriteLine(Path.GetExtension(file));
                Console.WriteLine(Path.GetFileName(file));
                int year;
                try
                {
                    year = Int32.Parse(Path.GetFileName(file));
                }
                catch
                {
                    throw new Exception("Incorrect folder exists. This folder may have been manually modified");
                }
                allTrainingYears.Add(year);
            }
            return allTrainingYears;
        }
        public static List<TrainingModel> GetYearTrainings(XLWorkbook workbook)
        {
            List<TrainingModel> yearTrainings = new List<TrainingModel>();
            foreach (int trainingMonth in GetTrainingMonths(workbook))
                yearTrainings.AddRange(GetMonthTrainings(workbook.Worksheet(trainingMonth.ToString())));
            return yearTrainings;
        }

        public static List<int> GetTrainingMonths(XLWorkbook workbook)
        {
            List<int> yearTrainingMonths = new List<int>();
            foreach (IXLWorksheet worksheet in workbook.Worksheets)
                try
                {
                    int month = Int32.Parse(worksheet.Name);
                    if (month >= 1 && month <= 12)
                        yearTrainingMonths.Add(month);
                    else
                        throw new Exception("Month out of range");
                }
                catch
                {
                    Console.WriteLine("Found worksheets other than training months named: " + worksheet.Name);
                }
            return yearTrainingMonths;
        }

        //Gets all tables as trainings from worksheet representing a month
        public static List<TrainingModel> GetMonthTrainings(IXLWorksheet worksheet)
        {
            /*var lastRowUsed = worksheet.LastRowUsed();
            if (lastRowUsed == null) return new List<TrainingModel>();
            List<int> tablesStartingRows = new List<int>();
            tablesStartingRows.Add(1);
            //If found space between tables add it to the end of tables rows;
            for (int i = 1; i <= lastRowUsed.RowNumber(); i++)
                if (worksheet.Cell(i, 1).IsEmpty() && !worksheet.Cell(i+1, 1).IsEmpty())
                    tablesStartingRows.Add(i+1);*/

            List<TrainingModel> monthTrainings = new List<TrainingModel>();
            foreach (int tableStartingRow in GetMonthTrainingStartingRows(worksheet))
                monthTrainings.Add(GetTraining(worksheet, tableStartingRow));

            return monthTrainings;
        }

        //Overload allowing to use workbook and months as integers to get monthly training sessions
        public static List<TrainingModel> GetMonthTrainings(XLWorkbook workbook, int month)
        {
            IXLWorksheet worksheet = workbook.Worksheet(month.ToString());
            return GetMonthTrainings(worksheet);
        }

        public static TrainingModel GetTraining(IXLWorksheet worksheet, int startingRow)
        {
            //if (worksheet.IsEmpty()) return null;
            var dateCell = worksheet.Cell(startingRow, 1);
            var t = dateCell.Value;
            var date = DateTime.Parse(dateCell.Value.ToString());
            var trainingExerciseList = new List<ExerciseModel>();

            //Skip the first two rows of headers
            startingRow+=2;

            bool tableFinished = false;
            //Add all the excersizes until table finishes
            while (!tableFinished)
            {
                if(!worksheet.Cell(startingRow, 1).IsEmpty())
                {
                    trainingExerciseList.Add(
                    new ExerciseModel(
                        worksheet.Cell(startingRow, 1).Value.ToString(),
                        Int32.Parse(worksheet.Cell(startingRow, 2).Value.ToString()),
                        Int32.Parse(worksheet.Cell(startingRow, 3).Value.ToString()),
                        Int32.Parse(worksheet.Cell(startingRow, 4).Value.ToString()),
                        worksheet.Cell(startingRow, 6).Value.ToString()
                        )
                    );
                    startingRow++;
                }
                else
                    tableFinished = true;
            }

            TrainingModel training = new TrainingModel(date, trainingExerciseList);
            return training;
        }

        //Should be in table reader most likely
        public static List<int> GetMonthTrainingStartingRows(IXLWorksheet worksheet)
        {
            var lastRowUsed = worksheet.LastRowUsed();
            if (lastRowUsed == null)
                return new List<int>();
                //throw new Exception("No such table");
            List<int> tablesStartingRows = new List<int>();
            tablesStartingRows.Add(1);
            //If found space between tables add it to the end of tables rows;
            for (int i = 1; i <= lastRowUsed.RowNumber(); i++)
                if (worksheet.Cell(i, 1).IsEmpty() && !worksheet.Cell(i + 1, 1).IsEmpty())
                    tablesStartingRows.Add(i + 1);

            return tablesStartingRows;
        }

        public static void DeleteTraining(IXLWorksheet worksheet, int position) 
        {
            var startingRow = new TableReader().GetTableStart(worksheet, position);
            IXLCell firstCell = worksheet.Cell(startingRow, 1);
            IXLCell lastCell;

            //Skip date row and table headers 
            startingRow += 2;

            bool tableFinished = false;
            //Find last used row
            while (!tableFinished)
                if (!worksheet.Cell(startingRow, 1).IsEmpty())
                    startingRow++;
                else
                    tableFinished = true;

            lastCell = worksheet.Cell(startingRow, 6);
            var tableToRemove = worksheet.Range(firstCell, lastCell);
            tableToRemove.Delete(XLShiftDeletedCells.ShiftCellsUp);
        }

        //Refine it later to be more optimized. Make it react to changes in Date, and exercises
        public static void UpdateTraining(TrainingModel training, XLWorkbook workbook, int position) 
        {
            var worksheetHandler = new _WorksheetHandler(workbook, training.TrainingDay.Month.ToString());
            var startingRow = new TableReader().GetTableStart(worksheetHandler.Worksheet, position);
            DeleteTraining(worksheetHandler.Worksheet, position);
            var tbMaker = new TableMaker(worksheetHandler.Worksheet);
            List<string[]> exerciesesData = new List<string[]>();
            foreach (ExerciseModel exercise in training.Exercises)
                exerciesesData.Add(new string[] { exercise.Name, exercise.Weight.ToString(), exercise.Sets.ToString(),
                                         exercise.Reps.ToString(), exercise.ExerciseVolume.ToString(), exercise.Type});

            tbMaker.InsertTable(startingRow, new string[] { "Exercise", "Weight", "Sets", "Reps", "Volume", "Type" }, training.TrainingDay.Date, exerciesesData);
        }

    }
}
