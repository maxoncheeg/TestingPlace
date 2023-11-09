using System.ComponentModel;
using System.Windows.Controls;
using TestingPlace.Data;
using TestingPlace.ViewModel.UserControls;

namespace TestingPlace.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private UserControl _menuControl;
        private UserControl _testListControl;
        private DataManager? _manager;

        private UserControl? _actualControl = null;
        private string _login = string.Empty;
        private string _name = string.Empty;

        #region Bindings
        public UserControl? ActualControl
        {
            get => _actualControl;
            set
            {
                _actualControl = value;
                Notify();
            }
        }

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
