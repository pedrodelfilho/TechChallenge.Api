using TechChallenge.Domain.Entities;

namespace TechChallenge.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task Remove(long id);
        Task<T> Get(long id);
        Task<List<T>> Get();
    }
}
