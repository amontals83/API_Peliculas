using API_Peliculas.Modelos;
using API_Peliculas.Modelos.Dtos;

namespace API_Peliculas.Repositorio.IRepositorio
{
    //33º PASO
    public interface IUsuarioRepositorio
    {
        ICollection<AppUsuario> GetUsuarios();
        AppUsuario GetUsuario(string usuarioId);
        bool IsUniqueUser(string usuario);
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        //Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto);
        Task<UsuarioDatosDto> Registro(UsuarioRegistroDto usuarioRegistroDto);
    }
}

