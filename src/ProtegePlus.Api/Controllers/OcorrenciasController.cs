using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProtegePlus.Api.Data;
using ProtegePlus.Api.DTOs;
using ProtegePlus.Api.Services;

namespace ProtegePlus.Api.Controllers;

[ApiController]
[Route("api/ocorrencias")]
public class OcorrenciasController(ProtegePlusDbContext db, IOcorrenciaService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Criar(CriarOcorrenciaRequest request)
    {
        var (item, erro) = await service.CriarAsync(request);
        return erro is not null ? BadRequest(new { erro }) : CreatedAtAction(nameof(Consultar), new { id = item!.Id }, item);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Consultar(int id)
    {
        var item = await db.Ocorrencias.AsNoTracking().Include(x => x.Usuario).FirstOrDefaultAsync(x => x.Id == id);
        return item is null ? NotFound(new { erro = "Ocorrência não encontrada." }) : Ok(item);
    }

    [HttpGet("usuario/{usuarioId:int}")]
    public async Task<IActionResult> Historico(int usuarioId)
    {
        if (!await db.Usuarios.AnyAsync(x => x.Id == usuarioId))
            return NotFound(new { erro = "Usuário não encontrado." });
        var itens = await db.Ocorrencias.AsNoTracking().Where(x => x.UsuarioId == usuarioId)
            .OrderByDescending(x => x.RegistradaEm).ToListAsync();
        return Ok(itens);
    }
}
