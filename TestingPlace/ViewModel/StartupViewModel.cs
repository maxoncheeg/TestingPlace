using System;
using System.ComponentModel;
using TestingPlace.Data;
using System.Linq;
using TestingPlace.ViewModel.Commands;
using System.Security.Cryptography;
using System.Text;

namespace TestingPlace.ViewModel
{
    class StartupViewModel : INotifyPropertyChanged
    {
        private string _login = string.Empty;
        private string _password = string.Empty;

        private DataManager _manager;

        private PropertyChangedEventArgs _loginArgs = new(nameof(Login));
        private PropertyChangedEventArgs _passwordArgs = new(nameof(Password));

        public event PropertyChangedEventHandler? PropertyChanged;

        public event Action? RegistrationClicked;
        public event Action? LoginSuccess;
        public event Action<string>? LoginError;

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
            if (_manager.TryAuthorizeUser(Login, Password))
            {
                LoginSuccess?.Invoke();
            }
            else
            {
                LoginError?.Invoke("Не удалось войти. Ошибка в логине или пароле.");
            }
        }

        public Command Register => Command.Create(RegisterMethod);
        private void RegisterMethod(object? sender, EventArgs e)
        {
            RegistrationClicked?.Invoke();
        }
        #endregion

        public StartupViewModel()
        {
            if (DataManager.Instance() is DataManager manager && manager != null)
                _manager = manager;
            else
                throw new InvalidOperationException();
        }
    }
}
