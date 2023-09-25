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
            ITestRepository rep = new SerializableTestRepository();
            if (await rep.LoadAsync())
            {
                Test test2 = rep.Tests.First();
                string text = "";
                for (int i = 0; i < test2.QuestionCount; i++)
                {
                    if (test2[i] is DefaultQuestion q) text += q.Text;
                    else if (test2[i] is TimeQuestion t) text += (t.Question as DefaultQuestion).Text + " | time: " + t.InitialMilliseconds;
                    else if (test2[i] is PictureQuestion p) text += (p.Question as DefaultQuestion).Text + " | path: " + p.PicturePath;


                    text += Environment.NewLine;
                }

                TEXT.Text = text;

                return;
            }

            var x = new DefaultQuestion(new Model.Testing.Answers.DefaultQuestionAnswer(new("полдень"), 10), new(), "Время?");
            var y = new DefaultQuestion(new Model.Testing.Answers.DefaultQuestionAnswer(new("полдень"), 10), new(), "Картинка?");
            DefaultTimeQuestion e = new(x, 1000, 10);
            DefaultPictureQuestion h = new(y, @"c:\hui\znaet\gde.png");
            List<ITestQuestion> list = new List<ITestQuestion>()
            {
                new DefaultQuestion(new Model.Testing.Answers.DefaultQuestionAnswer(new("РЫБА1"), 10), new(), "Кто хороший рыбка?"),
                new DefaultQuestion(new Model.Testing.Answers.DefaultQuestionAnswer(new("РЫБА2"), 10), new(), "Кто хороший рыбка2?"),
                new DefaultQuestion(new Model.Testing.Answers.DefaultQuestionAnswer(new("РЫБА3"), 10), new(), "Кто хороший рыбка1?"),
                new DefaultQuestion(new Model.Testing.Answers.DefaultQuestionAnswer(new("РЫБА4"), 10), new(), "Кто хороший рыбка3?"),
                new DefaultQuestion(new Model.Testing.Answers.DefaultQuestionAnswer(new("РЫБА5"), 10), new(), "Кто хороший рыбка4?"),
                e, h
            };

            Test test = new Test(list);

            list.RemoveAt(0);
            Test test1 = new Test(list);

            ITestRepository repository = new SerializableTestRepository();
            repository.Tests = new() { test, test1 };
            await repository.SaveAsync();
        }
    }
}
