using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestingPlace.View.MessageBoxes;
using TestingPlace.ViewModel;

namespace TestingPlace.View
{
    /// <summary>
    /// Логика взаимодействия для StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is StartupViewModel viewModel)
            {
                viewModel.LoginError += ShowErrorWindow;
            }
        }

        private void ShowErrorWindow(string message)
        {
            //MessageBox.Show(message);
            MessageBoxWindow.ShowMessage("Ошибка", message);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(DataContext is StartupViewModel viewModel && sender is PasswordBox box)
                viewModel.Password = box.Password;
        }
    }
}
