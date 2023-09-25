using System;

namespace TestingPlace.Model.Testing.Questions.QuestionsTypes.PictureQuestions
{
    [Serializable]
    internal class DefaultPictureQuestion(DefaultQuestion question, string picturePath) : PictureQuestion(question, picturePath)
    {}
}
