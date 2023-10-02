using System;
using System.Threading.Tasks;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions.QuestionsTypes.TimeQuestions
{
    //abstract class TimeQuestion
    //{
    //    private bool _isRunning;

    //    protected long _milliseconds;
    //    protected long _timePointsMilliseconds;
    //    protected float _pointsMultiplier;

    //    public readonly ITestQuestion Question;
    //    public readonly long InitialMilliseconds;
    //    public long TimePointsMilliseconds
    //    {
    //        get => _timePointsMilliseconds;
    //        set
    //        {
    //            if (value > InitialMilliseconds)
    //                _timePointsMilliseconds = InitialMilliseconds;
    //            else if (value < 0)
    //                _timePointsMilliseconds = 0;
    //            else
    //                _timePointsMilliseconds = value;
    //        }
    //    }

    //    public string TextSeconds
    //    {
    //        get
    //        {
    //            string time = "";

    //            double seconds = Math.Round((double)_milliseconds / 1000, 2);

    //            if (seconds < 10) time += "0";

    //            time += $"{Math.Ceiling(seconds)}.";

    //            return time;
    //        }
    //    }

    //    public float PointsMultiplier => _pointsMultiplier;

    //    public long Milliseconds { get => _milliseconds; }

    //    public event Action TimeEnded;

    //    public TimeQuestion(ITestQuestion question, int milliseconds, float pointsMultiplier)
    //    {
    //        Question = question;
    //        InitialMilliseconds = _milliseconds = milliseconds;
    //        _pointsMultiplier = pointsMultiplier;

    //        _timePointsMilliseconds = InitialMilliseconds / 2;
    //    }

    //    public async void Start()
    //    {
    //        if (_isRunning is true) return;

    //        _isRunning = true;

    //        await Task.Run(async () =>
    //        {
    //            while (_isRunning)
    //            {
    //                long start = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
    //                await Task.Delay(1);
    //                long end = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
    //                _milliseconds -= end - start;
    //                if (_milliseconds <= 0)
    //                {
    //                    _isRunning = false;
    //                    TimeEnded?.Invoke();
    //                }
    //            }
    //        });
    //    }

    //    public void Stop()
    //    {
    //        _isRunning = false;
    //    }

    //    //public float Answer(IAnswer answer)
    //    //{
    //    //    float points = Question.Answer(answer),
    //    //        timePoints = _pointsMultiplier * points;

    //    //    if (_timePointsMilliseconds > _milliseconds)
    //    //        timePoints *= _milliseconds / _timePointsMilliseconds;

    //    //    return points + timePoints;
    //    //}

    //    public float GetPoints()
    //    {
    //        float points = Question.GetPoints(),
    //            timePoints = _pointsMultiplier * points;
    //        return points + timePoints;
    //    }
   // }
}
