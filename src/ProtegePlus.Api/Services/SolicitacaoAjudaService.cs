using Microsoft.EntityFrameworkCore;
using ProtegePlus.Api.Data;
using ProtegePlus.Api.Domain;
using ProtegePlus.Api.DTOs;

namespace ProtegePlus.Api.Services;

public interface ISolicitacaoAjudaService
{
    Task<(SolicitacaoAjuda? Item, string? Erro)> CriarAsync(CriarSolicitacaoRequest request);
    Task<(SolicitacaoAjuda? Item, string? Erro)> AtualizarStatusAsync(int id, string status);
}

public class SolicitacaoAjudaService(ProtegePlusDbContext db) : ISolicitacaoAjudaService
{
    private static readonly string[] StatusPermitidos = ["Pendente", "EmAtendimento", "Concluida", "Cancelada"];

    public async Task<(SolicitacaoAjuda? Item, string? Erro)> CriarAsync(CriarSolicitacaoRequest request)
    {
        if (!await db.Usuarios.AnyAsync(x => x.Id == request.UsuarioId))
            return (null, "O usuário informado não existe.");
        if (request.ContatoConfiancaId is int contatoId &&
            !await db.ContatosConfianca.AnyAsync(x => x.Id == contatoId && x.UsuarioId == request.UsuarioId && x.Ativo))
            return (null, "O contato não existe, não pertence ao usuário ou está inativo.");

        var item = new SolicitacaoAjuda
        {
            UsuarioId = request.UsuarioId,
            ContatoConfiancaId = request.ContatoConfiancaId,
            Motivo = request.Motivo.Trim()
        };
        db.SolicitacoesAjuda.Add(item);
        await db.SaveChangesAsync();
        return (item, null);
    }

    public async Task<(SolicitacaoAjuda? Item, string? Erro)> AtualizarStatusAsync(int id, string status)
    {
        var item = await db.SolicitacoesAjuda.FindAsync(id);
        if (item is null) return (null, "Solicitação não encontrada.");
        if (!StatusPermitidos.Contains(status, StringComparer.OrdinalIgnoreCase))
            return (null, "Status inválido. Use Pendente, EmAtendimento, Concluida ou Cancelada.");
        if (item.Status is "Concluida" or "Cancelada")
            return (null, "Uma solicitação finalizada não pode ser alterada.");

        item.Status = StatusPermitidos.First(x => x.Equals(status, StringComparison.OrdinalIgnoreCase));
        item.FinalizadaEm = item.Status is "Concluida" or "Cancelada" ? DateTime.UtcNow : null;
        await db.SaveChangesAsync();
        return (item, null);
    }
}
