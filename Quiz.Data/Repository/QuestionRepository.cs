using Quiz.Data.QA;
using Quiz.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Data.Repository
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        private readonly QuizContext _db;

        public QuestionRepository(QuizContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Question source)
        {
            var dbObj = _db.Questions.FirstOrDefault(s => s.Id == source.Id);
            if (dbObj is null) _db.Questions.Add(source);
            else UpdateDbObject(dbObj, source);
        }

        public async Task UpdateAsync(Question source)
        {
            var dbObj = _db.Questions.FirstOrDefault(s => s.Id == source.Id);
            if (dbObj is null) await _db.Questions.AddAsync(source);
            else UpdateDbObject(dbObj, source);
        }

        private void UpdateDbObject(Question dbObj, Question source)
        {
            dbObj.Id = source.Id;
            dbObj.Question1 = source.Question1;
            dbObj.Answer = source.Answer;
            dbObj.QuizId = source.QuizId;

        }
    }
}
