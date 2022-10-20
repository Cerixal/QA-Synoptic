using Quiz.Data.QA;
using Quiz.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Data.Repository
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        private readonly QuizContext _db;

        public AnswerRepository(QuizContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Answer source)
        {
            var dbObj = _db.Answers.FirstOrDefault(s => s.Id == source.Id);
            if (dbObj is null) _db.Answers.Add(source);
            else UpdateDbObject(dbObj, source);
        }

        public async Task UpdateAsync(Answer source)
        {
            var dbObj = _db.Answers.FirstOrDefault(s => s.Id == source.Id);
            if (dbObj is null) await _db.Answers.AddAsync(source);
            else UpdateDbObject(dbObj, source);
        }

        private void UpdateDbObject(Answer dbObj, Answer source)
        {
            dbObj.Id = source.Id;
            dbObj.QuestionId = source.QuestionId;
            dbObj.Answer1 = source.Answer1;
            dbObj.Character = source.Character;
        }
    }
}
