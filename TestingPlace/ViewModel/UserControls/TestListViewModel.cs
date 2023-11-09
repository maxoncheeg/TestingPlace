using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using TestingPlace.Data;
using TestingPlace.Data.Tests;
using TestingPlace.Model.Testing;
using TestingPlace.ViewModel.Commands;

namespace TestingPlace.ViewModel.UserControls
{
    internal class TestListViewModel : BaseViewModel
    {
        private DataManager _manager;
        private bool _isSearching = false;

        private ObservableCollection<Test> _tests = new();
        private List<string> _testThemes = new();
        private Visibility _filterVisibility = Visibility.Collapsed;
        private int _listSelectedIndex = 0;
        private int _themeIndex = 0;
        private string _search = string.Empty;

        public event Action? TestSessionStarted;

        #region Bindings
        public ObservableCollection<Test> Tests
        {
            get => _tests;
            private set
            {
                _tests = value;
                Notify();
            }
        }

        public Visibility FilterVisibility
        {
            get => _filterVisibility;
            set
            {
                _filterVisibility = value;
                Notify();
            }
        }

        public int ListSelectedIndex
        {
            get => _listSelectedIndex;
            set
            {
                _listSelectedIndex = value;
                Notify();
            }
        }

        public List<string> TestThemes
        {
            get => _testThemes;
            set
            {
                _testThemes = value;
                Notify();
            }
        }

        public int ThemeIndex
        {
            get => _themeIndex;
            set
            {
                if (_themeIndex == value) return;

                _themeIndex = value;
                SortList();
                Notify();
            }
        }

        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                SortList();
                Notify();
            }
        }
        #endregion

        #region Commands
        public Command ChangeFilterVisibility => Command.Create(ChangeFilterVisibilityMethod);
        private void ChangeFilterVisibilityMethod(object? sender, EventArgs args)
        {
            if (FilterVisibility is Visibility.Collapsed)
                FilterVisibility = Visibility.Visible;
            else FilterVisibility = Visibility.Collapsed;
        }

        public Command OpenTest => Command.Create(OpenTestMethod);
        private void OpenTestMethod(object? sender, EventArgs args)
        {
            if (_listSelectedIndex < 0 || _tests.Count <= _listSelectedIndex) return;

            if (_manager.StartTestSession(_tests[_listSelectedIndex])) 
                TestSessionStarted?.Invoke();
        }
        #endregion

        public TestListViewModel()
        {
            if (DataManager.Instance() is DataManager manager && manager != null)
                _manager = manager;
            else
                throw new InvalidOperationException();

            Tests = new(_manager.TestRepository.GetAll());
            ListSelectedIndex = 0;
            FilterVisibility = Visibility.Collapsed;

            List<string> themes = new();

            int i = 0;
            while (Enum.IsDefined(typeof(TestTheme), i))
                themes.Add(((TestTheme)i++).ToString());

            TestThemes = themes;
            ThemeIndex = 0;
        }

        private async void SortList()
        {
            await Task.Run(async () =>
            {
                if (Search == string.Empty && ThemeIndex == (int)TestTheme.Любая)
                    Tests = new(_manager.TestRepository.GetAll());
                else Tests = new();

                if (ThemeIndex == (int)TestTheme.Любая)
                    foreach (var test in _manager.TestRepository.GetAll())
                    {
                        if (test.Name.ToLower().Contains(Search.ToLower()))
                            await Application.Current.Dispatcher.BeginInvoke(() => Tests.Add(test));
                    }
                else if (Search == string.Empty)
                    foreach (var test in _manager.TestRepository.GetAll())
                    {
                        if ((int)test.Theme == ThemeIndex)
                            await Application.Current.Dispatcher.BeginInvoke(() => Tests.Add(test));
                    }
                else
                    foreach (var test in _manager.TestRepository.GetAll())
                    {
                        if (test.Name.ToLower().Contains(Search.ToLower()) && (int)test.Theme == ThemeIndex)
                            await Application.Current.Dispatcher.BeginInvoke(() => Tests.Add(test));
                    }
            });
        }
    }
}
