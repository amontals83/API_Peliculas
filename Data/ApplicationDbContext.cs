using API_Peliculas.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Peliculas.Data
{
    //3º
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
        //17º -> siguiente - es un txt
        public DbSet<Pelicula> Pelicula { get; set; }
        //26º
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<AppUsuario> AppUsuario { get; set; } // 53º -> siguiente - es un txt
    }
}
