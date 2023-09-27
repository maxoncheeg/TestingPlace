using System;
using System.Windows.Documents;

namespace TestingPlace.Model.Testing.Answers
{
    [Serializable]
    public class DefaultQuestionAnswer : IQuestionAnswer, IAnswer
    {
        private Answer _answer;
        private float _points;

        public float Points => _points;
        public Answer Answer => _answer;

        public DefaultQuestionAnswer(Answer answer, float points = 1)
        {
            _answer = answer;
            _points = points;
        }

        public float Check(IAnswer answer) =>
            answer.Equals(_answer) ? _points : 0;

        public bool Equals(IAnswer other) =>
            other is DefaultQuestionAnswer questionAnswer && _answer.Equals(questionAnswer)
                || other is Answer answer && answer.Text == _answer.Text;
    }
}
