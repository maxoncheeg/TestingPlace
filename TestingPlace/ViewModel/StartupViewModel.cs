using System;
using TestingPlace.Data;
using TestingPlace.ViewModel.Commands;

namespace TestingPlace.ViewModel
{
    class StartupViewModel : BaseViewModel
    {
        private string _login = string.Empty;
        private string _password = string.Empty;

        private IDataManager _manager;

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
                Notify();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                Notify();
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

        public StartupViewModel(IDataManager manager)
        {
            _manager = manager;
        }
    }
}
