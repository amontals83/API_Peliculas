using Microsoft.AspNetCore.Identity;

namespace API_Peliculas.Modelos
{
    //51º
    public class AppUsuario : IdentityUser
    {
        //AÑADIR CAMPOS PERSONALIZADOS
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public AppUsuario() { }
    }
}
