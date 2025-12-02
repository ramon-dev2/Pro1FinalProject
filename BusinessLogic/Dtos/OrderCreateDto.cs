using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class OrderCreateDto
    {
        [Required(ErrorMessage = "El cliente es requerido")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Los items de la orden son requeridos")]
        [MinLength(1, ErrorMessage = "La orden debe tener al menos un item")]
        public List<OrderItemCreateDto> OrderItems { get; set; } = new List<OrderItemCreateDto>();

        [Required(ErrorMessage = "El estado es requerido")]
        public int StatusId { get; set; } = 1;
    }

    public class OrderItemCreateDto
    {
        [Required(ErrorMessage = "El producto es requerido")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "La cantidad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Quantity { get; set; }
    }
}

