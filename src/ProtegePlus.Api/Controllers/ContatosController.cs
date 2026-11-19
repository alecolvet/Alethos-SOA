using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProtegePlus.Api.Data;
using ProtegePlus.Api.Domain;
using ProtegePlus.Api.DTOs;

namespace ProtegePlus.Api.Controllers;

[ApiController]
[Route("api/contatos")]
public class ContatosController(ProtegePlusDbContext db) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Criar(CriarContatoRequest request)
    {
        if (!await db.Usuarios.AnyAsync(x => x.Id == request.UsuarioId))
            return NotFound(new { erro = "Usuário não encontrado." });
        var telefone = request.Telefone.Trim();
        if (await db.ContatosConfianca.AnyAsync(x => x.UsuarioId == request.UsuarioId && x.Telefone == telefone))
            return Conflict(new { erro = "Este telefone já está cadastrado para o usuário." });
        var contato = new ContatoConfianca
        {
            UsuarioId = request.UsuarioId, Nome = request.Nome.Trim(),
            Telefone = telefone, Parentesco = request.Parentesco.Trim()
        };
        db.ContatosConfianca.Add(contato);
        await db.SaveChangesAsync();
        return Created($"/api/contatos/{contato.Id}", contato);
    }

    [HttpGet("usuario/{usuarioId:int}")]
    public async Task<IActionResult> ListarPorUsuario(int usuarioId)
    {
        if (!await db.Usuarios.AnyAsync(x => x.Id == usuarioId))
            return NotFound(new { erro = "Usuário não encontrado." });
        return Ok(await db.ContatosConfianca.AsNoTracking().Where(x => x.UsuarioId == usuarioId).ToListAsync());
    }
}
