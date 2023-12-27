using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TestingPlace.ViewModel.Services.Navigation
{
    public enum NavigationServiceSettings
    {
        HidePreviousWindow
    }

    public class NavigationService : INavigationService
    {
        private Stack<KeyValuePair<Window, INavigationWindow?>> _openedWindows = new();
        private Dictionary<Enum, INavigationWindow> _windows;
        private KeyValuePair<Window, INavigationWindow?> _currentWindow;
        private List<NavigationServiceSettings>? _settings;

        public event Action<UserControl>? UserControlNavigating;

        public NavigationService(Window currentWindow, List<NavigationServiceSettings>? settings = null)
        {
            _windows = new();
            _currentWindow = KeyValuePair.Create<Window, INavigationWindow?>(currentWindow, null);
            _settings = settings;
        }
        public void NavigateTo(Enum key, object[]? parameters = null)
        {
            foreach (var navigation in _windows)
                if (navigation.Key.Equals(key))
                {
                    if (_currentWindow.Key != null)
                    {
                        if (_settings != null && _settings.Contains(NavigationServiceSettings.HidePreviousWindow))
                            _currentWindow.Key.Hide();
                        _openedWindows.Push(_currentWindow);
                    }

                    _currentWindow = KeyValuePair.Create<Window, INavigationWindow?>(null, navigation.Value);

                    if(parameters != null)
                    foreach (var obj in parameters)
                        navigation.Value.Content.AddParameter(obj);

                    Window window = navigation.Value.Instance() as Window
                        ?? throw new ApplicationException(navigation.ToString());

                    _currentWindow = KeyValuePair.Create<Window, INavigationWindow?>(window, navigation.Value);
                    _currentWindow.Key.Closing += OnWindowClosed;
                    _currentWindow.Key.ShowDialog();
                }
        }

        private void OnWindowClosed(object? sender, EventArgs e)
        {
            if (_currentWindow.Key.Visibility == Visibility.Visible)
            {
                _currentWindow = _openedWindows.Pop();
                if (_settings != null && _settings.Contains(NavigationServiceSettings.HidePreviousWindow))
                    _currentWindow.Key.Visibility = Visibility.Visible;
            }
        }

        public void NavigateToPrevious()
        {
            _currentWindow.Key.Visibility = Visibility.Hidden;
            _currentWindow.Key.Close();
            _currentWindow = _openedWindows.Pop();
            if (_settings != null && _settings.Contains(NavigationServiceSettings.HidePreviousWindow))
                _currentWindow.Key.Visibility = Visibility.Visible;
        }

        public void RegisterOnCurrentWindowVisibleChanged(Action method)
        {
            _currentWindow.Key.IsVisibleChanged += (_, _) => method();
        }

        public bool Register(Enum key, INavigationWindow window)
        {
            _windows.Add(key, window);
            return true;
        }

        public UserControl? ShowCurrentWindowUserControl(Enum key, object[] parameters)
        {
            if (_currentWindow.Value != null)
                return _currentWindow.Value.InstanceUserControl(key, parameters) as UserControl;
            return null;
        }

        public void SendUserControl(UserControl userControl)
        {
            UserControlNavigating?.Invoke(userControl);
        }
    }
}
