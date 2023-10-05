using System;
using System.Collections.Generic;

namespace TestingPlace.Model.Testing.Answers
{
	static class AnswerListExtensions
	{
		public static void RandomShuffle(this List<QuestionAnswer> answers, Random random, int steps)
		{
			for (int i = 0; i < steps; i++)
				for (int j = 0; j < answers.Count; j++)
				{
					int index = random.Next(answers.Count);

					if (index != j)
						(answers[j], answers[index]) = (answers[index], answers[j]);
				}
		}
	}
}
