using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
    class JsonTestRepository : ITestRepository
    {
        public List<Test> Tests { get; set; }

        public bool Load()
        {

            using var stream = File.Open("test.txt", FileMode.Open);
            using var reader = new StreamReader(stream);

            string x = reader.ReadToEnd();

            var y =JsonSerializer.Deserialize<List<TestEntity>>(x);
            Tests = new();
            foreach (var item in y)
            {
                Tests.Add(Test.Create(item.Id, item.Name, new()));
            }

            MessageBox.Show(Tests[0].Name);

            return true;
        }

        public Task<bool> LoadAsync()
        {
            throw new NotImplementedException();
        }
        
        public bool Save()
        {

            JsonTest test = new()
            {
                Test = Tests[0]
            };

            test.Questions.Add(Tests[0][0] as QuestionEntity);
            var xx = (Tests[0][0] as DefaultQuestion).Answers;
            test.Answers.AddRange(xx.Select(y => y as AnswerEntity));

            using var stream = File.Open("test.txt", FileMode.Create);
            using var writer = new StreamWriter(stream);

            string x = JsonSerializer.Serialize(test);
            writer.Write(x);

            return true;
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        //public bool Load()
        //{
        //    using XmlTextReader reader = new("test.xml");
        //    XDocument document = XDocument.Load(reader);

        //    var testsElement = document.Element("tests");
        //    if (testsElement is null) return false;

        //    var tests = from x in testsElement.Elements()
        //                where x.Name == "test"
        //                select x;

        //    List<Test> loadedTests = new();
        //    foreach (var item in tests)
        //    {
        //        Test test;
        //        List<ITestQuestion> testQuestions = new List<ITestQuestion>();

        //        if (item.Element("name") is XElement nameElement && nameElement is not null)
        //        {
        //            string testName = nameElement.Value;

        //            var questionsElement = item.Element("questions");
        //            if (questionsElement is null) continue;

        //            var questions = from x in questionsElement.Elements()
        //                            where x.Name == "question"
        //                            select x;

        //            foreach (var question in questions)
        //            {
        //                var typeAttribute = question.Attribute("type");
        //                var textElement = question.Element("text");
        //                var answersElement = question.Element("answers");

        //                if (typeAttribute is null
        //                    || textElement is null
        //                    || answersElement is null) continue;

        //                string type = typeAttribute.Value;
        //                string text = textElement.Value;

        //                Dictionary<IAnswer, bool> boolAnswers = new();

        //                var answers = from x in answersElement.Elements()
        //                              where x.Name == "answer"
        //                              select x;

        //                foreach (var answer in answers)
        //                {
        //                    var answerTypeAttribute = answer.Attribute("type");
        //                    var answerTextElement = answer.Element("text");

        //                    if (answerTypeAttribute is null
        //                        || answerTextElement is null) continue;

        //                    Answer a;
        //                    if (answer.Element("picturePath") is XElement path && path != null)
        //                        a = new PictureAnswer(answerTextElement.Value, path.Value);
        //                    else
        //                        a = new Answer(answerTextElement.Value);

        //                    switch (answerTypeAttribute.Value)
        //                    {
        //                        case nameof(DefaultQuestionAnswer):
        //                            var answerPointsElement = answer.Element("points");
        //                            var answerRightElement = answer.Element("isRightAnswer");

        //                            if (answerRightElement is null
        //                                || answerPointsElement is null) continue;

        //                            if (int.TryParse(answerPointsElement.Value, out int result))
        //                                boolAnswers.Add(new DefaultQuestionAnswer(a, result), answerRightElement.Value == true.ToString());
        //                            else continue;
        //                            break;
        //                        case nameof(Answer):
        //                            boolAnswers.Add(a, false);
        //                            break;
        //                    }
        //                }

        //                bool isMultipleAnswers = (from k in boolAnswers.Values
        //                                          where k
        //                                          select k).Count() > 1;

        //                switch (type)
        //                {
        //                    case nameof(DefaultPictureQuestion):
        //                        //nameof(DefaultQuestion):
        //                        break;
        //                    case nameof(MultiplePictureQuestion): break;

        //                    case nameof(DefaultTimeQuestion):
        //                        //nameof(DefaultQuestion):
        //                        break;

        //                    case nameof(MultipleTimeQuestion): break;

        //                    case nameof(DefaultQuestion): break;
        //                    case nameof(MultipleQuestion): break;
        //                    case nameof(OrderQuestion): break;
        //                }

        //            }
        //        }
        //        else continue;
        //    }

        //    return true;
        //}

        //public async Task<bool> LoadAsync()
        //{
        //    bool result = false;
        //    await Task.Run(() => { result = Load(); });
        //    return result;
        //}

        //public bool Save()
        //{
        //    using XmlWriter writer = XmlWriter.Create("test.xml", new() { Indent = true, });
        //    writer.WriteStartDocument();

        //    writer.WriteStartElement("tests");
        //    foreach (Test test in Tests)
        //    {
        //        writer.WriteStartElement("test");
        //        writer.WriteElementString("name", test.Name);

        //        writer.WriteStartElement("questions");
        //        for (int i = 0; i < test.QuestionCount; i++)
        //        {
        //            writer.WriteStartElement("question");
        //            Question? question = null;

        //            switch (test[i])
        //            {
        //                case TimeQuestion timeQuestion:
        //                    writer.WriteAttributeString("type",
        //                        timeQuestion is DefaultTimeQuestion ? nameof(DefaultTimeQuestion) : nameof(MultipleTimeQuestion));

        //                    writer.WriteElementString("initialMilliseconds", timeQuestion.InitialMilliseconds.ToString());
        //                    writer.WriteElementString("pointsMultiplier", timeQuestion.PointsMultiplier.ToString());

        //                    if (timeQuestion.Question is DefaultQuestion tx)
        //                        question = tx;
        //                    else if (timeQuestion.Question is MultipleQuestion ty)
        //                        question = ty;
        //                    break;

        //                case PictureQuestion pictureQuestion:
        //                    writer.WriteAttributeString("type",
        //                        pictureQuestion is DefaultPictureQuestion ? nameof(DefaultPictureQuestion) : nameof(MultiplePictureQuestion));
        //                    writer.WriteElementString("picturePath", pictureQuestion.PicturePath);

        //                    if (pictureQuestion.Question is DefaultQuestion px)
        //                        question = px;
        //                    else if (pictureQuestion.Question is MultipleQuestion py)
        //                        question = py;
        //                    break;

        //                case DefaultQuestion defaultQuestion:
        //                    writer.WriteAttributeString("type", nameof(DefaultQuestion));

        //                    question = defaultQuestion;
        //                    break;
        //                case MultipleQuestion multipleQuestion:
        //                    writer.WriteAttributeString("type", nameof(MultipleQuestion));

        //                    question = multipleQuestion;
        //                    break;
        //                default:
        //                    break;
        //            }

        //            if (question is null)
        //                return false;

        //            writer.WriteElementString("text", question.Text);
        //            SaveAnswers(writer, question);

        //            writer.WriteEndElement();
        //        }
        //        writer.WriteEndElement();

        //        writer.WriteEndElement();
        //    }
        //    writer.WriteEndElement();

        //    writer.WriteEndDocument();

        //    return true;
        //}

        //public async Task<bool> SaveAsync()
        //{
        //    bool result = false;
        //    await Task.Run(() => { result = Save(); });
        //    return result;
        //}

        //private bool SaveAnswers(XmlWriter writer, Question question)
        //{
        //    if (question is not ITestQuestion testQuestion) return false;

        //    writer.WriteStartElement("answers");

        //    foreach (var item in question.Answers)
        //    {
        //        writer.WriteStartElement("answer");

        //        switch (item)
        //        {
        //            case DefaultQuestionAnswer answer:
        //                writer.WriteAttributeString("type", nameof(DefaultQuestionAnswer));

        //                CreateAnswerContent(writer, answer.Answer);

        //                writer.WriteElementString("points", answer.Points.ToString());
        //                writer.WriteElementString("isRightAnswer", testQuestion.Answer(answer) > 0 ? true.ToString() : false.ToString());
        //                break;
        //            case Answer answer:
        //                writer.WriteAttributeString("type", nameof(Answer));

        //                CreateAnswerContent(writer, answer);
        //                break;
        //        }

        //        writer.WriteEndElement();
        //    }

        //    writer.WriteEndElement();

        //    return true;
        //}

        //private bool CreateAnswerContent(XmlWriter writer, Answer answer)
        //{
        //    writer.WriteElementString("text", answer.Text);

        //    if (answer is PictureAnswer pictureAnswer)
        //        writer.WriteElementString("picturePath", pictureAnswer.PicturePath);

        //    return true;
        //}
    }
}
