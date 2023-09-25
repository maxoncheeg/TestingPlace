using System;

namespace TestingPlace.Model.Testing.Answers
{
	[Serializable]
	public class Answer : IAnswer
	{
		public string Text { get; private set; }

		public Answer(string text)
		{
			if (string.IsNullOrEmpty(text))
				throw new ArgumentNullException("Текст класса Answer не может быть пустым и равняться нулю");

			Text = text;
		}

        public bool Equals(IAnswer other) =>
			other is DefaultQuestionAnswer questionAnswer && questionAnswer.Equals(this)
				|| other is Answer answer && answer.Text == Text;
    }
}
