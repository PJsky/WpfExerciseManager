using ExeTrackerLogic.ExcersizeReader;
using ExeTrackerLogic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfExeTracker.Commands;

namespace WpfExeTracker.ViewModels
{
    class ExerciseLookupViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ExerciseLookupViewModel()
        {
            getByTypeCommand = new RelayCommand(GetByType);
            getExerciseBestCommand = new RelayCommand(GetExerciseBest);
            getExerciseHistoryCommand = new RelayCommand(GetExerciseHistory);
        }

        private ExerciseModel selectedExercise;

        public ExerciseModel SelectedExercise
        {
            get { return selectedExercise; }
            set { selectedExercise = value; OnPropertyChanged("SelectedExercise"); }
        }

        private ObservableCollection<ExerciseModel> exerciseList;

        public ObservableCollection<ExerciseModel> ExerciseList
        {
            get { return exerciseList; }
            set { exerciseList = value; OnPropertyChanged("ExerciseList"); }
        }

        private List<TrainingModel> trainingList;

        public List<TrainingModel> TrainingList
        {
            get { return trainingList; }
            set { trainingList = value; }
        }


        private List<ExerciseModel> allExercises;
        public List<ExerciseModel> AllExercises
        {
            get { return allExercises; }
            set { allExercises = value; }
        }



        private RelayCommand getByTypeCommand;
        public RelayCommand GetByTypeCommand
        {
            get { return getByTypeCommand; }
        }
        
        private RelayCommand getExerciseBestCommand;
        public RelayCommand GetExerciseBestCommand
        {
            get { return getExerciseBestCommand; }
        }
        
        private RelayCommand getExerciseHistoryCommand;
        public RelayCommand GetExerciseHistoryCommand
        {
            get { return getExerciseHistoryCommand; }
        }

        private void GetByType()
        {
            var exercises = _ExerciseReader.GetByType(TrainingList, SelectedExercise.Type);
            ExerciseList = new ObservableCollection<ExerciseModel>(exercises);
        }

        private void GetExerciseBest()
        {
            var exercises = _ExerciseReader.GetExerciseBest(TrainingList, SelectedExercise.Name);
            ExerciseList = new ObservableCollection<ExerciseModel>(exercises);
        }

        private void GetExerciseHistory()
        {
            var exercises = _ExerciseReader.GetExerciseHistory(TrainingList, SelectedExercise.Name);
            ExerciseList = new ObservableCollection<ExerciseModel>(exercises);
        }
    }
}
