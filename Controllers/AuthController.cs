// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using LojaOnline.Dtos;
using LojaOnline.Services;

namespace LojaOnline.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UsuarioRegisterDto dto)
    {
        var token = await _authService.RegistrarAsync(dto);
        if (token == null) return BadRequest("E-mail já cadastrado.");
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UsuarioLoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        if (token == null) return Unauthorized("Credenciais inválidas.");
        return Ok(new { token });
    }
}