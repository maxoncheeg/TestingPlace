using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TestingPlace.Model.Testing;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;

namespace TestingPlace.Data.Tests.Json
{
    class JsonTestRepository : ITestRepository
    {
        private string _savePath;

        public List<Test> Tests { get; set; } = new();

        public JsonTestRepository() 
        {
            _savePath = ConfigurationManager.AppSettings["jsonTestPath"] ?? string.Empty;
            if (string.IsNullOrEmpty(_savePath)) 
                throw new System.Exception("Не задан путь сохранения тестов");
        }

        public bool Load()
        {
            using var stream = File.Open(_savePath, FileMode.Open);
            using var reader = new StreamReader(stream);

            string jsonText = reader.ReadToEnd();

            List<JsonTest>? jsonTests = JsonSerializer.Deserialize<List<JsonTest>>(jsonText);

            if (jsonTests is null) return false;

            Tests = new();

            foreach (JsonTest jsonTest in jsonTests)
            {
                List<ITestQuestion> questions = new();
                foreach (JsonQuestion question in jsonTest.Questions)
                {
                    List<QuestionAnswer> pointsAnswers = new(), answers = new();

                    foreach (var item in question.Answers)
                    {
                        QuestionAnswer answer = QuestionAnswer.Create(item.Id, item.QuestionId, item.Text, item.Points);
                        answer.ImagePath = string.IsNullOrEmpty(item.ImagePath) ? null : item.ImagePath;

                        if (answer.Points > 0) pointsAnswers.Add(answer);
                        else answers.Add(answer);
                    }

                    ITestQuestion? testQuestion = null;

                    switch (question.Question.Type)
                    {
                        case nameof(DefaultQuestion):
                            if (pointsAnswers.Count > 1 || pointsAnswers.Count <= 0) break;

                            testQuestion = DefaultQuestion.Create(question.Question.Id, question.Question.TestId, 
                                question.Question.Text, pointsAnswers.First(), answers);
                            break;
                        //case nameof(MultipleQuestion):
                        default:
                            testQuestion = null;
                            break;
                    }

                    if(testQuestion is not null)
                        questions.Add(testQuestion);

                }

                if (questions.Count > 0)
                    Tests.Add(Test.Create(jsonTest.Test.Id, jsonTest.Test.Name, questions));
            }

            return true;
        }

        public async Task<bool> LoadAsync()
        {
            return await Task.Run(Load);
        }

        public bool Save()
        {
            if (Tests.Count <= 0) return false;

            List<JsonTest> jsonTests = new();

            foreach (Test test in Tests)
            {
                List<JsonQuestion> jsonQuestions = new();

                foreach (ITestQuestion testQuestion in test)
                    if (testQuestion is Question question)
                        jsonQuestions.Add(new(question, question.Answers.Select(x => x as AnswerEntity).ToList()));

                jsonTests.Add(new(test, jsonQuestions));
            }

            using var stream = File.Open(_savePath, FileMode.Create);
            using var writer = new StreamWriter(stream);

            string textedTests = JsonSerializer.Serialize(jsonTests, new JsonSerializerOptions() { WriteIndented = true });
            writer.Write(textedTests);

            return true;
        }

        public async Task<bool> SaveAsync()
        {
            return await Task.Run(Save);
        }
    }
}
