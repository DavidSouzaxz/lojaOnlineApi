using System.ComponentModel.DataAnnotations;

namespace LojaOnline.Dtos;

public class CategoriaCreateDto
{
    [Required] [StringLength(50)] public string Nome { get; set; } = string.Empty;
}