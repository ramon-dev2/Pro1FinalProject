using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class OrderStatusUpdateDto
    {
        [Required(ErrorMessage = "El estado es requerido")]
        public int StatusId { get; set; }
    }
}

