using System.ComponentModel.DataAnnotations;

namespace ProtegePlus.Api.DTOs;

public record CriarUsuarioRequest([Required, StringLength(120, MinimumLength = 2)] string Nome,
    [Required, EmailAddress, StringLength(180)] string Email);

public record CriarContatoRequest([Range(1, int.MaxValue)] int UsuarioId,
    [Required, StringLength(120, MinimumLength = 2)] string Nome,
    [Required, StringLength(25, MinimumLength = 8)] string Telefone,
    [Required, StringLength(60)] string Parentesco);

public record CriarOcorrenciaRequest([Range(1, int.MaxValue)] int UsuarioId,
    [Required, StringLength(4000, MinimumLength = 5)] string MensagemRecebida,
    [Required, StringLength(40)] string Canal,
    [Required] string Classificacao,
    [Range(0, 100)] int NivelRisco,
    [Required, StringLength(1000, MinimumLength = 5)] string Justificativa);

public record CriarSolicitacaoRequest([Range(1, int.MaxValue)] int UsuarioId,
    int? ContatoConfiancaId,
    [Required, StringLength(500, MinimumLength = 5)] string Motivo);

public record AtualizarStatusRequest([Required] string Status);
