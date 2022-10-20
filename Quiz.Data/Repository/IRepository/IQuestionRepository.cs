using Quiz.Data.QA;
using System;
using System.Threading.Tasks;

namespace Quiz.Data.Repository.IRepository
{
    public interface IQuestionRepository : IRepository<Question>
    {
        void Update(Question source);
        Task UpdateAsync(Question source);
    }
}
