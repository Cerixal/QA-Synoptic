using Quiz.Data.Repository.IRepository;
using Quiz.Data.QA;
using System.Threading.Tasks;
using Quiz.Data.Repository;

namespace Portland.Data.Repository
{
    public class QAUnitOfWork : IQAUnitOfWork
    {
        private readonly QuizContext _db;

        private IUserRepository _User;
        private IQuestionRepository _Question;
        private IQuizRepository _Quiz;
        private IAnswerRepository _Answer;
        public IUserRepository User => _User ??= new UserRepository(_db);
        public IQuestionRepository Question => _Question ??= new QuestionRepository(_db);
        public IQuizRepository Quiz => _Quiz ??= new QuizRepository(_db);
        public IAnswerRepository Answer => _Answer ??= new AnswerRepository(_db);


        public QAUnitOfWork(QuizContext db)
        {
            _db = db;
        }
        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task DisposeAsync()
        {
            await _db.DisposeAsync();
        }


        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
