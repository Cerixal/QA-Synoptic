using Quiz.Data.QA;
using Quiz.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Data.Repository
{
    public class QuizRepository : Repository<QA.Quiz>, IQuizRepository
    {
        private readonly QuizContext _db;

        public QuizRepository(QuizContext db) : base(db)
        {
            _db = db;
        }

        public void Update(QA.Quiz source)
        {
            var dbObj = _db.Quizzes.FirstOrDefault(s => s.Id == source.Id);
            if (dbObj is null) _db.Quizzes.Add(source);
            else UpdateDbObject(dbObj, source);
        }

        public async Task UpdateAsync(QA.Quiz source)
        {
            var dbObj = _db.Quizzes.FirstOrDefault(s => s.Id == source.Id);
            if (dbObj is null) await _db.Quizzes.AddAsync(source);
            else UpdateDbObject(dbObj, source);
        }

        private void UpdateDbObject(QA.Quiz dbObj, QA.Quiz source)
        {
            dbObj.Id = source.Id;
            dbObj.Title = source.Title;
        }
    }
}
