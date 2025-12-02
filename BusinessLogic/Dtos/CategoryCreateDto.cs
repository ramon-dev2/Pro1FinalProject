using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Dtos
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Name { get; set; } = string.Empty;
    }
}

