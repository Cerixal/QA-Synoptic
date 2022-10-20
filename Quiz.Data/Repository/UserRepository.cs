using Quiz.Data.QA;
using Quiz.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly QuizContext _db;

        public UserRepository(QuizContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User source)
        {
            var dbObj = _db.Users.FirstOrDefault(s => s.Id == source.Id);
            if (dbObj is null) _db.Users.Add(source);
            else UpdateDbObject(dbObj, source);
        }

        public async Task UpdateAsync(User source)
        {
            var dbObj = _db.Users.FirstOrDefault(s => s.Id == source.Id);
            if (dbObj is null) await _db.Users.AddAsync(source);
            else UpdateDbObject(dbObj, source);
        }

        private void UpdateDbObject(User dbObj, User source)
        {
            dbObj.Id = source.Id;
            dbObj.Username = source.Username;
            dbObj.Password = source.Password;
            dbObj.Role = source.Role;
        }
    }
}
