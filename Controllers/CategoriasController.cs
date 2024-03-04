using API_Peliculas.Modelos;
using API_Peliculas.Modelos.Dtos;
using API_Peliculas.Repositorio.IRepositorio;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc;

namespace API_Peliculas.Controllers
{
    //11º PASO
    [ApiController]
    [Route("api/categorias")] //[Route("api/[controller]")] //Es otra opcion
    public class CategoriasController : ControllerBase //ControllerBase es un controlador para APIs
    {
        private readonly ICategoriaRepositorio _ctRepo;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepositorio ctRepo, IMapper mapper)
        {
            _ctRepo = ctRepo;
            _mapper = mapper;
        }

        // ///////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] //EndPoints
        [ProducesResponseType(StatusCodes.Status200OK)] //EndPoints
        public IActionResult GetCategorias()
        {
            var listaCategorias = _ctRepo.GetCategorias();
            var listaCategoriasDto = new List<CategoriaDto>();

            foreach (var lista in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(lista));
            }

            return Ok(listaCategoriasDto);
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //12º PASO
        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoria(int categoriaId)
        {
            var itemCategoria = _ctRepo.GetCategoria(categoriaId);

            if (itemCategoria == null) return NotFound();

            var itemCategoriaDto = _mapper.Map<CategoriaDto>(categoriaId);

            return Ok(itemCategoriaDto);
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //13º PASO
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CategoriaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearCategoria([FromBody] CrearCategoriaDto crearCategoriaDto) //LO RECIBE EN FORMATO JSON
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (crearCategoriaDto == null) return BadRequest(ModelState);

            if (_ctRepo.ExisteCategoria(crearCategoriaDto.Nombre))
            {
                ModelState.AddModelError("", "La categoría ya existe");
                return StatusCode(404, ModelState);
            }

            var categoria = _mapper.Map<Categoria>(crearCategoriaDto);

            if (!_ctRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria);
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //14º PASO
        [HttpPatch("{categoriaId:int}", Name = "ActualizarPatchCategoria")]
        [ProducesResponseType(201, Type = typeof(CategoriaDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ActualizarPatchCategoria(int categoriaId, [FromBody] CategoriaDto categoriaDto) //LO RECIBE EN FORMATO JSON
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (categoriaDto == null || categoriaId != categoriaDto.Id) return BadRequest(ModelState);

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            if (!_ctRepo.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal actualizando el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // ///////////////////////////////////////////////////////////////////////////////////
        //15º PASO
        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult BorrarCategoria(int categoriaId)
        {
            if (!_ctRepo.ExisteCategoria(categoriaId)) return NotFound();

            var categoria = _ctRepo.GetCategoria(categoriaId);

            if (!_ctRepo.BorrarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió mal borrando el registro {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

