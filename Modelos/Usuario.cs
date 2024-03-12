using System.ComponentModel.DataAnnotations;

namespace API_Peliculas.Modelos
{
    //25º - LOS SIGUIENTES PASOS EN ADELANTE PARA EL USUARIO SON UNA MODIFICACION DEL MODELO ANTES DEL IDENTITY. PUEDE HABER LIO ENTRE LOS PASOS
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
