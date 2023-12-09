using System;
using TestingPlace.ViewModel.Commands;
using TestingPlace.ViewModel.Managers;

namespace TestingPlace.ViewModel.UserControls
{
    internal class MainMenuViewModel : BaseViewModel
    {
        private IDataManager _manager;

        public event Action? TestListButtonClicked;
        public event Action? CreateTestButtonClicked;

        public event Action<string, string>? OnMessage;

        public Command TestListOpen => Command.Create(TestListOpenMethod);
        private void TestListOpenMethod(object? sender, EventArgs e)
        {
            TestListButtonClicked?.Invoke();
        }

        public Command CreateTest => Command.Create(CreateTestMethod);
        private void CreateTestMethod(object? sender, EventArgs e)
        {
            if (_manager.CurrentUser?.IsTeacher ?? false)
                CreateTestButtonClicked?.Invoke();
            else
                OnMessage?.Invoke("Внимание!", "Вы не являетесь учителем и не можете создавать тесты");
        }

        public MainMenuViewModel(IDataManager manager) { _manager = manager; }


    }
}
