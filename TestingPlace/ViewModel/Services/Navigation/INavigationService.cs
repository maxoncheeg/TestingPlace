using System;
using System.Windows.Controls;

namespace TestingPlace.ViewModel.Services.Navigation
{
    public interface INavigationService
    {
        public event Action<UserControl>? UserControlNavigating;
        public bool Register(Enum key, INavigationWindow window);
        public void RegisterOnCurrentWindowVisibleChanged(Action method);
        public void NavigateTo(Enum key, object[]? parameters = null);
        public void NavigateToPrevious();
        public UserControl? ShowCurrentWindowUserControl(Enum key, object[] parameters);
        public void SendUserControl(UserControl control);
    }
}
