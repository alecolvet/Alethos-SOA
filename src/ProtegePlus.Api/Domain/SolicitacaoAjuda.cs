namespace ProtegePlus.Api.Domain;

public class SolicitacaoAjuda
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public int? ContatoConfiancaId { get; set; }
    public ContatoConfianca? ContatoConfianca { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string Status { get; set; } = "Pendente";
    public DateTime CriadaEm { get; set; } = DateTime.UtcNow;
    public DateTime? FinalizadaEm { get; set; }
}
