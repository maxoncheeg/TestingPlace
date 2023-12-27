using System;
using TestingPlace.ViewModel.Commands;
using TestingPlace.ViewModel.Services;
using TestingPlace.ViewModel.Services.Navigation;

namespace TestingPlace.ViewModel
{
    class StartupViewModel : BaseViewModel
    {
        private string _login = string.Empty;
        private string _password = string.Empty;

        private IDataManager _manager;
        private INavigationService _navigation;

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
                _navigation.NavigateTo(TestingPlaceWindows.MainWindow);
            }
            else
            {
                LoginError?.Invoke("Не удалось войти. Ошибка в логине или пароле.");
            }
        }

        public Command Register => Command.Create(RegisterMethod);
        private void RegisterMethod(object? sender, EventArgs e)
        {
            _navigation.NavigateTo(TestingPlaceWindows.RegistrationWindow);
        }
        #endregion

        public StartupViewModel(INavigationService navigation, IDataManager manager)
        {
            _manager = manager;
            _navigation = navigation;
        }
    }
}
