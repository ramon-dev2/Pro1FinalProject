using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class StatusCreateDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "La descripción no puede exceder 200 caracteres")]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

