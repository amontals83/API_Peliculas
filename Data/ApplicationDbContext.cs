using API_Peliculas.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Peliculas.Data
{
    //3º PASO
    //public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : IdentityDbContext<AppUsuario> //51º
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) 
        {
        }

        //52º
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        //AGREGAR LOS MODELOS AQUI
        public DbSet<Categoria> Categoria { get; set; }
        //17º PASO -> siguiente paso es un txt
        public DbSet<Pelicula> Pelicula { get; set; }
        //26º PASO -> siguiente paso es un txt
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<AppUsuario> AppUsuario { get; set; } // 53º -> siguiente paso es un txt
    }
}
