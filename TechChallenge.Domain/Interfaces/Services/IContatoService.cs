using TechChallenge.Domain.Entities.Models;

namespace TechChallenge.Domain.Interfaces.Services
{
    public interface IContatoService
    {
        Task<Contato> Create(Contato contato, byte ddd);
        Task<Contato> Update(Contato contato, byte ddd);
        Task Remove(long id);
        Task<Contato> Get(long id);
        Task<List<Contato>> Get();
    }
}
