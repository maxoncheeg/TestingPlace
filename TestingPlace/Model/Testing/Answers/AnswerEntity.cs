using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlace.Model;

namespace TestingPlace.Model.Testing.Answers
{
    public class AnswerEntity : IEntity
    {
        public Guid Id { get; }
        public string Text { get; } = string.Empty;
        public string? ImagePath { get; set; }
        public double Points { get; set; }
        public Guid QuestionId { get; }

        public AnswerEntity(Guid id, Guid questionId, string text, double points = 0)
        {
            Id = id;
            Text = text;
            QuestionId = questionId;
            Points = points;
        }
    }
}
