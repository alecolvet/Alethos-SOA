using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProtegePlus.Api.Data;
using ProtegePlus.Api.Domain;
using ProtegePlus.Api.DTOs;

namespace ProtegePlus.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController(ProtegePlusDbContext db) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Criar(CriarUsuarioRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        if (await db.Usuarios.AnyAsync(x => x.Email == email))
            return Conflict(new { erro = "Já existe um usuário cadastrado com este e-mail." });
        var usuario = new Usuario { Nome = request.Nome.Trim(), Email = email };
        db.Usuarios.Add(usuario);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(Consultar), new { id = usuario.Id }, usuario);
    }

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await db.Usuarios.AsNoTracking().OrderBy(x => x.Nome).ToListAsync());

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Consultar(int id)
    {
        var usuario = await db.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return usuario is null ? NotFound(new { erro = "Usuário não encontrado." }) : Ok(usuario);
    }
}
