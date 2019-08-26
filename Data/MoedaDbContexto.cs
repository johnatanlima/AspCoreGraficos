using AspCoreGraficos.Models;
using Microsoft.EntityFrameworkCore;

namespace AspCoreGraficos.Data
{
    public class MoedaDbContexto : DbContext {
   
    public MoedaDbContexto (DbContextOptions<MoedaDbContexto> options) : base (options) 
    {
        
    }
    
    public DbSet<Moeda> Moedas { get; set; }

    }
}