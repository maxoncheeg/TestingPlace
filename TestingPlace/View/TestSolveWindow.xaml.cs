using System;
using System.Windows;
using TestingPlace.View.MessageBoxes;
using TestingPlace.ViewModel;
using TestingPlace.ViewModel.Services;
using TestingPlace.ViewModel.TestSessions;

namespace TestingPlace.View
{
    /// <summary>
    /// Логика взаимодействия для TestSolveWindow.xaml
    /// </summary>
    public partial class TestSolveWindow : Window
    {
        public TestSolveWindow()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
        }

        private void OnMessage(string title, string message)
        {
            MessageBoxWindow.ShowMessage(title, message);
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is TestSolveViewModel viewModel)
            {
                viewModel.OnMessage += OnMessage;
            }
        }
    }
}
