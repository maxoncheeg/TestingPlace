using System;

namespace TestingPlace.Model.Testing.Answers
{
    public class QuestionAnswer : AbstractAnswerEntity, IQuestionAnswer
    {
        public QuestionAnswer(Guid id, Guid questionId, string text, double points = 0) 
            : base(id, questionId, text, points)
        {}

        public override double Check(IQuestionAnswer answer)
        {
            if (answer != null)
                return this.Equals(answer) ? Points : 0;
            return 0;
        }

        public bool Equals(IQuestionAnswer other) =>
            other is QuestionAnswer questionAnswer && questionAnswer.Text == Text;
    }
}
