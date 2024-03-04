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

        //12º PASO
        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)] //EndPoints
        [ProducesResponseType(StatusCodes.Status200OK)] //EndPoints
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //EndPoints
        [ProducesResponseType(StatusCodes.Status404NotFound)] //EndPoints
        public IActionResult GetCategoria(int categoriaId)
        {
            var itemCategoria = _ctRepo.GetCategoria(categoriaId);

            if (itemCategoria == null) return NotFound();

            var itemCategoriaDto = _mapper.Map<CategoriaDto>(categoriaId);

            return Ok(itemCategoriaDto);
        }
    }
}

