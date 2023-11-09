using System;
using System.Windows;
using TestingPlace.View.UserControls.TestQuestions;
using TestingPlace.ViewModel;

namespace TestingPlace.View
{
    /// <summary>
    /// Логика взаимодействия для TestSolveWindow.xaml
    /// </summary>
    public partial class TestSolveWindow : Window
    {
        private Window _previousWindow;

        public TestSolveWindow(Window window)
        {
            _previousWindow = window;

            InitializeComponent();

            this.Loaded += OnLoaded;
            this.Closed += OnClosed;

            DefaultQuestionControl control = new DefaultQuestionControl();
            //остальное

            DataContext = new TestSolveViewModel(control);
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
