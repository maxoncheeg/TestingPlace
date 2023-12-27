using System;
using System.Windows;
using TestingPlace.View.UserControls;
using TestingPlace.ViewModel;
using TestingPlace.ViewModel.Services;
using TestingPlace.ViewModel.TestSessions;
using TestingPlace.ViewModel.UserControls;

namespace TestingPlace.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public event EventHandler OnUpdating;

        public MainWindow()
        {
            InitializeComponent();

            //TestListControl testList = new(manager);
            //if (testList.DataContext is TestListViewModel testModel)
            //    testModel.TestSessionStarted += OnTestSessionStarted;

            //MainMenuControl main = new(manager);
            //if(main.DataContext is MainMenuViewModel mainModel)
            //    mainModel.CreateTestButtonClicked += OnCreateTestButtonClicked;


            //MainViewModel mainViewModel = new(manager, main, testList);
            //DataContext = mainViewModel;

            //OnUpdating += async (s, e) => await mainViewModel.UpdateInfo();
            // OnUpdating?.Invoke(this, EventArgs.Empty);

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                OnUpdating += async (s, e) => await viewModel.UpdateInfo();
                OnUpdating?.Invoke(this, EventArgs.Empty);
            }
            }

            private void OnCreateTestButtonClicked()
        {
            new TestCreationWindow().ShowDialog();
        }

        private void OnTestSessionStarted(ITestSession session)
        {
            //new TestSolveWindow(this, _dataManager, session).ShowDialog();
        }

        private void OnClosed(object? sender, EventArgs e)
        {
           // _window.Visibility = Visibility.Visible;
        }
    }
}
