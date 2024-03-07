using API_Peliculas.Modelos.Dtos;
using API_Peliculas.Modelos;
using API_Peliculas.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Peliculas.Controllers
{
    //37º PASO
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usRepo;
        private readonly IMapper _mapper;

        public UsuariosController(IUsuarioRepositorio usRepo, IMapper mapper)
        {
            _usRepo = usRepo;
            _mapper = mapper;
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //39º PASO
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

            var itemUsuarioDto = _mapper.Map<UsuarioDto>(usuarioId);

            return Ok(itemUsuarioDto);
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //13º PASO
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UsuarioDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearUsuario([FromBody] CrearUsuarioDto crearUsuarioDto) //LO RECIBE EN FORMATO JSON
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (crearUsuarioDto == null) return BadRequest(ModelState);

            if (_usRepo.ExisteUsuario(crearUsuarioDto.Nombre))
            {
                ModelState.AddModelError("", "La categoría ya existe");
                return StatusCode(404, ModelState);
            }

            var usuario = _mapper.Map<Usuario>(crearUsuarioDto);

            if (!_usRepo.CrearUsuario(usuario))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el registro {usuario.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetUsuario", new { usuarioId = usuario.Id }, usuario); //SE DEVUELVE EL ID DE LA CATEGORIA QUE SE CREÓ
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //14º PASO
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
    }
}
