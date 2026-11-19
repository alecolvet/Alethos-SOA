using Microsoft.EntityFrameworkCore;
using ProtegePlus.Api.Domain;

namespace ProtegePlus.Api.Data;

public class ProtegePlusDbContext(DbContextOptions<ProtegePlusDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<ContatoConfianca> ContatosConfianca => Set<ContatoConfianca>();
    public DbSet<Ocorrencia> Ocorrencias => Set<Ocorrencia>();
    public DbSet<SolicitacaoAjuda> SolicitacoesAjuda => Set<SolicitacaoAjuda>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("Usuarios");
            e.HasKey(x => x.Id);
            e.Property(x => x.Nome).HasMaxLength(120).IsRequired();
            e.Property(x => x.Email).HasMaxLength(180).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();
        });
        modelBuilder.Entity<ContatoConfianca>(e =>
        {
            e.ToTable("ContatosConfianca");
            e.HasKey(x => x.Id);
            e.Property(x => x.Nome).HasMaxLength(120).IsRequired();
            e.Property(x => x.Telefone).HasMaxLength(25).IsRequired();
            e.Property(x => x.Parentesco).HasMaxLength(60).IsRequired();
            e.HasOne(x => x.Usuario).WithMany(x => x.Contatos).HasForeignKey(x => x.UsuarioId).OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(x => new { x.UsuarioId, x.Telefone }).IsUnique();
        });
        modelBuilder.Entity<Ocorrencia>(e =>
        {
            e.ToTable("Ocorrencias");
            e.HasKey(x => x.Id);
            e.Property(x => x.MensagemRecebida).HasMaxLength(4000).IsRequired();
            e.Property(x => x.Canal).HasMaxLength(40).IsRequired();
            e.Property(x => x.Classificacao).HasMaxLength(30).IsRequired();
            e.Property(x => x.Justificativa).HasMaxLength(1000).IsRequired();
            e.HasOne(x => x.Usuario).WithMany(x => x.Ocorrencias).HasForeignKey(x => x.UsuarioId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<SolicitacaoAjuda>(e =>
        {
            e.ToTable("SolicitacoesAjuda");
            e.HasKey(x => x.Id);
            e.Property(x => x.Motivo).HasMaxLength(500).IsRequired();
            e.Property(x => x.Status).HasMaxLength(20).IsRequired();
            e.HasOne(x => x.Usuario).WithMany(x => x.Solicitacoes).HasForeignKey(x => x.UsuarioId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.ContatoConfianca).WithMany().HasForeignKey(x => x.ContatoConfiancaId).OnDelete(DeleteBehavior.SetNull);
        });
    }
}
