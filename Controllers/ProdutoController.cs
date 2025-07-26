// Controllers/ProdutoController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LojaOnline.Data;
using LojaOnline.Models;
using LojaOnline.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace LojaOnline.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProdutoController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetAll()
    {
        
        var produtos = await _context.Produtos.Include(p => p.Categoria).ToListAsync();
        if(produtos.Count == 0) return NotFound("Nenhum produto encontrado.");
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        return Ok(new
        {
            mensagem = "Produtos listados com sucesso.",
            dados = produtosDto
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoDto>> GetById(int id)
    {
        var produto = await _context.Produtos.Include(p => p.Categoria)
                                             .FirstOrDefaultAsync(p => p.Id == id);

        if (produto == null) return NotFound("Id não encontrado.");
    
        var produtoDto = _mapper.Map<ProdutoDto>(produto);
        return Ok(new
        {
            mensagem = "Produto encontrado",
            dados = produtoDto
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ProdutoDto>> Create(ProdutoCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
        if (!categoriaExiste) return BadRequest("Categoria inválida.");

        var produto = _mapper.Map<Produto>(dto);
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        var produtoDto = _mapper.Map<ProdutoDto>(
            await _context.Produtos.Include(p => p.Categoria).FirstAsync(p => p.Id == produto.Id)
        );

        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, new
        {
            mensagem = "Produto cadastrado com sucesso.",
            dados =produtoDto
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProdutoCreateDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        _mapper.Map(dto, produto);
        await _context.SaveChangesAsync();
        
        var produtoDto = _mapper.Map(dto, produto);
        
        return Ok(new
        {
            mensagem = "Produto atualizado com sucesso.",
            dados = produtoDto
        });
        
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();

        return Ok("Produto removido com sucesso.");
    }
}
