using System;

namespace TestingPlace.Model.Testing.Answers
{
	[Serializable]
	public class Answer : IAnswer
	{
		private readonly string _text;
		public string Text { get => _text; }

		public Answer(string text)
		{
			if (string.IsNullOrEmpty(text))
				throw new ArgumentNullException("Текст класса Answer не может быть пустым и равняться нулю");

            _text = text;
		}

        public bool Equals(IAnswer other) =>
			other is DefaultQuestionAnswer questionAnswer && questionAnswer.Equals(this)
				|| other is Answer answer && answer.Text == Text;
    }
}
