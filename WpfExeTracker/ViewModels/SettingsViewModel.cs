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
    class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public SettingsViewModel()
        {
            openFolderPickerCommand = new RelayCommand(OpenFolderPicker);
        }

        public SettingsViewModel(string inputFolderChosen, Action<string> changeFolder, string inputChosenClient, Action<string> changeClientName)
        {
            ChangeFolder = changeFolder;
            ChangeClientName = changeClientName;
            FolderChosen = inputFolderChosen;
            ChosenClient = inputChosenClient;
            openFolderPickerCommand = new RelayCommand(OpenFolderPicker);
            changeChosenClientCommand = new RelayCommand(ChangeChosenClient);
            var aaa = "aaa";
        }

        private Action<string> ChangeFolder;
        private Action<string> ChangeClientName;

        private string folderChosen;

        public string FolderChosen
        {
            get { return folderChosen; }
            set { 
                folderChosen = value; 
                if(ChangeFolder != null)
                    ChangeFolder.Invoke(FolderChosen); 
                OnPropertyChanged("FolderChosen"); }
        }

        private string chosenClient;
        public string ChosenClient
        {
            get { return chosenClient; }
            set { 
                chosenClient = value;
                OnPropertyChanged("ChosenClient");
            }
        }


        private RelayCommand openFolderPickerCommand;

        public RelayCommand OpenFolderPickerCommand
        {
            get { return openFolderPickerCommand; }
            set { openFolderPickerCommand = value; }
        }

        private RelayCommand changeChosenClientCommand;

        public RelayCommand ChangeChosenClientCommand
        {
            get { return changeChosenClientCommand; }
            set { changeChosenClientCommand = value; }
        }


        private void OpenFolderPicker()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            FolderChosen = dialog.SelectedPath;
        }

        private void ChangeChosenClient()
        {
            if (ChangeClientName != null)
                ChangeClientName.Invoke(chosenClient);
        }
    }
}
