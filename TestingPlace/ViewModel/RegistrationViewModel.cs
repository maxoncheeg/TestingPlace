using System;
using System.ComponentModel;
using TestingPlace.Data;
using TestingPlace.ViewModel.Commands;

namespace TestingPlace.ViewModel
{
    internal class RegistrationViewModel : INotifyPropertyChanged
    {
        private string _login = string.Empty;
        private string _name = string.Empty;
        private string _password = string.Empty;
        private string _passwordRepeat = string.Empty;
        private string _email = string.Empty;
        private bool _isTeacher = false;

        private DataManager _manager;

        private PropertyChangedEventArgs _loginArgs = new(nameof(Login));
        private PropertyChangedEventArgs _nameArgs = new(nameof(Name));
        private PropertyChangedEventArgs _passwordArgs = new(nameof(Password));
        private PropertyChangedEventArgs _passwordRepeatArgs = new(nameof(PasswordRepeat));
        private PropertyChangedEventArgs _emailArgs = new(nameof(Email));
        private PropertyChangedEventArgs _isTeacherArgs = new(nameof(IsTeacher));

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

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, _nameArgs);
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

        public string PasswordRepeat
        {
            get => _passwordRepeat;
            set
            {
                _passwordRepeat = value;
                PropertyChanged?.Invoke(this, _passwordRepeatArgs);
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                PropertyChanged?.Invoke(this, _emailArgs);
            }
        }

        public bool IsTeacher
        {
            get => _isTeacher;
            set
            {
                _isTeacher = value;
                PropertyChanged?.Invoke(this, _isTeacherArgs);
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

        public RegistrationViewModel()
        {
            if (DataManager.Instance() is DataManager manager && manager != null)
                _manager = manager;
            else
                throw new InvalidOperationException();
        }
    }
}
