namespace LojaOnline.Models;

public class Usuario
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public byte[] SenhaHash { get; set; } = [];
    
    public byte[] SenhaSalt { get; set; } = [];
    
    public string Role { get; set; } = "Cliente";
}