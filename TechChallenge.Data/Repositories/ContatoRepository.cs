using TechChallenge.Data.Context;
using TechChallenge.Domain.Entities.Models;
using TechChallenge.Domain.Interfaces.Repositories;

namespace TechChallenge.Data.Repositories
{
    public class ContatoRepository : BaseRepository<Contato>, IContatoRepository
    {
        private readonly DataContext _context;

        public ContatoRepository(DataContext context) : base(context) 
        {
            _context = context;
        }
    }
}
