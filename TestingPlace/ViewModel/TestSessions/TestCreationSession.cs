using System;
using System.Collections.Generic;
using System.Linq;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.ViewModel.TestSessions
{
    public class TestCreationSession : ITestCreationSession
    {
        public Guid TestId { get; } = Guid.NewGuid();

        public IDictionary<AbstractQuestionEntity, IList<AbstractAnswerEntity>> Questions { get; set; } = new Dictionary<AbstractQuestionEntity, IList<AbstractAnswerEntity>>();

        public Guid CurrentQuestionId { get; private set; }

        public IList<AbstractAnswerEntity> CurrentQuestionAnswers { get; set; } = new List<AbstractAnswerEntity>();

        public void GetQuestion(int index)
        {
            if (index >= Questions.Count) return;
            CurrentQuestionId = Questions.ElementAt(index).Key.Id;
            CurrentQuestionAnswers = Questions.ElementAt(index).Value;
        }

        public void NewQuestion()
        {
            CurrentQuestionId = Guid.NewGuid();
            CurrentQuestionAnswers = new List<AbstractAnswerEntity>();
        }
    }
}
