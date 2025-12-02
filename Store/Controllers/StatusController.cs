using BusinessLogic.Dtos;
using BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAll([FromQuery] StatusFilterDto? filter = null)
        {
            try
            {
                var statuses = await _statusService.GetAllAsync(filter);
                return Ok(statuses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener estados: {ex.Message}");
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetActive()
        {
            try
            {
                var activeStatuses = await _statusService.GetActiveAsync();
                return Ok(activeStatuses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener estados activos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDto>> Get(int id)
        {
            var status = await _statusService.Get(id);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<StatusDto>> Post([FromBody] StatusCreateDto statusCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var statusDto = await _statusService.CreateAsync(statusCreateDto);
                return CreatedAtAction(nameof(Get), new { id = statusDto.Id }, statusDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el estado: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StatusDto statusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _statusService.UpdateAsync(id, statusDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el estado: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _statusService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el estado: {ex.Message}");
            }
        }
    }
}

