using ExcelToModelLogic.TrainingDataAccess;
using ExeTrackerLogic.ExcersizeReader;
using ExeTrackerLogic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfExeTracker.ViewModels;

namespace WpfExeTracker.AdditionalWindows
{
    /// <summary>
    /// Interaction logic for ExerciseLookupWindow.xaml
    /// </summary>
    public partial class ExerciseLookupWindow : Window
    {
        private ExerciseLookupViewModel exerciseLookupViewModel;
        public ExerciseLookupWindow()
        {
            InitializeComponent();
            exerciseLookupViewModel = new ExerciseLookupViewModel();
            this.DataContext = exerciseLookupViewModel;
        }
        public void ChangeSelectedExercise(ExerciseModel exercise, string folderPath, string clientName)
        {
            exerciseLookupViewModel.SelectedExercise = exercise;
            var allTrainingsList = TrainingDataMapper.GetAllTrainings(folderPath, clientName);
            exerciseLookupViewModel.TrainingList = allTrainingsList;
            exerciseLookupViewModel.ExerciseList = new ObservableCollection<ExerciseModel>();
        }
    }
}
