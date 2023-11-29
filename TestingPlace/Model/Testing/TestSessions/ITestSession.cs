using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.TestSessions
{
    public interface ITestSession
    {
        public Test Test { get; }
        public int CurrentQuestionIndex { get; }
        public IReadOnlyDictionary<int, IQuestionAnswer> Answers { get; }
        public ObservableCollection<int> CurrentSelectedAnswerIndexes { get; set; }

        public event EventHandler<QuestionEventArgs> QuestionChanged;
        public event Action QuestionAnswered;
        public event Action TestCompleted;

        public bool NextQuestion();
        public bool PreviousQuestion();
        public void Answer(IQuestionAnswer answer);
        public void Complete();
    }
}
