using System;
using System.Windows;
using TestingPlace.Model.Testing.TestSessions;
using TestingPlace.View.MessageBoxes;
using TestingPlace.View.UserControls.TestQuestions;
using TestingPlace.ViewModel;
using TestingPlace.ViewModel.Managers;

namespace TestingPlace.View
{
    /// <summary>
    /// Логика взаимодействия для TestSolveWindow.xaml
    /// </summary>
    public partial class TestSolveWindow : Window
    {
        private Window _previousWindow;

        public TestSolveWindow(Window window, IDataManager manager, ITestSession session)
        {
            _previousWindow = window;

            InitializeComponent();

            this.Loaded += OnLoaded;
            this.Closed += OnClosed;

            DefaultQuestionControl control = new DefaultQuestionControl(session);

            TestSolveViewModel context = new TestSolveViewModel(manager, session, control);
            DataContext = context;

            context.OnMessage += OnMessage;
            session.TestCompleted += () => this.Close();
        }

        private void OnMessage(string title, string message)
        {
            MessageBoxWindow.ShowMessage(title, message);
        }

        private void OnClosed(object? sender, EventArgs e)
        {
            _previousWindow.Visibility = Visibility.Visible;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _previousWindow.Hide();
        }
    }
}
