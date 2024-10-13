using TechChallenge.Domain.Entities.Models;
using TechChallenge.Domain.Exceptions;
using TechChallenge.Domain.Interfaces.Repositories;
using TechChallenge.Domain.Interfaces.Services;

namespace TechChallenge.Manager.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IDDDService _idDDService;

        public ContatoService(IContatoRepository contatoRepository, IDDDService idDDService)
        {
            _contatoRepository = contatoRepository;
            _idDDService = idDDService;
        }

        public async Task<Contato> Create(Contato contato, byte nrDDD)
        {
            var ddds = _idDDService.Get().Result.ToList();
            var dddDoContato = ddds.FirstOrDefault(x => x.NrDDD == nrDDD) ?? throw new DomainException("Número de DDD inexistente");
            contato.SetDDDId(dddDoContato.Id);
            return await _contatoRepository.Create(contato);
        }

        public async Task<Contato> Get(long id)
        {
            var contato = await _contatoRepository.Get(id);
            var ddd = await _idDDService.Get(contato.DDDId);
            contato.SetDDD(DDD.SetDDD(ddd.Id, ddd.NrDDD));

            return contato;
        }

        public async Task<List<Contato>> Get()
        {
            var contatos = await _contatoRepository.Get();
            var ddds = await _idDDService.Get();

            foreach ( var contato in contatos )
            {
                contato.SetDDD(ddds.Find(x => x.Id == contato.DDDId) ?? new DDD());
            }
            return contatos;
        }

        public async Task Remove(long id)
        {
            await _contatoRepository.Remove(id);
        }

        public async Task<Contato> Update(Contato contato, byte nrDDD)
        {
            var ddds = _idDDService.Get().Result.ToList();
            var dddDoContato = ddds.FirstOrDefault(x => x.NrDDD == nrDDD) ?? throw new DomainException("Número de DDD inexistente");
            contato.SetDDDId(dddDoContato.Id);
            await _contatoRepository.Update(contato);
            return contato;
        }
    }
}
