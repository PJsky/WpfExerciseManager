﻿using ExcelToModelLogic.TrainingDataAccess;
using ExeTrackerLogic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfExeTracker.AdditionalWindows;
using WpfExeTracker.Commands;
using WpfExeTracker.SettingsHelpers;
using XLHelperLib.WorkbookHandler;
using XLHelperLib.WorksheetHandler;

namespace WpfExeTracker.ViewModels
{
    class TrainingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public _WorkbookHandler workbookHandler;
        public _WorksheetHandler worksheetHandler;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public TrainingViewModel()
        {
            LoadSettingsFromFile();
            LoadData();
            saveCommand = new RelayCommand(Save);
            deleteCommand = new RelayCommand(Delete);
            resetCommand = new RelayCommand(Reset);
            openExerciseLookupCommand = new RelayCommand(OpenExerciseLookup);
            exerciseLookupWindow = new ExerciseLookupWindow();
            openSettingsWindowCommand = new RelayCommand(OpenSettingsWindow);
            settingsWindow = new SettingsWindow(FolderPath, ChangeFolderPath, clientName, ChangeClientName);
        }

        #region Properties
        private string folderPath;
        public string FolderPath
        {
            get { return folderPath; }
            set { folderPath = value; SaveSettingsToFile(); LoadData(); OnPropertyChanged("FolderPath"); }
        }

