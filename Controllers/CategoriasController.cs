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
    }
}
