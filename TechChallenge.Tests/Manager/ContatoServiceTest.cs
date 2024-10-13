using TechChallenge.Domain.Entities.Models;
using TechChallenge.Domain.Exceptions;
using TechChallenge.Domain.Interfaces.Repositories;
using TechChallenge.Domain.Interfaces.Services;
using TechChallenge.Manager.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace TechChallenge.Tests.Manager;
public class ContatoServiceTest
{
    private readonly Mock<IContatoRepository> _contatoRepositoryMock;
    private readonly Mock<IDDDService> _dddServiceMock;
    private readonly ContatoService _contatoService;
    private readonly Mock<ILogger<ContatoServiceTest>> _loggerMock;

    public ContatoServiceTest()
    {
        _contatoRepositoryMock = new Mock<IContatoRepository>();
        _dddServiceMock = new Mock<IDDDService>();
        _contatoService = new ContatoService(_contatoRepositoryMock.Object, _dddServiceMock.Object);
        _loggerMock = new Mock<ILogger<ContatoServiceTest>>();
    }

    [Fact]
    [Trait("Service", "Erro ao registrar contato com DDD inexistente")]
    public async Task Create_ShouldThrowException_WhenDDDNotFound()
    {
        // Arrange
        var contato = new Contato { Nome = "Teste", NrTelefone = "1234567890", Email = "teste@example.com" };
        byte nrDDD = 11;
        _dddServiceMock.Setup(r => r.Get()).ReturnsAsync(new List<DDD>());

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => _contatoService.Create(contato, nrDDD));
    }

    [Fact]
    [Trait("Service", "Sucesso ao registrar contato com DDD existente")]
    public async Task Create_ShouldCreateContato_WhenDDDExists()
    {
        // Arrange
        var contato = new Contato { Nome = "Teste", NrTelefone = "1234567890", Email = "teste@example.com" };
        byte nrDDD = 11;
        var ddd = new DDD { Id = 1, NrDDD = nrDDD };
        _dddServiceMock.Setup(r => r.Get()).ReturnsAsync(new List<DDD> { ddd });
        _contatoRepositoryMock.Setup(r => r.Create(It.IsAny<Contato>())).ReturnsAsync(contato);

        // Act
        var result = await _contatoService.Create(contato, nrDDD);

        // Assert
        Assert.NotNull(result);
        _contatoRepositoryMock.Verify(r => r.Create(It.IsAny<Contato>()), Times.Once);
    }

    [Fact]
    [Trait("Service", "Sucesso ao obter contato com DDD existente")]
    public async Task Get_ShouldReturnContatoWithDDD()
    {
        // Arrange
        long contatoId = 1;
        var contato = new Contato { Id = contatoId, Nome = "Teste", NrTelefone = "1234567890", Email = "teste@example.com"};
        contato.SetDDDId(1);
        var ddd = DDD.SetDDD(1, 11);
        _contatoRepositoryMock.Setup(r => r.Get(contatoId)).ReturnsAsync(contato);
        _dddServiceMock.Setup(r => r.Get(1)).ReturnsAsync(ddd);

        // Act
        var result = await _contatoService.Get(contatoId);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    [Trait("Service", "Sucesso ao obter todos contatos")]
    public async Task Get_ShouldReturnAllContatosWithDDDs()
    {
        // Arrange
        var contatos = new List<Contato>
        {
            new Contato { Id = 1, Nome = "Teste1", NrTelefone = "1234567890", Email = "teste1@example.com"},
            new Contato { Id = 2, Nome = "Teste2", NrTelefone = "0987654321", Email = "teste2@example.com"}
        };
        foreach(var contato in contatos)
        {
            contato.SetDDDId(contato.Id);
        }
        var ddds = new List<DDD>
        {
            new DDD { Id = 1, NrDDD = 11 },
            new DDD { Id = 2, NrDDD = 22 }
        };
        _contatoRepositoryMock.Setup(r => r.Get()).ReturnsAsync(contatos);
        _dddServiceMock.Setup(r => r.Get()).ReturnsAsync(ddds);

        // Act
        var result = await _contatoService.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ddds.Count, result.Select(c => c.DDD).Distinct().Count());
    }

    [Fact]
    [Trait("Service", "Sucesso ao remover contato")]
    public async Task Remove_ShouldCallRemoveOnRepository()
    {
        // Arrange
        long contatoId = 1;

        // Act
        await _contatoService.Remove(contatoId);

        // Assert
        _contatoRepositoryMock.Verify(r => r.Remove(contatoId), Times.Once);
    }

    [Fact]
    [Trait("Service", "Erro ao atualizar contato com DDD inexistente")]
    public async Task Update_ShouldThrowException_WhenDDDNotFound()
    {
        // Arrange
        var contato = new Contato { Id = 1, Nome = "Teste", NrTelefone = "1234567890", Email = "teste@example.com" };
        byte nrDDD = 11;
        _dddServiceMock.Setup(r => r.Get()).ReturnsAsync(new List<DDD>());

        // Act & Assert
        await Assert.ThrowsAsync<DomainException>(() => _contatoService.Update(contato, nrDDD));
    }

    [Fact]
    [Trait("Service", "Sucesso ao atualizar contato com DDD existente")]
    public async Task Update_ShouldUpdateContato_WhenDDDExists()
    {
        // Arrange
        var contato = new Contato { Id = 1, Nome = "Teste", NrTelefone = "1234567890", Email = "teste@example.com" };
        byte nrDDD = 11;
        var ddd = new DDD { Id = 1, NrDDD = nrDDD };
        _dddServiceMock.Setup(r => r.Get()).ReturnsAsync(new List<DDD> { ddd });
        _contatoRepositoryMock.Setup(r => r.Update(It.IsAny<Contato>())).ReturnsAsync(contato);

        // Act
        var result = await _contatoService.Update(contato, nrDDD);

        // Assert
        Assert.NotNull(result);
        _contatoRepositoryMock.Verify(r => r.Update(It.IsAny<Contato>()), Times.Once);
    }
}
