using System.Collections.Generic;
using System.Linq;

namespace TestingPlace.Model.Testing.Answers
{
    //public class MultipleQuestionAnswer : IQuestionAnswer
    //{
    //    private List<QuestionAnswer> _answers;

    //    public double Points => (from answer in _answers
    //                            select answer.Points).Sum();

    //    public float ErrorPointsCost { get; set; } = 0;

    //    public IReadOnlyList<QuestionAnswer> Answers => _answers;

    //    public MultipleQuestionAnswer(List<QuestionAnswer> answers)
    //    {
    //        _answers = new(answers);
    //    }

    //    public double Check(IQuestionAnswer answer)
    //    {
    //        if (_answers.FirstOrDefault(x => x.Equals(answer)) is QuestionAnswer rightAnswer)
    //            return rightAnswer.Points;

    //        return 0;
    //    }

    //    public double Check(IQuestionAnswer[] answers)
    //    {
    //        double points = 0;

    //        foreach (IQuestionAnswer answer in answers)
    //            if (_answers.FirstOrDefault(x => x.Equals(answer)) is QuestionAnswer rightAnswer)
    //                points += rightAnswer.Points;
    //            else
    //                points = points - ErrorPointsCost < 0 ? 0 : points - ErrorPointsCost;

    //        return points;
    //    }
    //}
}
