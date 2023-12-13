using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestingPlace.Model;
using TestingPlace.Model.Testing;
using TestingPlace.ViewModel.Managers;
using TestingPlace.ViewModel.UserControls;

namespace TestingPlace.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private UserControl _menuControl;
        private UserControl _testListControl;
        private IDataManager _manager;

        private UserControl? _actualControl = null;
        private string _login = string.Empty;
        private string _name = string.Empty;

        private double _averageTestPercent;
        private double _pointsAmount;

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
            get => (_manager.CurrentUser?.Solves.Count ?? 0).ToString();

        }

        public string AverageTestPercent
        {
            get => $"{_averageTestPercent * 100}%";
        }

        public double PointsAmount
        {
            get => _pointsAmount;
        }
        #endregion

        public MainViewModel(IDataManager manager, UserControl menuControl, UserControl testListControl)
        {
            _manager = manager;

            if (_manager.CurrentUser != null)
            {
                Login = _manager.CurrentUser.Login;
                Name = _manager.CurrentUser.Name;
            }

            _menuControl = menuControl;
            _testListControl = testListControl;

            ActualControl = _menuControl;

            if (_menuControl.DataContext is MainMenuViewModel model)
            {
                model.TestListButtonClicked += OnTestListButtonClicked;
            }
        }

        public async Task UpdateInfo()
        {
            double avg = await CalculateAverageTestPercent();
            _averageTestPercent = avg == double.NaN ? 0 : avg;
            Notify(nameof(AverageTestPercent));

            _pointsAmount = 0;
            await Task.Run(() => _pointsAmount = _manager.CurrentUser?.Solves.Sum(s => s.BestPoints) ?? 0);
            Notify(nameof(PointsAmount));
        }

        private void OnTestListButtonClicked()
        {
            ActualControl = _testListControl;
        }

        private async Task<double> CalculateAverageTestPercent()
        {
            if (_manager.CurrentUser == null) return 0;

            double sum = 0;
            int counter = 0;

            await Task.Run(() =>
            {
                foreach (var solve in _manager.CurrentUser.Solves)
                    if (solve.Test is IEntity entity && _manager.TestRepository.Get(entity.Id) is Test foundTest && foundTest != null)
                    {
                        counter++;
                        sum += solve.BestPoints / foundTest.GetTotalPoints();
                    }
            });

            double avg = sum / counter;
            if (avg > 1) avg = 1;
            else if (double.IsNaN(avg)) avg = 0;

            return Math.Round(avg, 2);
        }
    }
}
