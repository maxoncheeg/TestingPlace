using System.Windows.Controls;
using TestingPlace.ViewModel.TestSessions;
using TestingPlace.ViewModel.UserControls.TestQuestions;

namespace TestingPlace.View.UserControls.TestQuestions
{
    /// <summary>
    /// Логика взаимодействия для DefaultQuestionControl.xaml
    /// </summary>
    public partial class DefaultQuestionControl : UserControl
    {
        public DefaultQuestionControl(ITestSession session)
        {
            DataContext = new DefaultQuestionViewModel(session);
            InitializeComponent();
        }
    }
}
