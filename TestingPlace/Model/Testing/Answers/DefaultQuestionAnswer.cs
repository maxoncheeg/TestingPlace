using System;
using System.Windows.Documents;
using TestingPlace.Data.Tests;

namespace TestingPlace.Model.Testing.Answers
{
    public class DefaultQuestionAnswer : AnswerEntity,IQuestionAnswer, IAnswer
    {

        private DefaultQuestionAnswer(Guid id, Guid questionId, string text, double points = 0) : base(id, questionId, text, points)
        {
        }

        public static DefaultQuestionAnswer Create(Guid questionId, string text, double points = 0) =>
            new(Guid.NewGuid(), questionId, text, points);

        public static DefaultQuestionAnswer Create(Guid id, Guid questionId, string text, double points = 0) =>
            new(id, questionId, text, points);

        public double Check(IAnswer[] answer) =>
            answer[0].Equals(this) ? Points : 0;

        public bool Equals(IAnswer other) =>
            other is DefaultQuestionAnswer questionAnswer && questionAnswer.Text == Text;
    }
}
