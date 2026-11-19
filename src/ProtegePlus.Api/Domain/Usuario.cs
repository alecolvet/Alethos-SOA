namespace ProtegePlus.Api.Domain;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public ICollection<ContatoConfianca> Contatos { get; set; } = new List<ContatoConfianca>();
    public ICollection<Ocorrencia> Ocorrencias { get; set; } = new List<Ocorrencia>();
    public ICollection<SolicitacaoAjuda> Solicitacoes { get; set; } = new List<SolicitacaoAjuda>();
}
