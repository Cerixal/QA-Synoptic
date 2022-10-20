using Quiz.Data.QA;


namespace Quiz.Data.Repository.IRepository
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        void Update(Answer source);
        Task UpdateAsync(Answer source);
    }
}