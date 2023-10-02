using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions.QuestionsTypes.TimeQuestions
{

    //internal class MultipleTimeQuestion : TimeQuestion, IMultipleTestQuestion
    //{
    //    public MultipleTimeQuestion(IMultipleTestQuestion question, int milliseconds, float pointsMultiplier)
    //        : base(question, milliseconds, pointsMultiplier)
    //    { }

    //    public float Answer(List<IAnswer> answers)
    //    {
    //        float points = (Question as IMultipleTestQuestion).Answer(answers),
    //            timePoints = _pointsMultiplier * points;

    //        if (_timePointsMilliseconds > _milliseconds)
    //            timePoints *= _milliseconds / _timePointsMilliseconds;

    //        return points + timePoints;
    //    }
    //}
}
