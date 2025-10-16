using Application;
using Application.Dtos.HistorialTarea;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialesTareasController: ControllerBase
    {
        private readonly ILogger<HistorialesTareasController> _logger;
        private readonly IApplication<HistorialTarea> _historialTarea;
        private readonly IMapper _mapper;
        public HistorialesTareasController(ILogger<HistorialesTareasController> logger, IApplication<HistorialTarea> historialTarea, IMapper mapper)
        {
            _logger = logger;
            _historialTarea = historialTarea;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> All()
        {
            return Ok(_mapper.Map<IList<HistorialTareaResponseDto>>(_historialTarea.GetAll()));
        }
        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> ById(int? Id)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            HistorialTarea historialTarea = _historialTarea.GetById(Id.Value);
            if (historialTarea == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<HistorialTareaResponseDto>(historialTarea));
        }
        [HttpPost]
        public async Task<IActionResult> Crear(HistorialTareaRequestDto historialTareaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var historialTarea = _mapper.Map<HistorialTarea>(historialTareaRequestDto);
            _historialTarea.Save(historialTarea);
            return Ok(historialTarea.Id);
        }
        [HttpPut]
        public async Task<IActionResult> Editar(int? Id, HistorialTareaRequestDto historialTareaRequestDto)
        {
            if (!Id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            HistorialTarea historialTareaBack = _historialTarea.GetById(Id.Value);
            if (historialTareaBack == null)
            {
                return NotFound();
            }
            historialTareaBack = _mapper.Map<HistorialTarea>(historialTareaRequestDto);
            _historialTarea.Save(historialTareaBack);
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
            HistorialTarea historialTareaBack = _historialTarea.GetById(Id.Value);
            if (historialTareaBack == null)
            {
                return NotFound();
            }
            _historialTarea.Delete(historialTareaBack.Id);
            return Ok();
        }
    }
}
