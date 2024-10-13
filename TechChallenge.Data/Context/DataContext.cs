using TechChallenge.Data.Mappings;
using TechChallenge.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace TechChallenge.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("BdPadraoConnection");
            }
        }

        public virtual DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContatoMap());
            base.OnModelCreating(builder);
        }
    }
}
