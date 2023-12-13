﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using TestingPlace.Data.Tests;
using TestingPlace.Data.Tests.Json;
using TestingPlace.Data.Users;
using TestingPlace.Data.Users.Json;
using TestingPlace.Model.Testing;
using TestingPlace.Model.Testing.Answers;
using TestingPlace.Model.Testing.Questions;
using TestingPlace.View;
using TestingPlace.ViewModel.Managers;

namespace TestingPlace
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDataManager _dataManager;
        private TestRepository _testRepository;
        private UserRepository _usersRepository;

        public App() : base()
        {
            string testsSavePath = ConfigurationManager.AppSettings["jsonTestPath"] ?? string.Empty;
            string usersSavePath = ConfigurationManager.AppSettings["jsonUserPath"] ?? string.Empty;

            _testRepository = new JsonTestRepository(testsSavePath);
            _usersRepository = new JsonUserRepository(usersSavePath);
       
            _dataManager = DataManager.Instance(_testRepository, _usersRepository);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await _dataManager.LoadAllAsync();
            ///await CreateBaseInfo(_dataManager);

            StartupWindow window = new(_dataManager);
            window.Show();
        }

        private async Task CreateBaseInfo(IDataManager manager)
        {
            var testId = Guid.NewGuid();
            Guid q1 = Guid.NewGuid(), q2 = Guid.NewGuid(), q3 = Guid.NewGuid();

            List<IQuestionAnswer> answers1 = new()
            {
                new QuestionAnswer(Guid.NewGuid(), q1, "ПИСАТЬ НА C SHARP"),
                new QuestionAnswer(Guid.NewGuid(), q1, "ПИСАТЬ НА Py SON"),
            };

            List<IQuestionAnswer> answers2 = new()
            {
                new QuestionAnswer(Guid.NewGuid(), q2, "1"),
                new QuestionAnswer(Guid.NewGuid(), q2, "2"),
                new QuestionAnswer(Guid.NewGuid(), q2, "3"),
                new QuestionAnswer(Guid.NewGuid(), q2, "4"),
                new QuestionAnswer(Guid.NewGuid(), q2, "5"),
                new QuestionAnswer(Guid.NewGuid(), q2, "6"),
                new QuestionAnswer(Guid.NewGuid(), q2, "8"),
                new QuestionAnswer(Guid.NewGuid(), q2, "9"),
            };

            List<IQuestionAnswer> answers3 = new()
            {
                new QuestionAnswer(Guid.NewGuid(), q3, "Sergio Speedster"),
                new QuestionAnswer(Guid.NewGuid(), q3, "Maxonella Jordan"),
                new QuestionAnswer(Guid.NewGuid(), q3, "Claudio Leviafan"),
            };

            List<ITestQuestion> testQuestions = new()
            {
                new DefaultQuestion(Guid.NewGuid(), testId, "ЧТО ВАЖНЕЕ?", new QuestionAnswer(Guid.NewGuid(), q1, "РЕШАТЬ ИНТЕГРАЛЫ", 10), answers1),
                new DefaultQuestion(Guid.NewGuid(), testId, "1, -37, 3, 5, -33, ___. Какое число пропущено?", new QuestionAnswer(Guid.NewGuid(), q2, "7", 10), answers2),
                new DefaultQuestion(Guid.NewGuid(), testId, "Молодому ученому с фамилией Левиафанистер(потомок рода династии Левиафан) понадобилась помощь с программированием высокоскоростных вычислительных машин на языке программирования PAYSON. К кому лучше обратится молодому ученому, чтобы выполнить работу более качественно?", new QuestionAnswer(Guid.NewGuid(), q3, "Denix Payson", 10), answers3),
            };

            Test test = new Test(testId, "Проверка программиста. Часть 1", TestTheme.Программирование, Guid.NewGuid(), testQuestions);

            manager.TestRepository.Add(test);
            await manager.TestRepository.SaveAsync();
        }
    }
}
