using Microsoft.Win32;
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
using TestingPlace.ViewModel.TestCreationSessions;
using TestingPlace.ViewModel.UserControls.TestCreation;

namespace TestingPlace.View.UserControls.TestCreation
{
    /// <summary>
    /// Логика взаимодействия для QuestionCreationWindow.xaml
    /// </summary>
    public partial class QuestionCreationWindow : UserControl
    {
        public QuestionCreationWindow(ITestCreationSession session)
        {
            InitializeComponent();
            var context = new QuestionCreationViewModel(session);
            DataContext = context;

            context.GettingFilePath += OnGettingFilePath;
        }

        private string OnGettingFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG IMAGE|*.png|GIF|*.gif|JPEG IMAGE|*.jpeg|JPG IMAGE|*.jpg";

            if(openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return "";
        }
    }
}
