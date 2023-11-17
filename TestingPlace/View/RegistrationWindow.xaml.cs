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
using TestingPlace.Data;
using TestingPlace.ViewModel;

namespace TestingPlace.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private readonly Window _previousWindow;
        public RegistrationWindow(Window previousWindow, IDataManager manager)
        {
            _previousWindow = previousWindow;
            _previousWindow.Hide();

            InitializeComponent();
            DataContext = new RegistrationViewModel(manager);

            if (DataContext is RegistrationViewModel registrationViewModel)
            {
                registrationViewModel.LoginError += OnError;
                registrationViewModel.LoginSuccess += OnSuccess;
            }

            Closed += OnClosed;
        }

        private void OnSuccess()
        {
            MessageBox.Show("Вы зареганы!!!!");
            Close();
        }

        private void OnError(string obj)
        {
            MessageBox.Show(obj);
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
            if (DataContext is RegistrationViewModel viewModel && sender is PasswordBox box)
                viewModel.Password = box.Password;
        }

        private void PasswordRepeatChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegistrationViewModel viewModel && sender is PasswordBox box)
                viewModel.PasswordRepeat = box.Password;
        }

        private void OnClosed(object? sender, EventArgs e)
        {
            _previousWindow.Show();
        }
    }
}
