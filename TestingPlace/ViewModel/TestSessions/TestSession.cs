using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestingPlace.Model.Testing;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.ViewModel.TestSessions
{
    public class TestSession(Test test) : ITestSession
    {
        private Test _test = test;
        private int _currentQuestionIndex = 0;
        private Dictionary<int, IQuestionAnswer> _answers = new();

        public Test Test => _test;
        public int CurrentQuestionIndex => _currentQuestionIndex;

        public ObservableCollection<int> CurrentSelectedAnswerIndexes { get; set; } = new();
        public IReadOnlyDictionary<int, IQuestionAnswer> Answers => _answers;

        public event EventHandler<QuestionEventArgs>? QuestionChanged;
        public event Action? QuestionAnswered;
        public event Action? TestCompleted;

        public void Answer(IQuestionAnswer answer)
        {
            if (_answers.ContainsKey(CurrentQuestionIndex))
            {
                _answers[CurrentQuestionIndex] = answer;
            }
            else _answers.Add(CurrentQuestionIndex, answer);

            QuestionAnswered?.Invoke();
        }

        public void Complete()
        {
            TestCompleted?.Invoke();
        }

        public bool NextQuestion()
        {
            if (_test.QuestionCount > _currentQuestionIndex + 1)
            {
                _currentQuestionIndex++;
                QuestionChanged?.Invoke(this, new(_test[_currentQuestionIndex]));
                return true;
            }

            return false;
        }

        public bool PreviousQuestion()
        {
            if (_currentQuestionIndex - 1 >= 0)
            {
                _currentQuestionIndex--;
                QuestionChanged?.Invoke(this, new(_test[_currentQuestionIndex]));
                return true;
            }

            return false;
        }
    }
}
