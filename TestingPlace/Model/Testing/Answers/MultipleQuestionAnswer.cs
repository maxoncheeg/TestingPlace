using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingPlace.Model.Testing.Answers
{
    //public class MultipleQuestionAnswer : IMultipleQuestionAnswer
    //{
    //    private List<DefaultQuestionAnswer> _answers;

    //    public float Points => (from answer in _answers
    //                           select answer.Points).Sum();

    //    public float ErrorPointsCost { get; set; } = 0;

    //    public MultipleQuestionAnswer(List<DefaultQuestionAnswer> answers)
    //    {
    //        _answers = answers;
    //    }

    //    public float Check(IAnswer answer)
    //    {
    //        if (_answers.FirstOrDefault(x => x.Equals(answer)) is DefaultQuestionAnswer rightAnswer)
    //            return rightAnswer.Points;

    //        return 0;
    //    }

    //    public float Check(List<IAnswer> answers)
    //    {
    //        float points = 0;

    //        foreach (IAnswer answer in answers)
    //            if (_answers.FirstOrDefault(x => x.Equals(answer)) is DefaultQuestionAnswer rightAnswer)
    //                points += rightAnswer.Points;
    //            else 
    //                points = points - ErrorPointsCost < 0 ? 0 : points - ErrorPointsCost;

    //        return points;
    //    }
    //}
}
