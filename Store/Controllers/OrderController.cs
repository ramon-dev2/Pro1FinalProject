using BusinessLogic.Dtos;
using BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll([FromQuery] OrderFilterDto? filter = null)
        {
            try
            {
                var orders = await _orderService.GetAllAsync(filter);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener órdenes: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            var order = await _orderService.Get(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Post([FromBody] OrderCreateDto orderCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var orderDto = await _orderService.CreateOrderAsync(orderCreateDto);
                return CreatedAtAction(nameof(Get), new { id = orderDto.Id }, orderDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la orden: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderDto orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo");
            }

            var existingOrder = await _orderService.Get(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            try
            {
                _orderService.Update(orderDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la orden: {ex.Message}");
            }
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<OrderDto>> UpdateStatus(int id, [FromBody] OrderStatusUpdateDto statusUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedOrder = await _orderService.UpdateOrderStatusAsync(id, statusUpdateDto.StatusId);
                return Ok(updatedOrder);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el estado de la orden: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.Get(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderService.Delete(order);
            return NoContent();
        }
    }
}

