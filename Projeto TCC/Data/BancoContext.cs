using Microsoft.EntityFrameworkCore;
using Projeto_TCC.Models;

namespace Projeto_TCC.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) 
        { }

        public DbSet<CadastroModel> cadastro { get; set; }
    }
}
