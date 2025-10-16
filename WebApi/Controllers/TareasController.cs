using Application;
using Application.Dtos.Tarea;
using Application.Dtos.Usuario;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ILogger<TareasController> _logger;
        private readonly IApplication<Tarea> _tarea;
        private readonly IMapper _mapper;
        public TareasController(ILogger<TareasController> logger, IApplication<Tarea> tarea, IMapper mapper)
        {
            _logger = logger;
            _tarea = tarea;
           _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<TareaResponseDto>>(_tarea.GetAll()));
        }
        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                   return BadRequest();
            }
            Tarea tarea = _tarea.GetById(Id.Value);
            if (tarea == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TareaResponseDto>(tarea));
        }
        [HttpPost]
        public async Task<IActionResult> Crear(TareaRequestDto tareaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var tarea = _mapper.Map<Tarea>(tareaRequestDto);
            _tarea.Save(tarea);
            return Ok(tarea.Id);
        }
        [HttpPut]
        public async Task<IActionResult> Editar(int? Id,TareaRequestDto tareaRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Tarea tareaBack = _tarea.GetById(Id.Value);
            if (tareaBack == null)
            {
                return NotFound();
            }
            tareaBack = _mapper.Map<Tarea>(tareaRequestDto);
            _tarea.Save(tareaBack);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Borrar(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Tarea tareaBack = _tarea.GetById(Id.Value);
            if (tareaBack == null)
            {
                return NotFound();
            }
            _tarea.Delete(tareaBack.Id);
            return Ok();
        }
    }
}
