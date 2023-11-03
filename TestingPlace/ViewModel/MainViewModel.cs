using System.ComponentModel;
using System.Windows.Controls;
using TestingPlace.Data;
using TestingPlace.ViewModel.UserControls;

namespace TestingPlace.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private UserControl _menuControl;
        private UserControl _testListControl;
        private DataManager? _manager;

        private UserControl? _actualControl = null;
        private string _login = string.Empty;
        private string _name = string.Empty;

        private PropertyChangedEventArgs _actualControlArgs = new(nameof(ActualControl));
        private PropertyChangedEventArgs _loginArgs = new(nameof(Login));
        private PropertyChangedEventArgs _nameArgs = new(nameof(Name));

        public event PropertyChangedEventHandler? PropertyChanged;

        #region Bindings
        public UserControl? ActualControl
        {
            get => _actualControl;
            set
            {
                _actualControl = value;
                PropertyChanged?.Invoke(this, _actualControlArgs);
            }
        }

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

        public string SolvedTests
        {
            get 
            {
                if (_manager != null && _manager.CurrentUser != null)
                    return _manager.CurrentUser.SolvedTestsIds.Count.ToString();
                return "0";
            }
        }
        #endregion

        public MainViewModel(UserControl menuControl, UserControl testListControl)
        {
            _manager = DataManager.Instance();

            if (_manager != null && _manager.CurrentUser != null)
            {
                Login = _manager.CurrentUser.Login;
                Name = _manager.CurrentUser.Name;
            }

            _menuControl = menuControl;
            _testListControl = testListControl;

            ActualControl = _menuControl;

            if(_menuControl.DataContext is MainMenuViewModel model)
            {
                model.TestListButtonClicked += OnTestListButtonClicked;
            }
        }

        private void OnTestListButtonClicked()
        {
            ActualControl = _testListControl;
        }
    }
}
