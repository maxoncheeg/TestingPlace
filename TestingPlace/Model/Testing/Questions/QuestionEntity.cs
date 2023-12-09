using System;

namespace TestingPlace.Model.Testing.Questions
{
    public class QuestionEntity : IEntity, IQuestion
    {
        public Guid Id { get; }
        public string Type { get; } = string.Empty;
        public string Text { get; } = string.Empty;
        public string? ImagePath { get; set; }
        public Guid TestId { get; }

        public QuestionEntity(Guid id, string type, string text, Guid testId)
        {
            Id = id;
            Type = type;
            Text = text;
            TestId = testId;
        }
    }
}
