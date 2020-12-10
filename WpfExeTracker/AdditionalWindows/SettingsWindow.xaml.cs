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
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel settingsViewModel;
        public SettingsWindow()
        {
            InitializeComponent();
            settingsViewModel = new SettingsViewModel();
            this.DataContext = settingsViewModel;
        }

        public SettingsWindow(string chosenFolder, Action<string> changeFolder, string chosenClient, Action<string> changeClientName)
        {
            InitializeComponent();
            settingsViewModel = new SettingsViewModel(chosenFolder, changeFolder, chosenClient, changeClientName);
            this.DataContext = settingsViewModel;
        }
    }
}