        private string clientName;
        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; SaveSettingsToFile(); LoadData(); OnPropertyChanged("ClientName"); }
        }

        private ObservableCollection<ExerciseModel> exerciseList;
        public ObservableCollection<ExerciseModel> ExerciseList
        {
            get { return exerciseList; }
            set { exerciseList = value; OnPropertyChanged("ExerciseList"); }
        }
        private DateTime trainingDay;

        public DateTime TrainingDay
        {
            get { return trainingDay; }
            set { trainingDay = value; OnPropertyChanged("TrainingDay"); }
        }

        private List<int> trainingYears;

        public List<int> TrainingYears
        {
            get { return trainingYears; }
            set { trainingYears = value; OnPropertyChanged("TrainingYears"); }
        }

        private List<int> trainingMonths;

        public List<int> TrainingMonths
        {
            get { return TrainingDataMapper.GetTrainingMonths(workbookHandler.getWorkbook(SelectedTrainingYear)); }
            set { trainingMonths = value; OnPropertyChanged("TrainingMonths"); }
        }

        private int selectedTrainingYear;

        public int SelectedTrainingYear
        {
            get { return selectedTrainingYear; }
            set {
                selectedTrainingYear = value;
                OnPropertyChanged("SelectedTrainingYear");
                OnPropertyChanged("TrainingMonths");
                SelectedTrainingMonth = TrainingMonths[0];
            }
        }

        private int selectedTrainingMonth;
        public int SelectedTrainingMonth
        {
            get { return selectedTrainingMonth; }
            set {
                selectedTrainingMonth = value;
                OnPropertyChanged("SelectedTrainingMonth");
                OnPropertyChanged("TrainingDays");
                OnPropertyChanged("TrainingDaysAsString");
                SelectedTrainingDay = TrainingDays[0];
            }
        }
        private List<DateTime> trainingDays;

        public List<DateTime> TrainingDays
        {
            get {
                return TrainingList.Select(t => t.TrainingDay).ToList(); 
            }
            set { 
                trainingDays = value;
                OnPropertyChanged("TrainingDays");
                OnPropertyChanged("SelectedTrainingDay");
                OnPropertyChanged("TrainingDaysAsString");
            }
        }
        private DateTime selectedTrainingDay;

        public DateTime SelectedTrainingDay
        {
            get { return selectedTrainingDay; }
            set { selectedTrainingDay = value;
                OnPropertyChanged("SelectedTrainingDay");
                OnPropertyChanged("ExerciseList");
                ExerciseList = new ObservableCollection<ExerciseModel>(TrainingList[TrainingPositionInMonth].Exercises);
                CurrentlySelectedTrainingDay = SelectedTrainingDay;
            }
        }

        public List<TrainingModel> TrainingList
        {
            get {
                var trainingList = TrainingDataMapper.GetMonthTrainings(workbookHandler.getWorkbook(SelectedTrainingYear), SelectedTrainingMonth);
                trainingList.Add(new TrainingModel(10, 10, 10));
                return trainingList;
            }
        }

        private int trainingPositionInMonth;

        public int TrainingPositionInMonth
        {
            get {
                if (trainingPositionInMonth < 0) return 0;
                return trainingPositionInMonth; 
            }   
            set { trainingPositionInMonth = value; OnPropertyChanged("TrainingPositionInMonth"); }
        }

        private DateTime currentlySelectedTrainingDay;
                
        public DateTime CurrentlySelectedTrainingDay
        {
            get { return currentlySelectedTrainingDay; }
            set { currentlySelectedTrainingDay = value; OnPropertyChanged("CurrentlySelectedTrainingDay"); }
        }
        private ExerciseModel currentlySelectedExercise;

        public ExerciseModel CurrentlySelectedExercise
        {
            get { return currentlySelectedExercise; }
            set { currentlySelectedExercise = value; OnPropertyChanged("CurrentlySelectedExercise");  }
        }


        //Command properties
        private RelayCommand saveCommand;

        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
        }

        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        private RelayCommand resetCommand;

        public RelayCommand ResetCommand
        {
            get { return resetCommand; }
        }

        private RelayCommand openExerciseLookupCommand;
        public RelayCommand OpenExerciseLookupCommand
        {
            get { return openExerciseLookupCommand; }
        }

        private RelayCommand openSettingsWindowCommand;
        public RelayCommand OpenSettingsWindowCommand
        {
            get { return openSettingsWindowCommand; }
        }

        //Property with a binded exercise lookup window and settings window
        public ExerciseLookupWindow exerciseLookupWindow { get; set; }
        public SettingsWindow settingsWindow { get; set; }
        #endregion


        private void LoadData()
        {
            workbookHandler = new _WorkbookHandler(folderPath, clientName);
            var workbook = workbookHandler.getWorkbook();
            worksheetHandler = new _WorksheetHandler(workbook, DateTime.Now.Month.ToString());
            TrainingYears = TrainingDataMapper.GetTrainingYears(folderPath, clientName);
            SelectedTrainingYear = TrainingYears[0];
            TrainingMonths = TrainingDataMapper.GetTrainingMonths(workbookHandler.getWorkbook(SelectedTrainingYear));
            SelectedTrainingMonth = TrainingMonths[0];
            TrainingDays = TrainingList.Select(t => t.TrainingDay).ToList();
            SelectedTrainingDay = TrainingDays[0];
            ExerciseList = new ObservableCollection<ExerciseModel>(TrainingList[TrainingPositionInMonth].Exercises);
            CurrentlySelectedTrainingDay = DateTime.Now;
        }

        private void Save()
        {
            var workbook = workbookHandler.getWorkbook(CurrentlySelectedTrainingDay.Year);
            
            if (SelectedTrainingDay != TrainingDays[TrainingDays.Count() - 1])
            {
                MessageBoxResult result = MessageBox.Show("Do you want to update this training session?", "Update", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No) return;
                TrainingDataMapper.UpdateTraining(new TrainingModel(CurrentlySelectedTrainingDay, ExerciseList.ToList()), SelectedTrainingYear, SelectedTrainingMonth,TrainingPositionInMonth + 1 , folderPath, "kk");
                MessageBox.Show("A training has been updated");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Do you want to create new training session?", "Create", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No) return;
                TrainingDataMapper.AddTraining(new TrainingModel(CurrentlySelectedTrainingDay, ExerciseList.ToList()), workbook);
                TrainingYears = TrainingDataMapper.GetTrainingYears(folderPath, "kk");
                MessageBox.Show("A new training has been saved");
                workbook.Save();
            }
        }

        private void Delete()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to delete this training session?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No) return;

                var workbook = workbookHandler.getWorkbook(SelectedTrainingYear);
            if (SelectedTrainingDay != TrainingDays[TrainingDays.Count() - 1])
            {
                TrainingDataMapper.DeleteTraining(workbook.Worksheet(SelectedTrainingMonth.ToString()), TrainingPositionInMonth + 1);
                MessageBox.Show("A training has been deleted");
            }
            else
                MessageBox.Show("Can't delete non-existing training");

            workbook.Save();
        }

        private void Reset()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to reset datagrid exercise list?","Reset", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
                ExerciseList = new ObservableCollection<ExerciseModel>(TrainingList[TrainingPositionInMonth].Exercises);
        }

        private void OpenExerciseLookup()
        {
            if(!exerciseLookupWindow.IsLoaded)
                exerciseLookupWindow = new ExerciseLookupWindow();
            exerciseLookupWindow.ChangeSelectedExercise(CurrentlySelectedExercise, folderPath, "kk");
            exerciseLookupWindow.Show();
        }

        private void OpenSettingsWindow() {
            if (!settingsWindow.IsLoaded)
                settingsWindow = new SettingsWindow(FolderPath, ChangeFolderPath, clientName, ChangeClientName);
            settingsWindow.Show();
        }

        private void ChangeFolderPath(string newFolderPath)
        {
            FolderPath = newFolderPath;
        }

        private void ChangeClientName(string newClientName)
        {
            ClientName = newClientName;
        }

        private void SaveSettingsToFile()
        {
            SettingsHelper.SaveSetting(folderPath, clientName);
        }

        private void LoadSettingsFromFile()
        {
            SettingsHelper.LoadSettings(ref folderPath, ref clientName);
        }

    }
}
