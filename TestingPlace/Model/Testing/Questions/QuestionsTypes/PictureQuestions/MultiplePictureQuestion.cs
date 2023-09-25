using System;
using System.Collections.Generic;
using TestingPlace.Model.Testing.Answers;

namespace TestingPlace.Model.Testing.Questions.QuestionsTypes.PictureQuestions
{
    [Serializable]
    internal class MultiplePictureQuestion(IMultipleTestQuestion question, string picturePath) : PictureQuestion(question, picturePath), IMultipleTestQuestion
    {
        public float Answer(List<IAnswer> answers) => (Question as IMultipleTestQuestion).Answer(answers);
    }
}
