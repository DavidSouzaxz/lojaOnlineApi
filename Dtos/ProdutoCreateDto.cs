using System.ComponentModel.DataAnnotations;

namespace LojaOnline.Dtos;

public class ProdutoCreateDto
{
    [Required] [StringLength(100)] public string Nome { get; set; } = string.Empty;
    
    [Required] [StringLength(200)] public string Descricao { get; set; } = string.Empty;
    
    [Required] public decimal Preco { get; set; }
    
    [Required] public string ImagemUrl { get; set; } = string.Empty;
    
    [Required] public int CategoriaId { get; set; }
}