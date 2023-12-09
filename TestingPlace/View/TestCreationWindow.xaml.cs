using System.Windows;
using TestingPlace.View.UserControls.TestCreation;

namespace TestingPlace.ViewModel
{
    /// <summary>
    /// Логика взаимодействия для TestCreationWindow.xaml
    /// </summary>
    public partial class TestCreationWindow : Window
    {
        public TestCreationWindow()
        {
            InitializeComponent();

            TestCreationSessions.ITestCreationSession session = null;

            us.Content = new QuestionCreationWindow(session);
        }
    }
}
