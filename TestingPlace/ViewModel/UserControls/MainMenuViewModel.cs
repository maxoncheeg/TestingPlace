using System;
using TestingPlace.ViewModel.Commands;
using TestingPlace.ViewModel.Services;
using TestingPlace.ViewModel.Services.Navigation;

namespace TestingPlace.ViewModel.UserControls
{
    internal class MainMenuViewModel : BaseViewModel
    {
        private IDataManager _manager;
        INavigationService _navigation;

        public event Action? TestListButtonClicked;

        public event Action<string, string>? OnMessage;

        public Command TestListOpen => Command.Create(TestListOpenMethod);
        private void TestListOpenMethod(object? sender, EventArgs e)
        {
            _navigation.SendUserControl(
                _navigation.ShowCurrentWindowUserControl(TestingPlaceUserControls.MainWindow_TestList, new object[]{ _navigation, _manager}));
        }

        public Command CreateTest => Command.Create(CreateTestMethod);
        private void CreateTestMethod(object? sender, EventArgs e)
        {
            if (_manager.CurrentUser?.IsTeacher ?? false)
                _navigation.NavigateTo(TestingPlaceWindows.TestCreationWindow);
            else
                OnMessage?.Invoke("Внимание!", "Вы не являетесь учителем и не можете создавать тесты");
        }

        public MainMenuViewModel(INavigationService navigation, IDataManager manager) { _navigation = navigation; _manager = manager; }
    }
}
