using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestingPlace.Data.Tests;
using TestingPlace.Model.Testing;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.Model.Testing.Questions.QuestionsTypes.PictureQuestions;
using TestingPlace.Model.Testing.Questions.QuestionsTypes.TimeQuestions;

namespace TestingPlace.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Microsoft1();
        }

        public async void Microsoft1()
        {
            //ITestRepository rep = new SerializableTestRepository();
            //if (await rep.LoadAsync())
            //{
            //    Test test2 = rep.Tests.First();
            //    string text = "";
            //    for (int i = 0; i < test2.QuestionCount; i++)
            //    {
            //        if (test2[i] is DefaultQuestion q) text += q.Text;
            //        else if (test2[i] is TimeQuestion t) text += (t.Question as DefaultQuestion).Text + " | time: " + t.InitialMilliseconds;
            //        else if (test2[i] is PictureQuestion p) text += (p.Question as DefaultQuestion).Text + " | path: " + p.PicturePath;


            //        text += Environment.NewLine;
            //    }

            //    TEXT.Text = text;

            //    return;
            //}

            Guid testGuid = Guid.NewGuid();
            Guid questionGuid = Guid.NewGuid();
            List<ITestQuestion> q = new();

            DefaultQuestionAnswer an = DefaultQuestionAnswer.Create(questionGuid, "ОТВЕТ 2", 10);
            List<DefaultQuestionAnswer> answer = new() 
            { 
                DefaultQuestionAnswer.Create(questionGuid, "ОТВЕТ 1", 0),
                DefaultQuestionAnswer.Create(questionGuid, "ОТВЕТ 3", 0),
            };

            DefaultQuestion question = DefaultQuestion.Create(questionGuid, testGuid, "WHO AM I???", an, answer);

                q.Add(question);
            Test test = Test.Create(testGuid, "первый", q);



            ITestRepository repository = new JsonTestRepository();
            repository.Tests = new() { test };
            repository.Save();

            //repository.Load();
        }
    }
}
