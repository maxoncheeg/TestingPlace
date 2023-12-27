using Microsoft.Win32;
using System.Windows.Controls;

namespace TestingPlace.View.UserControls.TestCreation
{
    /// <summary>
    /// Логика взаимодействия для QuestionCreationWindow.xaml
    /// </summary>
    public partial class QuestionCreationWindow : UserControl
    {
        public QuestionCreationWindow()
        {
            InitializeComponent();
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
