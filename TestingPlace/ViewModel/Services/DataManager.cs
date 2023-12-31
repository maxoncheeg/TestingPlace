﻿using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Threading.Tasks;
using TestingPlace.Data.Tests;
using TestingPlace.Data.Users;
using TestingPlace.Model.Users;

namespace TestingPlace.ViewModel.Services
{
    public class DataManager : IDataManager
    {
        private UserEntity? _currentUser = null;

        private readonly TestRepository _testRepository;
        private readonly UserRepository _userRepository;

        public TestRepository TestRepository => _testRepository;
        public UserRepository UserRepository => _userRepository;

        public UserEntity? CurrentUser => _currentUser;

        private DataManager(TestRepository _testRepository, UserRepository _userRepository)
        {
            this._testRepository = _testRepository;
            this._userRepository = _userRepository;
        }

        public static DataManager Instance(TestRepository testRepository, UserRepository userRepository)
            => new(testRepository, userRepository);

        public async Task SaveAllAsync()
        {
            await _testRepository.SaveAsync();
            await _userRepository.SaveAsync();
        }

        public async Task LoadAllAsync()
        {
            await _testRepository.LoadAsync();
            await _userRepository.LoadAsync();
        }

        public bool TryAuthorizeUser(string login, string password)
        {
            password = Convert.ToBase64String(MD5.HashData(Encoding.UTF8.GetBytes(password)));

            var user = (from x in UserRepository.GetAll()
                        where x.Login == login && x.Password == password
                        select x).FirstOrDefault();

            if (user != null)
            {
                _currentUser = user;
                return true;
            }

            return false;
        }

        public bool TryRegisterUser(string login, string password, string name, string email, bool isTeacher)
        {
            password = Convert.ToBase64String(MD5.HashData(Encoding.UTF8.GetBytes(password)));
            UserEntity user = new UserEntity(Guid.NewGuid(), login, password, name, email) { IsTeacher = isTeacher };

            return UserRepository.Add(user);
        }
    }
}
