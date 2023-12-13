using System;

namespace TestingPlace.Model.Testing.Answers
{
    public abstract class AbstractAnswerEntity : IEntity, IQuestionAnswer
    {
        public Guid Id { get; }
        public string Text { get; } = string.Empty;
        public string? ImagePath { get; set; }
        public double Points { get; set; }
        public Guid QuestionId { get; }

        public AbstractAnswerEntity(Guid id, Guid questionId, string text, double points = 0)
        {
            Id = id;
            Text = text;
            QuestionId = questionId;
            Points = points;
        }

        public abstract double Check(IQuestionAnswer answer);
    }
}
