using System;

namespace TestingPlace.Model.Testing.Answers
{
    [Serializable]
    internal class PictureAnswer : Answer
    {
        public string PicturePath { get; private set; }

        public PictureAnswer(string text, string picturePath) : base(text) =>     
            PicturePath = picturePath;      
    }
}
