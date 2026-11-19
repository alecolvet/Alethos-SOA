using Microsoft.EntityFrameworkCore;
using ProtegePlus.Api.Data;
using ProtegePlus.Api.Domain;
using ProtegePlus.Api.DTOs;

namespace ProtegePlus.Api.Services;

public interface IOcorrenciaService
{
    Task<(Ocorrencia? Item, string? Erro)> CriarAsync(CriarOcorrenciaRequest request);
}

public class OcorrenciaService(ProtegePlusDbContext db) : IOcorrenciaService
{
    public async Task<(Ocorrencia? Item, string? Erro)> CriarAsync(CriarOcorrenciaRequest request)
    {
        if (!await db.Usuarios.AnyAsync(x => x.Id == request.UsuarioId))
            return (null, "O usuário informado não existe.");
        var classificacoes = new[] { "Suspeito", "AparentementeSeguro", "Inconclusivo" };
        if (!classificacoes.Contains(request.Classificacao, StringComparer.OrdinalIgnoreCase))
            return (null, "Classificação inválida. Use Suspeito, AparentementeSeguro ou Inconclusivo.");

        var item = new Ocorrencia
        {
            UsuarioId = request.UsuarioId,
            MensagemRecebida = request.MensagemRecebida.Trim(),
            Canal = request.Canal.Trim(),
            Classificacao = request.Classificacao,
            NivelRisco = request.NivelRisco,
            Justificativa = request.Justificativa.Trim()
        };
        db.Ocorrencias.Add(item);
        await db.SaveChangesAsync();
        return (item, null);
    }
}
