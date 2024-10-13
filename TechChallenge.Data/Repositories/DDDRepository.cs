using TechChallenge.Data.Context;
using TechChallenge.Domain.Entities.Models;
using TechChallenge.Domain.Interfaces.Repositories;

namespace TechChallenge.Data.Repositories
{
    public class DDDRepository : BaseRepository<DDD>, IDDDRepository
    {
        private readonly DataContext _context;

        public DDDRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
