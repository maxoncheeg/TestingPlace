using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.ViewModel.Commands;
using TestingPlace.ViewModel.Services.Navigation;
using TestingPlace.ViewModel.TestSessions;

namespace TestingPlace.ViewModel.UserControls.TestCreation
{
    internal class QuestionCreationViewModel : BaseViewModel
    {
        private bool _isVisibleRegistered;
        private INavigationService _navigation;
        private ITestCreationSession _session;

        private int _questionIndex;

        private int _answersAmount;
        private string _questionTitle = string.Empty;
        private string _imagePath = string.Empty;

        private ObservableCollection<ITestQuestion> _questions = new();

        public event Func<string>? GettingFilePath;
        public event Action<string, string>? MessageReceiving;

        #region Bindings
        public int QuestionIndex
        {
            get => _questionIndex;
            set
            {
                _questionIndex = value;
                Notify();
                UpdateInfo();
            }
        }

        public int AnswersAmount
        {
            get => _answersAmount;
            set
            {
                _answersAmount = value;
                Notify();
            }
        }

        public string QuestionTitle
        {
            get => _questionTitle;
            set
            {
                _questionTitle = value;
                Notify();
            }
        }

        public string QuestionPath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                Notify();
            }
        }

        public ObservableCollection<ITestQuestion> Questions
        {
            get => _questions;
            set
            {
                _questions = value;
                Notify();
            }
        }
        #endregion

        #region Commands
        public Command OpenImage => Command.Create(OpenImageMethod);
        private void OpenImageMethod(object? sender, EventArgs args)
        {
            QuestionPath = GettingFilePath?.Invoke() ?? string.Empty;
        }

        public Command SaveQuestion => Command.Create(SaveQuestionMethod);
        private void SaveQuestionMethod(object? sender, EventArgs args)
        {            
            QuestionAnswer? answer = _session.CurrentQuestionAnswers.FirstOrDefault(x => x.Points > 0) as QuestionAnswer;

            if (answer == null) {
                MessageReceiving?.Invoke("Внимание", "Вы не создали ни одного правильного ответа, с баллами отличными от 0!");
                return; 
            }

            var answers = new List<IQuestionAnswer>(_session.CurrentQuestionAnswers);
            answers.Remove(answer);
            DefaultQuestion question = new DefaultQuestion(Guid.NewGuid(), _session.TestId, QuestionTitle, answer, answers);

            if (_questionIndex == -1 || Questions.Count == 0)
                _session.Questions.Add(question, _session.CurrentQuestionAnswers);
            else
            {
                _session.Questions.Remove(_session.Questions.Keys.ElementAt(_questionIndex));
                _session.Questions.Add(question, _session.CurrentQuestionAnswers);
            }

            Questions = new(_session.Questions.Keys);
            QuestionIndex = Questions.IndexOf(question);
            UpdateInfo();
            DefineNewQuestion();
        }

        public Command DeleteQuestion => Command.Create(DeleteQuestionMethod);
        private void DeleteQuestionMethod(object? sender, EventArgs args)
        {
            if (QuestionIndex < _session.Questions.Count)
            {
                _session.CurrentQuestionAnswers.RemoveAt(QuestionIndex);
                Questions = new(_session.Questions.Keys);

                QuestionIndex = 0;//!!!
                UpdateInfo();
            }
        }

        public Command NewQuestion => Command.Create(NewQuestionMethod);
        private void NewQuestionMethod(object? sender, EventArgs args)
        {
            Questions = new(_session.Questions.Keys);
            QuestionIndex = -1;
            DefineNewQuestion();
        }

        public Command OpenAnswersRedactor => Command.Create(OpenAnswersRedactorMethod);
        private void OpenAnswersRedactorMethod(object? sender, EventArgs args)
        {
            if (!_isVisibleRegistered)
            {
                _navigation.RegisterOnCurrentWindowVisibleChanged(() => AnswersAmount = _session.CurrentQuestionAnswers.Count);
                _isVisibleRegistered = true;
            }
            _navigation.NavigateTo(TestingPlaceWindows.AnswerCreationWindow, new object[] { _session });
        }
        #endregion

        public QuestionCreationViewModel(INavigationService navigation, ITestCreationSession session)
        {
            _navigation = navigation;
            _session = session;
            Questions = new(_session.Questions.Keys);

            DefineNewQuestion();
        }

        private void DefineNewQuestion()
        {
            _session.NewQuestion();
            QuestionTitle = string.Empty;
            QuestionPath = string.Empty;
            AnswersAmount = _session.CurrentQuestionAnswers.Count;
        }

        private void UpdateInfo()
        {
            if (QuestionIndex >= 0 && Questions.Count > 0)
            {
                QuestionTitle = Questions[QuestionIndex].Text;
                AnswersAmount = _session.CurrentQuestionAnswers.Count;
            }
        }
    }
}


