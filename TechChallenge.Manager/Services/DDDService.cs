using TechChallenge.Domain.Entities.Models;
using TechChallenge.Domain.Interfaces.Repositories;
using TechChallenge.Domain.Interfaces.Services;

namespace TechChallenge.Manager.Services
{
    public class DDDService : IDDDService
    {
        private readonly IDDDRepository _dddrepository;
        private readonly IContatoRepository _contatoRepository;

        public DDDService(IDDDRepository dDDrepository, IContatoRepository contatoRepository)
        {
            _dddrepository = dDDrepository;
            _contatoRepository = contatoRepository;
        }

        public async Task<DDD> Create(DDD ddd)
        {
            return await _dddrepository.Create(ddd);
        }

        public async Task<DDD> Get(long id)
        {
            return await _dddrepository.Get(id);
        }

        public async Task<List<DDD>> Get()
        {
            var ddds = await _dddrepository.Get();
            var contatos = _contatoRepository.Get().Result.ToList();

            foreach (var ddd in ddds)
            {
                var contatosDoDDD = contatos.Where(x => x.DDDId == ddd.Id);
                foreach(var contato in contatosDoDDD)
                {
                    ddd.Contatos.Add(contato);
                }
            }

            return ddds;
        }

        public async Task Remove(long id)
        {
            await _dddrepository.Remove(id);
        }

        public async Task<DDD> Update(DDD ddd)
        {
            await _dddrepository.Update(ddd);
            return ddd;
        }
    }
}
