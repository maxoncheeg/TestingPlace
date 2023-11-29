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
using System.Windows.Shapes;
using TestingPlace.Model.Testing.TestSessions;
using TestingPlace.View.UserControls;
using TestingPlace.ViewModel;
using TestingPlace.ViewModel.Managers;
using TestingPlace.ViewModel.UserControls;

namespace TestingPlace.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataManager _dataManager;
        private Window _window;
        public MainWindow(Window window, IDataManager manager)
        {
            _dataManager = manager;
            _window = window;
            _window.Hide();

            InitializeComponent();
            Closed += OnClosed;

            TestListControl control = new(manager);
            if (control.DataContext is TestListViewModel model)
                model.TestSessionStarted += OnTestSessionStarted;

            MainViewModel mainViewModel = new(_dataManager, new MainMenuControl(), control);
            DataContext = mainViewModel;
        }

        private void OnTestSessionStarted(ITestSession session)
        {
      
            new TestSolveWindow(this, _dataManager, session).ShowDialog();
        }

        private void OnClosed(object? sender, EventArgs e)
        {
            _window.Visibility = Visibility.Visible;
        }
    }
}
