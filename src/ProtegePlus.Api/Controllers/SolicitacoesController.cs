using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProtegePlus.Api.Data;
using ProtegePlus.Api.DTOs;
using ProtegePlus.Api.Services;

namespace ProtegePlus.Api.Controllers;

[ApiController]
[Route("api/solicitacoes-ajuda")]
public class SolicitacoesController(ProtegePlusDbContext db, ISolicitacaoAjudaService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Criar(CriarSolicitacaoRequest request)
    {
        var (item, erro) = await service.CriarAsync(request);
        return erro is not null ? BadRequest(new { erro }) : CreatedAtAction(nameof(Consultar), new { id = item!.Id }, item);
    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] int? usuarioId, [FromQuery] string? status)
    {
        var query = db.SolicitacoesAjuda.AsNoTracking().AsQueryable();
        if (usuarioId.HasValue) query = query.Where(x => x.UsuarioId == usuarioId.Value);
        if (!string.IsNullOrWhiteSpace(status)) query = query.Where(x => x.Status == status);
        return Ok(await query.OrderByDescending(x => x.CriadaEm).ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Consultar(int id)
    {
        var item = await db.SolicitacoesAjuda.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        return item is null ? NotFound(new { erro = "Solicitação não encontrada." }) : Ok(item);
    }

    [HttpPatch("{id:int}/status")]
    public async Task<IActionResult> AtualizarStatus(int id, AtualizarStatusRequest request)
    {
        var (item, erro) = await service.AtualizarStatusAsync(id, request.Status);
        if (erro == "Solicitação não encontrada.") return NotFound(new { erro });
        return erro is not null ? BadRequest(new { erro }) : Ok(item);
    }
}
