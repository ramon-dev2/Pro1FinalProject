using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class CustomerCreateDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [MaxLength(100, ErrorMessage = "El apellido no puede exceder 100 caracteres")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        [MaxLength(200, ErrorMessage = "El email no puede exceder 200 caracteres")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        public string? Phone { get; set; }

        [MaxLength(500, ErrorMessage = "La dirección no puede exceder 500 caracteres")]
        public string? Address { get; set; }
    }
}

