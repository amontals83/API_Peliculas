using API_Peliculas.Modelos.Dtos;
using API_Peliculas.Modelos;
using API_Peliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API_Peliculas.Controllers
{
    //39º PASO
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usRepo;
        private readonly IMapper _mapper;
        protected RespuestaAPI _respuestaAPI; //42º PASO 1/3

        public UsuariosController(IUsuarioRepositorio usRepo, IMapper mapper)
        {
            _usRepo = usRepo;
            _mapper = mapper;
            this._respuestaAPI = new RespuestaAPI(); //42º PASO 3/3
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //40º PASO
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _usRepo.GetUsuarios();
            var listaUsuariosDto = new List<UsuarioDto>();

            foreach (var lista in listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(lista));
            }

            return Ok(listaUsuariosDto);
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        
        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuario(int usuarioId)
        {
            var itemUsuario = _usRepo.GetUsuario(usuarioId);

            if (itemUsuario == null) return NotFound();

            var itemUsuarioDto = _mapper.Map<UsuarioDto>(itemUsuario);

            return Ok(itemUsuarioDto);
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //41º PASO
        [HttpPost("registro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDto usuarioRegistroDto)
        {
            bool validaNombreUsuarioUnico = _usRepo.IsUniqueUser(usuarioRegistroDto.NombreUsuario);

            if (!validaNombreUsuarioUnico)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.IsSuccess = false;
                _respuestaAPI.ErrorMessages.Add("El nombre de usuario ya existe");
                return BadRequest();
            }

            var usuario = await _usRepo.Registro(usuarioRegistroDto);

            if (usuario == null)
            {
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.IsSuccess = false;
                _respuestaAPI.ErrorMessages.Add("Error en el registro");
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.IsSuccess = true;
            return Ok(_respuestaAPI);
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //43º PASO
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var respuestaLogin = await _usRepo.Login(usuarioLoginDto);

            if (respuestaLogin.Usuario == null || string.IsNullOrEmpty(respuestaLogin.Token))
            {
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.IsSuccess = false;
                _respuestaAPI.ErrorMessages.Add("El nombre de usuario o password son incorrectos");
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.IsSuccess = true;
            _respuestaAPI.Result = respuestaLogin;
            return Ok(_respuestaAPI);
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        /*
        [HttpPatch("{usuarioId:int}", Name = "ActualizarPatchUsuario")]
        [ProducesResponseType(201, Type = typeof(UsuarioDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchUsuario(int usuarioId, [FromBody] UsuarioDto usuarioDto) //LO RECIBE EN FORMATO JSON
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (usuarioDto == null || usuarioId != usuarioDto.Id) return BadRequest(ModelState);

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            if (!_usRepo.ActualizarUsuario(usuario))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {usuario.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //15º PASO
        [HttpDelete("{usuarioId:int}", Name = "BorrarUsuario")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BorrarUsuario(int usuarioId)
        {
            if (!_usRepo.ExisteUsuario(usuarioId)) return NotFound();

            var usuario = _usRepo.GetUsuario(usuarioId);

            if (!_usRepo.BorrarUsuario(usuario))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {usuario.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        */
    }
}
