using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using TestingPlace.Model.Testing;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.ViewModel.Commands;
using TestingPlace.ViewModel.Services;
using TestingPlace.ViewModel.Services.Navigation;
using TestingPlace.ViewModel.TestSessions;

namespace TestingPlace.ViewModel
{
    internal class TestSolveViewModel : BaseViewModel
    {
        private int _selectedIndex = -1;
        private string _title = string.Empty;
        private ObservableCollection<IQuestionAnswer> _answers = new();

        private ITestSession _testSession;
        private IDataManager _manager;
        private INavigationService _navigation;

        private bool _isPrevCommandEnabled;
        private bool _isNextCommandEnabled;

        private bool _isUserNotifiedAboutAnswers = false;

        public event Action<string, string>? OnMessage;

        #region Bindings
        public int QuestionNumber => _testSession.CurrentQuestionIndex + 1;

        public int QuestionCount => _testSession.Test.QuestionCount;

        public int AnsweredQuestionCount => _testSession.Answers.Count;

        public bool IsPrevCommandEnabled
        {
            get => _isPrevCommandEnabled;
            set
            {
                _isPrevCommandEnabled = value;
                Notify();
            }
        }

        public bool IsNextCommandEnabled
        {
            get => _isNextCommandEnabled;
            set
            {
                _isNextCommandEnabled = value;
                Notify();
            }
        }

        public ObservableCollection<IQuestionAnswer> Answers
        {
            get => _answers;
            set
            {
                _answers = value;
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

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;

                if (SelectedIndex != -1 && _testSession != null)
                    _testSession.Answer(Answers[SelectedIndex]);

                Notify();
            }
        }
        #endregion

        #region Commands
        public Command GetNextQuestion => Command.Create(GetNextQuestionMethod);
        public void GetNextQuestionMethod(object? sender, EventArgs args)
        {
            if (_testSession != null)
            {
                if (!_testSession.NextQuestion())
                    OnMessage?.Invoke("Внимание", "Дальше вопросов нет");

                UpdateCommandEnable();

                Notify(nameof(QuestionNumber));
                Notify(nameof(AnsweredQuestionCount));
            }
        }

        public Command GetPreviousQuestion => Command.Create(GetPreviousQuestionMethod);
        public void GetPreviousQuestionMethod(object? sender, EventArgs args)
        {
            if (_testSession != null)
            {
                if (!_testSession.PreviousQuestion())
                    OnMessage?.Invoke("Внимание", "Дальше вопросов нет");

                UpdateCommandEnable();

                Notify(nameof(QuestionNumber));
                Notify(nameof(AnsweredQuestionCount));
            }
        }

        public Command CompleteTest => Command.Create(CompleteTestMethod);
        private async void CompleteTestMethod(object? sender, EventArgs args)
        {
            double points = 0;
            foreach (KeyValuePair<int, IQuestionAnswer> pair in _testSession.Answers)
            {
                points += _testSession.Test[pair.Key].Answer(pair.Value);
            }

            if (_manager.CurrentUser != null)
            {
                if (_manager.CurrentUser.Solves.FirstOrDefault(x => x.TestId == _testSession.Test.Id) is ITestSolve testSolve
                    && testSolve != null)
                {
                    testSolve.Attempts++;
                    if (testSolve.BestPoints < points) testSolve.BestPoints = points;
                }
                else
                {
                    TestSolveEntity entity = new(Guid.NewGuid(), _testSession.Test.Id, _manager.CurrentUser.Id, points, 1);
                    _manager.CurrentUser.Solves.Add(entity);
                }

                _manager.UserRepository.Update(_manager.CurrentUser);
                await _manager.UserRepository.SaveAsync();
            }

            OnMessage?.Invoke("Тест пройден", $"ВЫ ЗАРАБОТАЛИ {points} ИЗ {_testSession.Test.GetTotalPoints()}");
            _testSession.Complete();
        }
        #endregion

        public TestSolveViewModel(INavigationService navigation, IDataManager manager, ITestSession session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            if (manager == null) throw new ArgumentNullException(nameof(manager));
            if (navigation == null) throw new ArgumentNullException(nameof(navigation));

            _manager = manager;
            _testSession = session;
            _navigation = navigation;   

            UpdateCommandEnable();

            _testSession.QuestionAnswered += OnQuestionAnswered;
            Notify(nameof(QuestionCount));

            _testSession.QuestionChanged += OnQuestionChanged;
            OnQuestionChanged(this, new(_testSession.Test[_testSession.CurrentQuestionIndex]));
        }

        private void OnQuestionAnswered()
        {
            if (AnsweredQuestionCount == _testSession.Test.QuestionCount && !_isUserNotifiedAboutAnswers)
            {
                OnMessage?.Invoke("Внимание", "Даны ответы на все вопросы!");
                _isUserNotifiedAboutAnswers = true;
            }
            Notify(nameof(AnsweredQuestionCount));
        }

        private void UpdateCommandEnable()
        {
            IsPrevCommandEnabled = _testSession.CurrentQuestionIndex - 1 >= 0;
            IsNextCommandEnabled = _testSession.CurrentQuestionIndex + 1 < _testSession.Test.QuestionCount;
        }

        private void OnQuestionChanged(object? sender, QuestionEventArgs e)
        {
            if (e.Question is DefaultQuestion question)
            {
                Answers = new(question.Answers);
                if (_testSession.Answers.ContainsKey(_testSession.CurrentQuestionIndex) && _testSession.Answers[_testSession.CurrentQuestionIndex] is QuestionAnswer answer)
                    SelectedIndex = Answers.IndexOf(answer);
                else SelectedIndex = -1;

                Title = question.Text;
            }
        }
    }
}
