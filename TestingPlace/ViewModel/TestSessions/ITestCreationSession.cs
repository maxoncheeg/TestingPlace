using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.ViewModel.TestSessions
{
    public interface ITestCreationSession
    {
        public Guid TestId { get; }

        public IDictionary<AbstractQuestionEntity, IList<AbstractAnswerEntity>> Questions { get; set; }
        public IList<AbstractAnswerEntity> CurrentQuestionAnswers { get; set; }

        public Guid CurrentQuestionId { get; }

        public void NewQuestion();
        public void GetQuestion(int index);
    }
}
