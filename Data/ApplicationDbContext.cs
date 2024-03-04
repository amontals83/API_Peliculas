using API_Peliculas.Modelos;
using Microsoft.EntityFrameworkCore;

namespace API_Peliculas.Data
{
    //3º PASO
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) 
        {
        }

        //AGREGAR LOS MODELOS AQUI
        public DbSet<Categoria> Categoria { get; set; }
    }
}
