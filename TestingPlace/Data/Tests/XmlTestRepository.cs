using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using TestingPlace.Model.Testing;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.Model.Testing.Questions.QuestionsTypes.PictureQuestions;
using TestingPlace.Model.Testing.Questions.QuestionsTypes.TimeQuestions;

namespace TestingPlace.Data.Tests
{
    class XmlTestRepository : ITestRepository
    {
        public List<Test> Tests { get; set; }

        public bool Load()
        {
            using XmlTextReader reader = new("test.xml");

           //XmlDocument document = new XmlDocument();
           // document.

           // while (reader.Read())
           // {
           //     if(reader.NodeType == XmlNodeType.Element)
           //     {
           //         reader.IsEmptyElement
           //     }
           //     MessageBox.Show(reader.Name + " " + reader.Value);
           // }

            return true;
        }

        private void ReadXml(XmlReader reader)
        {
            string name = reader.Name;

             
        }

        public Task<bool> LoadAsync()
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            //using XmlWriter writer = XmlWriter.Create("test.xml", new() { Indent = false,  });
            //writer.WriteStartDocument();

            //writer.WriteStartElement("tests");
            //foreach (Test test in Tests)
            //{
            //    writer.WriteStartElement("test");
            //    writer.WriteElementString("name", test.Name);

            //    writer.WriteStartElement("questions");
            //    for (int i = 0; i < test.QuestionCount; i++)
            //    {
            //        writer.WriteStartElement("question");
            //        Question question = null;

            //        switch (test[i])
            //        {
            //            case TimeQuestion timeQuestion:
            //                writer.WriteAttributeString("type",
            //                    timeQuestion is DefaultTimeQuestion ? nameof(DefaultTimeQuestion) : nameof(MultipleTimeQuestion));

            //                writer.WriteElementString("initialMilliseconds", timeQuestion.InitialMilliseconds.ToString());
            //                writer.WriteElementString("pointsMultiplier", timeQuestion.PointsMultiplier.ToString());

            //                if (timeQuestion.Question is DefaultQuestion tx)
            //                    question = tx;
            //                else if (timeQuestion.Question is MultipleQuestion ty)
            //                    question = ty;


            //                break;

            //            case PictureQuestion pictureQuestion:
            //                writer.WriteAttributeString("type",
            //                    pictureQuestion is DefaultPictureQuestion ? nameof(DefaultPictureQuestion) : nameof(MultiplePictureQuestion));
            //                writer.WriteElementString("picturePath", pictureQuestion.PicturePath);

            //                if (pictureQuestion.Question is DefaultQuestion px)
            //                    question = px;
            //                else if (pictureQuestion.Question is MultipleQuestion py)
            //                    question = py;

            //                break;

            //            case DefaultQuestion defaultQuestion:
            //                writer.WriteAttributeString("type", nameof(DefaultQuestion));

            //                question = defaultQuestion;
            //                break;
            //            case MultipleQuestion multipleQuestion:
            //                writer.WriteAttributeString("type", nameof(MultipleQuestion));

            //                question = multipleQuestion;
            //                break;
            //            default:
            //                break;
            //        }

            //        if (question is null) 
            //            return false;

            //        writer.WriteElementString("text", question.Text);
            //        SaveAnswers(writer, question);

            //        //if ((question as ITestQuestion) is PictureQuestion pictureQuestion)
            //        //    writer.WriteElementString("picturePath", pictureQuestion.PicturePath);
            //        //if ((question as ITestQuestion) is TimeQuestion timeQuestion)
            //        //{
            //        //    writer.WriteElementString("initialMilliseconds", timeQuestion.InitialMilliseconds.ToString());
            //        //    writer.WriteElementString("pointsMultiplier", timeQuestion.PointsMultiplier.ToString());
            //        //}

            //        writer.WriteEndElement();
            //    }
            //    writer.WriteEndElement();

            //    writer.WriteEndElement();
            //}
            //writer.WriteEndElement();

            //writer.WriteEndDocument();

            XmlSerializer serializer = new XmlSerializer(new {Tests});
            using var stream = File.Open("text.xml", FileMode.Create);
            serializer.Serialize(stream, Tests);

            return true;
        }

        public async Task<bool> SaveAsync()
        {
            bool result = false;
            await Task.Run(() => { result = Save(); });
            return result;
        }

        private bool SaveAnswers(XmlWriter writer, Question question)
        {
            if (question is not ITestQuestion testQuestion) return false;

            writer.WriteStartElement("answers");

            foreach (var item in question.Answers)
            {
                writer.WriteStartElement("answer");

                switch (item)
                {
                    case DefaultQuestionAnswer answer:
                        CreateAnswerContent(writer, answer.Answer);

                        writer.WriteElementString("points", answer.Points.ToString());
                        writer.WriteElementString("isRightAnswer", testQuestion.Answer(answer) > 0 ? true.ToString() : false.ToString());
                        break;
                    case Answer answer:
                        CreateAnswerContent(writer, answer);
                        break;
                }

                writer.WriteEndElement();
            }

            writer.WriteEndElement();

            return true;
        }

        private bool CreateAnswerContent(XmlWriter writer, Answer answer)
        {
            writer.WriteAttributeString("type", nameof(Answer));
            writer.WriteElementString("text", answer.Text);

            if (answer is PictureAnswer pictureAnswer)
                writer.WriteElementString("picturePath", pictureAnswer.PicturePath);

            return true;
        }
    }
}
