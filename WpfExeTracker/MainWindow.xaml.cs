using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfExeTracker.ViewModels;

namespace WpfExeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //EmployeeViewModel ViewModel;
        TrainingViewModel trainingViewModel;
        public MainWindow()
        {
            InitializeComponent();
            trainingViewModel = new TrainingViewModel();
            this.DataContext = trainingViewModel;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var trainingList = trainingViewModel.TrainingList;
            Console.WriteLine();
        }

    }
}
