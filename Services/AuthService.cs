using System.Security.Cryptography;
using System.Text;
using LojaOnline.Data;
using LojaOnline.Dtos;
using LojaOnline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LojaOnline.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<string?> RegistrarAsync(UsuarioRegisterDto dto)
    {
        if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email)) return "Email em uso";
        CreatePasswordHash(dto.Senha, out byte[] hash, out byte[] salt);

        var user = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = hash,
            SenhaSalt = salt
        };

        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();
        return GenerateToken(user);
    }

    public async Task<string?> LoginAsync(UsuarioLoginDto loginDto)
    {
        var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null) return "Usuário não encontrado";
        
        if (!VerifyPasswordHash(loginDto.Senha, user.SenhaHash, user.SenhaSalt))
            return "Senha incorreta";
            
        return GenerateToken(user);
    }

    private void CreatePasswordHash(string senha, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
    }

    private bool VerifyPasswordHash(string senha, byte[] hash, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
        return computed.SequenceEqual(hash);
    }

    private string GenerateToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Role, usuario.Role)
        };
    
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],      
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}