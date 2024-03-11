using System.ComponentModel.DataAnnotations;

namespace API_Peliculas.Modelos
{
    //25º PASO - LOS SIGUIENTES PASOS PARA EL USUARIO SON SOLO SI NO SE APLICA IDENTITY
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
