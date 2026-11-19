namespace ProtegePlus.Api.Domain;

public class Ocorrencia
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public string MensagemRecebida { get; set; } = string.Empty;
    public string Canal { get; set; } = "WhatsApp";
    public string Classificacao { get; set; } = "Inconclusivo";
    public int NivelRisco { get; set; }
    public string Justificativa { get; set; } = string.Empty;
    public DateTime RegistradaEm { get; set; } = DateTime.UtcNow;
}
