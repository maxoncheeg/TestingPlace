using System;
using System.Windows.Controls;
using TestingPlace.ViewModel.Commands;
using TestingPlace.ViewModel.Services;
using TestingPlace.ViewModel.Services.Navigation;
using TestingPlace.ViewModel.TestSessions;

namespace TestingPlace.ViewModel
{
    public class TestCreationViewModel : BaseViewModel
    {
        private UserControl? _actualControl = null;

        private ITestCreationSession _session;
        private INavigationService _navigation;
        private IDataManager _manager;

        private Enum _currentControl;

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
        #endregion

        #region Commands
        public Command QuestionCreation => Command.Create(QuestionCreationMethod);
        private void QuestionCreationMethod(object? sender, EventArgs e)
        {
            if (!_currentControl.Equals(TestingPlaceUserControls.TestCreationWindow_QuestionCreation))
            {
                _currentControl = TestingPlaceUserControls.TestCreationWindow_QuestionCreation;
                ActualControl = _navigation.ShowCurrentWindowUserControl(TestingPlaceUserControls.TestCreationWindow_QuestionCreation, new object[] { _navigation, _session });
            }
        }

        public Command TestCreation => Command.Create(TestCreationMethod);
        private void TestCreationMethod(object? sender, EventArgs e)
        {
            if (!_currentControl.Equals(TestingPlaceUserControls.TestCreationWindow_FinalCreation))
            {
                _currentControl = TestingPlaceUserControls.TestCreationWindow_FinalCreation;
                ActualControl = _navigation.ShowCurrentWindowUserControl(TestingPlaceUserControls.TestCreationWindow_FinalCreation, new object[] { _manager, _navigation, _session });
            }
        }

        #endregion

        public TestCreationViewModel(INavigationService navigation, IDataManager manager, ITestCreationSession session)
        {
            _session = session;
            _manager = manager;
            _navigation = navigation;

            _currentControl = TestingPlaceWindows.TestCreationWindow;
            QuestionCreationMethod(this, EventArgs.Empty);
        }
    }
}
