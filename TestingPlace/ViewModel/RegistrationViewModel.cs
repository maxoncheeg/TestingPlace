using System;
using TestingPlace.Data;
using TestingPlace.ViewModel.Commands;

namespace TestingPlace.ViewModel
{
    internal class RegistrationViewModel : BaseViewModel
    {
        private string _login = string.Empty;
        private string _name = string.Empty;
        private string _password = string.Empty;
        private string _passwordRepeat = string.Empty;
        private string _email = string.Empty;
        private bool _isTeacher = false;

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

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
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

        public string PasswordRepeat
        {
            get => _passwordRepeat;
            set
            {
                _passwordRepeat = value;
                Notify();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                Notify();
            }
        }

        public bool IsTeacher
        {
            get => _isTeacher;
            set
            {
                _isTeacher = value;
                Notify();
            }
        }
        #endregion

        #region Commands

        public Command Register => Command.Create(RegisterMethod);

        private async void RegisterMethod(object? sender, EventArgs e)
        {
            string errorString = string.Empty;

            if (string.IsNullOrEmpty(Name))
                errorString += $"Пустое поле имени{Environment.NewLine}";
            //if (string.IsNullOrEmpty(Password))
            //  errorString += $"Пароль"
            // потом сделаем .!.

            if (Password != PasswordRepeat)
                errorString += $"Пароли не совпадают!{Environment.NewLine}";


            if (errorString != string.Empty)
            {
                LoginError?.Invoke(errorString);
                return;
            }

            if (_manager.TryRegisterUser(Login, Password, Name, Email, "none"))
            {
                await _manager.SaveAllAsync();
                LoginSuccess?.Invoke();
            }
        }
        #endregion

        public RegistrationViewModel(IDataManager manager)
        {
            _manager = manager;
        }
    }
}
