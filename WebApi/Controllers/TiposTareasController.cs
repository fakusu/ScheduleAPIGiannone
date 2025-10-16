using Application;
using Application.Dtos.TipoTarea;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposTareasController : ControllerBase
    {
        private readonly ILogger<TiposTareasController> _logger;
        private readonly IApplication<TipoTarea> _tipoTarea;
        private readonly IMapper _mapper;
        public TiposTareasController(ILogger<TiposTareasController> logger, IApplication<TipoTarea> tipoTarea, IMapper mapper)
        {
            _logger = logger;
            _tipoTarea = tipoTarea;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<TipoTareaResponseDto>>(_tipoTarea.GetAll()));
        }
        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            TipoTarea tipoTarea = _tipoTarea.GetById(Id.Value);
            if (tipoTarea == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TipoTareaResponseDto>(tipoTarea));
        }
        [HttpPost]
        public async Task<IActionResult> Crear(TipoTareaRequestDto tipoTareaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var tipoTarea = _mapper.Map<TipoTarea>(tipoTareaRequestDto);
            _tipoTarea.Save(tipoTarea);
            return Ok(tipoTarea.Id);
        }
        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, TipoTareaRequestDto tipoTareaRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            TipoTarea tipoTareaBack = _tipoTarea.GetById(Id.Value);
            if (tipoTareaBack == null)
            {
                return NotFound();
            }
            tipoTareaBack = _mapper.Map<TipoTarea>(tipoTareaRequestDto);
            _tipoTarea.Save(tipoTareaBack);
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
            TipoTarea tipoTareaBack = _tipoTarea.GetById(Id.Value);
            if (tipoTareaBack == null)
            {
                return NotFound();
            }
            _tipoTarea.Delete(tipoTareaBack.Id);
            return Ok();
        }
    }
}

