namespace ProtegePlus.Api.Domain;

public class ContatoConfianca
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Parentesco { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
}
