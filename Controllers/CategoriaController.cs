using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaOnline.Data;
using LojaOnline.Dtos;
using LojaOnline.Models;
using Microsoft.AspNetCore.Authorization;

namespace LojaOnline.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CategoriaController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetAll()
    {
        var categorias = await _context.Categorias.ToListAsync();
        if(categorias.Count == 0) return Ok("Nenhuma categoria encontrada.");
        
        var categoriasDto = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
        
        return Ok(new
        {
            mensagem = "Categorias listadas com sucesso.",
            dados = categoriasDto
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaDto>> GetById(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return NotFound("Id não encontrado.");
        var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
        
        return Ok(new
        {
            mensagem = "Categoria listada com sucesso.",
            dados = categoriaDto
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> Create(CategoriaCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var categoria = _mapper.Map<Categoria>(dto);
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        
        
        
        var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
        
        return  CreatedAtAction(nameof(GetById), new { id = categoria.Id }, new {
            mensagem = "Categoria cadastrada com sucesso.", 
            dados =categoriaDto
        });
    }
    
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CategoriaCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();

        _mapper.Map(dto, categoria);
        await _context.SaveChangesAsync();
        
        var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
        
        return Ok(new
        {
            messagem = "Categoria atualizada com sucesso.",
            dados = categoriaDto
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return NotFound();
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return Ok("Categoria removida com sucesso.");
    }
}