using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions
{
    //public class MultipleQuestion : Question, ITestQuestion
    //{
    //    private MultipleQuestion(Guid id, Guid testId, MultipleQuestionAnswer answer, List<QuestionAnswer> incorrectAnswers, string text) : base(answer, text, nameof(MultipleQuestion)
    //    {
    //        _answers = new(incorrectAnswers);
    //        _answers.AddRange((_answer as MultipleQuestionAnswer).Answers);

    //        ShuffleAnswers(new());
    //    }

    //    public static MultipleQuestion Create(Guid testId, string text, MultipleQuestionAnswer answer, List<QuestionAnswer> incorrectAnswers)
    //        => new(Guid.NewGuid(), testId, answer, incorrectAnswers, text);

    //    public static MultipleQuestion Create(Guid id, Guid testId, string text, MultipleQuestionAnswer answer, List<QuestionAnswer> incorrectAnswers)
    //        => new(id, testId, answer, incorrectAnswers, text);

    //    public double Answer(QuestionAnswer[] answers) => (_answer as MultipleQuestionAnswer).Check(answers);

    //    public double GetPoints() => (_answer as MultipleQuestionAnswer).Points;
    //}
}
