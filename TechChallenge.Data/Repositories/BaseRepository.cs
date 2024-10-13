using TechChallenge.Data.Context;
using TechChallenge.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using TechChallenge.Domain.Entities;

namespace TechChallenge.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public virtual async Task<T> Create(T obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task<T> Get(long id)
        {
            var obj = await _context.Set<T>()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ToListAsync();
            return obj.FirstOrDefault();
        }

        public virtual async Task<List<T>> Get()
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task Remove(long id)
        {
            var obj = await _context.Set<T>()
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);

            if (obj != null)
            {
                var trackedEntity = _context.Set<T>().Local.FirstOrDefault(e => e.Id == id);
                if (trackedEntity != null)
                {
                    _context.Entry(trackedEntity).State = EntityState.Deleted;
                }
                else
                {
                    _context.Remove(obj);
                }

                await _context.SaveChangesAsync();

            }
        }

        public virtual async Task<T> Update(T obj)
        {
            var existingEntity = await _context.Set<T>().FindAsync(obj.Id); 

            if (existingEntity == null)
            {
                throw new InvalidOperationException("A entidade a ser atualizada não foi encontrada.");
            }

            _context.Entry(existingEntity).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

    }
}
