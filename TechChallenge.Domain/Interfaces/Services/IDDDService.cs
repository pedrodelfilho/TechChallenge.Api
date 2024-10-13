using TechChallenge.Domain.Entities.Models;

namespace TechChallenge.Domain.Interfaces.Services
{
    public interface IDDDService
    {
        Task<DDD> Create(DDD agendamento);
        Task<DDD> Update(DDD agendamento);
        Task Remove(long id);
        Task<DDD> Get(long id);
        Task<List<DDD>> Get();
    }
}
