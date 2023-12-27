using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestingPlace.View.MessageBoxes;
using TestingPlace.ViewModel;

namespace TestingPlace.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel registrationViewModel)
            {
                registrationViewModel.LoginError += OnError;
                registrationViewModel.LoginSuccess += OnSuccess;
            }
        }

        private void OnSuccess()
        {
            MessageBoxWindow.ShowMessage("Успех", "Вы зарегистрированы.");
            Close();
        }

        private void OnError(string message)
        {
            MessageBoxWindow.ShowMessage("Ошибка", "message");
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
            Close();
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel && sender is PasswordBox box)
                viewModel.Password = box.Password;
        }

        private void PasswordRepeatChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel && sender is PasswordBox box)
                viewModel.PasswordRepeat = box.Password;
        }
    }
}
