using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlace.Model.Testing;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.ViewModel.Commands;
using TestingPlace.ViewModel.Services;
using TestingPlace.ViewModel.Services.Navigation;
using TestingPlace.ViewModel.TestSessions;

namespace TestingPlace.ViewModel.UserControls.TestCreation
{
    public class FinalCreationViewModel : BaseViewModel
    {
        private IDataManager _manager;
        private INavigationService _navigationService;
        private ITestCreationSession _session;

        private double _points;
        private int _questionsAmount;
        private string _title = string.Empty;

        #region Bindings
        public double PointsAmount
        {
            get => _points;
            set
            {
                _points = value;
                Notify();
            }
        }

        public int QuestionsAmount
        {
            get => _questionsAmount;
            set
            {
                _questionsAmount = value;
                Notify();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Notify();
            }
        }
        #endregion

        public Command Final => Command.Create(FinalMethod);
        private async void FinalMethod(object? sender, EventArgs args)
        {
            Test test = 
                new(_session.TestId, _title, (TestTheme)0, _manager.CurrentUser.Id, new List<ITestQuestion>(_session.Questions.Keys), new TimeSpan(0, 10, 0));

            _manager.TestRepository.Add(test);
            await _manager.TestRepository.SaveAsync();
            _navigationService.NavigateToPrevious();
        }

        public FinalCreationViewModel(IDataManager manager, INavigationService navigationService, ITestCreationSession session)
        {
            _manager = manager;
            _navigationService = navigationService;
            _session = session;

            PointsAmount = _session.Questions.Keys.Sum(q => q.GetPoints());
            QuestionsAmount = _session.Questions.Count();
        }
    }
}
