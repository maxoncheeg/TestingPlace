using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestingPlace.Model.Users;

namespace TestingPlace.Data.Users
{
    public abstract class UserRepository : IBaseRepository<UserEntity>
    {
        protected List<UserEntity> _users = new();

        public bool Add(UserEntity entity)
        {
            if (entity == null
                || _users.Contains(entity)
                || _users.Find(e => e.Id == entity.Id) != null)
                return false;

            _users.Add(entity);
            return true;

        }

        public bool Delete(UserEntity entity)
        {
            if (_users.Contains(entity))
            {
                _users.Remove(entity);
                return true;
            }

            return false;
        }

        public UserEntity? Get(Guid id)
        {
            return _users.FirstOrDefault(user => user.Id == id);
        }

        public IReadOnlyCollection<UserEntity> GetAll()
        {
            return _users;
        }

        public IReadOnlyCollection<UserEntity> GetBy(Predicate<UserEntity> predicate)
        {
            List<UserEntity> users = new();

            foreach (var item in _users)
                if (predicate(item)) users.Add(item);

            return users;
        }

        public bool Update(UserEntity entity)
        {
            for (int i = 0; i < _users.Count; i++)
                if (_users[i] == entity)
                {
                    _users[i] = entity;
                    return true;
                }

            return false;
        }

        public abstract Task<bool> LoadAsync();

        public abstract Task<bool> SaveAsync();
    }
}
