using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Data.Repository.IRepository
{
    public interface IQAUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IQuizRepository Quiz { get; }
        IQuestionRepository Question { get; }
        IAnswerRepository Answer { get; }
        void Save();
        Task SaveAsync();
    }
}
