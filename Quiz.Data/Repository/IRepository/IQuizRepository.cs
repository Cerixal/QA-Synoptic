using Quiz.Data.QA;
using System;
using System.Threading.Tasks;

namespace Quiz.Data.Repository.IRepository
{
    public interface IQuizRepository : IRepository<QA.Quiz>
    {
        void Update(QA.Quiz source);
        Task UpdateAsync(QA.Quiz source);
    }
}
