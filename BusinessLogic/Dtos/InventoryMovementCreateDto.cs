using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class InventoryMovementCreateDto
    {
        [Required(ErrorMessage = "El producto es requerido")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "El tipo de movimiento es requerido")]
        [RegularExpression("Entry|Exit|Adjustment|Sale|Return", ErrorMessage = "El tipo de movimiento debe ser: Entry, Exit, Adjustment, Sale o Return")]
        public string MovementType { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad es requerida")]
        public int Quantity { get; set; }

        [MaxLength(500, ErrorMessage = "La razón no puede exceder 500 caracteres")]
        public string? Reason { get; set; }

        [MaxLength(100, ErrorMessage = "La referencia no puede exceder 100 caracteres")]
        public string? Reference { get; set; }

        public string? CreatedBy { get; set; }
    }
}

