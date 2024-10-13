using TechChallenge.Data.Context;
using TechChallenge.Data.Repositories;
using TechChallenge.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace TechChallenge.Tests.Data;

public class ContatoRepositoryTest
{
    private readonly DataContext _context;
    private readonly ContatoRepository _contatoRepository;

    public ContatoRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new DataContext(options);
        _contatoRepository = new ContatoRepository(_context);
    }

    [Fact]
    [Trait("Repository", "Sucesso para registrar contato")]
    public async Task Create_ShouldAddContatoToDatabase()
    {
        // Arrange
        var contato = new Contato { Nome = "Teste", NrTelefone = "1234567890", Email = "teste@example.com" };
        contato.SetDDDId(1);

        // Act
        var result = await _contatoRepository.Create(contato);
        var addedContato = await _context.Contatos.FindAsync(result.Id);

        // Assert
        Assert.NotNull(addedContato);
        Assert.Equal(contato.Nome, addedContato.Nome);
    }

    [Fact]
    [Trait("Repository", "Sucesso para obter contato pelo id")]
    public async Task Get_ShouldReturnContatoById()
    {
        // Arrange
        var contato = new Contato { Nome = "Teste", NrTelefone = "1234567890", Email = "teste@example.com" };
        contato.SetDDDId(1);
        _context.Contatos.Add(contato);
        await _context.SaveChangesAsync();

        // Act
        var result = await _contatoRepository.Get(contato.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contato.Id, result.Id);
    }

    [Fact]
    [Trait("Repository", "Sucesso para obter todos os contatos")]
    public async Task Get_ShouldReturnAllContatos()
    {
        // Arrange
        var contatos = new List<Contato>
        {
            new() { Nome = "Teste11", NrTelefone = "981260057", Email = "teste11@example.com" },
            new() { Nome = "Teste22", NrTelefone = "996110992", Email = "teste22@example.com" }
        };

        foreach (var contato in contatos)
        {
            contato.SetDDDId(contato.Id);
        }
        _context.Contatos.AddRange(contatos);
        await _context.SaveChangesAsync();

        // Act
        var result = await _contatoRepository.Get();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    [Trait("Repository", "Sucesso para remover usuario")]
    public async Task Remove_ShouldDeleteContatoFromDatabase()
    {
        // Arrange
        var contato = new Contato { Nome = "Teste", NrTelefone = "1234567890", Email = "teste@example.com" };
        contato.SetDDDId(1);
        _context.Contatos.Add(contato);
        await _context.SaveChangesAsync();

        // Act
        await _contatoRepository.Remove(contato.Id);
        var deletedContato = await _context.Contatos.FindAsync(contato.Id);

        // Assert
        Assert.Null(deletedContato);
    }

    [Fact]
    [Trait("Repository", "Sucesso para atualizar contato")]
    public async Task Update_ShouldModifyContatoInDatabase()
    {
        // Arrange
        var contato = new Contato { Nome = "Teste", NrTelefone = "1234567890", Email = "teste@example.com" };
        contato.SetDDDId(1);
        _context.Contatos.Add(contato);
        await _context.SaveChangesAsync();

        contato.Nome = "Teste Atualizado";

        // Act
        var result = await _contatoRepository.Update(contato);
        var updatedContato = await _context.Contatos.FindAsync(result.Id);

        // Assert
        Assert.NotNull(updatedContato);
        Assert.Equal("Teste Atualizado", updatedContato.Nome);
    }
}
