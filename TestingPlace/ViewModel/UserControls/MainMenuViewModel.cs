using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlace.ViewModel.Commands;

namespace TestingPlace.ViewModel.UserControls
{
    internal class MainMenuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public event Action TestListButtonClicked;

        public Command TestListOpen => Command.Create(TestListOpenMethod);
        private void TestListOpenMethod(object? sender, EventArgs e)
        {
            TestListButtonClicked?.Invoke();
        }

        public MainMenuViewModel() { }


    }
}
