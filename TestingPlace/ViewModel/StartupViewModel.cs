using System;
using System.ComponentModel;
using System.Windows;
using TestingPlace.ViewModel.Commands;

namespace TestingPlace.ViewModel
{
    class StartupViewModel : INotifyPropertyChanged
    {
        private string _login;
        private string _password;

        private PropertyChangedEventArgs _loginArgs  = new(nameof(Login));
        private PropertyChangedEventArgs _passwordArgs = new(nameof(Password));

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Bindings
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                PropertyChanged?.Invoke(this, _loginArgs);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, _passwordArgs);
            }
        }
        #endregion

        #region Commands
        public Command StartTest => Command.Create(StartTestMethod);
        private void StartTestMethod(object? sender, EventArgs e)
        {
            MessageBox.Show($"Уважаемый {_login}, начинаем тестирование. Ваш пароль - {_password}");
        }

        public Command Register => Command.Create(RegisterMethod);
        private void RegisterMethod(object? sender, EventArgs e)
        {
            MessageBox.Show("НЕЛЬЗЯ РЕГИСТРИРОВАТЬСЯ");
        }
        #endregion

        public StartupViewModel()
        {

        }
    }
}
