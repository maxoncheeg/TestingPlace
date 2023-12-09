using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using TestingPlace.Model.Testing;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.ViewModel.Commands;
using TestingPlace.ViewModel.Managers;
using TestingPlace.ViewModel.TestSessions;

namespace TestingPlace.ViewModel
{
    internal class TestSolveViewModel : BaseViewModel
    {
        private ITestSession _testSession;
        private IDataManager _manager;
        private UserControl _defaultQuestionControl;

        private bool _isPrevCommandEnabled;
        private bool _isNextCommandEnabled;

        private bool _isUserNotifiedAboutAnswers = false;

        public event Action<string, string>? OnMessage;

        #region Bindings
        public UserControl ActualControl
        {
            get => _defaultQuestionControl;
            set
            {
                _defaultQuestionControl = value;
                Notify();
            }
        }

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

                switch (_testSession.Test[_testSession.CurrentQuestionIndex])
                {
                    case DefaultQuestion:
                        ActualControl = _defaultQuestionControl;
                        break;
                }
                 
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

                switch (_testSession.Test[_testSession.CurrentQuestionIndex])
                {
                    case DefaultQuestion:
                        ActualControl = _defaultQuestionControl;
                        break;
                }

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
                if (_manager.CurrentUser.Solves.FirstOrDefault(x => x.TestId == _testSession.Test.Id) is TestSolveEntity testSolve
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

        public TestSolveViewModel(IDataManager manager, ITestSession session, UserControl defaultQuestionControl)
        {
            if (session == null) throw new ArgumentNullException();
            if (manager == null) throw new ArgumentNullException();

            _manager = manager;
            _testSession = session;
            _defaultQuestionControl = defaultQuestionControl;

            UpdateCommandEnable();

            _testSession.QuestionAnswered += OnQuestionAnswered;
            Notify(nameof(QuestionCount));
            switch (_testSession.Test[_testSession.CurrentQuestionIndex])
            {
                case DefaultQuestion:
                    ActualControl = _defaultQuestionControl;
                    break;
            }
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
    }
}
