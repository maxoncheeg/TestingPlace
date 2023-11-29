using System;

namespace TestingPlace.Model.Testing.Answers
{
    public class QuestionAnswer : AnswerEntity, IQuestionAnswer
    {
        private QuestionAnswer(Guid id, Guid questionId, string text, double points = 0) 
            : base(id, questionId, text, points)
        {}

        public static QuestionAnswer Create(string text, double points = 0) =>
            new(Guid.NewGuid(), Guid.NewGuid(), text, points); //УБРАТЬ???

        public static QuestionAnswer Create(Guid questionId, string text, double points = 0) =>
            new(Guid.NewGuid(), questionId, text, points);

        public static QuestionAnswer Create(Guid id, Guid questionId, string text, double points = 0) =>
            new(id, questionId, text, points);

        public double Check(IQuestionAnswer answer)
        {
            if (answer != null)
                return this.Equals(answer) ? Points : 0;
            return 0;
        }

        public bool Equals(IQuestionAnswer other) =>
            other is QuestionAnswer questionAnswer && questionAnswer.Text == Text;
    }
}
