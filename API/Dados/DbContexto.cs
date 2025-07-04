using API.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace API.Dados
{
    public class DbContexto : DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endereco>()
                    .HasOne(e => e.Cliente)
                    .WithOne(c => c.Endereco)
                    .HasForeignKey<Endereco>(e => e.ClienteId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
