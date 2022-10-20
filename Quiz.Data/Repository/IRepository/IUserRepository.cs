using Quiz.Data.QA;
using System;
using System.Threading.Tasks;

namespace Quiz.Data.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User source);
        Task UpdateAsync(User source);
    }
}
