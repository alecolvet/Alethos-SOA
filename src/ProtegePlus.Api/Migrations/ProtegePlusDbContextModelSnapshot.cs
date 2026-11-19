using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProtegePlus.Api.Data;

#nullable disable

namespace ProtegePlus.Api.Migrations;

[DbContext(typeof(ProtegePlusDbContext))]
partial class ProtegePlusDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "10.0.0");
        modelBuilder.HasAnnotation("Relational:MaxIdentifierLength", 64);
        modelBuilder.Entity("ProtegePlus.Api.Domain.Usuario", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER").HasAnnotation("Sqlite:Autoincrement", true);
            b.Property<string>("Email").IsRequired().HasMaxLength(180).HasColumnType("TEXT");
            b.Property<string>("Nome").IsRequired().HasMaxLength(120).HasColumnType("TEXT");
            b.Property<DateTime>("CriadoEm").HasColumnType("TEXT");
            b.HasKey("Id"); b.HasIndex("Email").IsUnique(); b.ToTable("Usuarios");
        });
        modelBuilder.Entity("ProtegePlus.Api.Domain.ContatoConfianca", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER").HasAnnotation("Sqlite:Autoincrement", true);
            b.Property<bool>("Ativo").HasColumnType("INTEGER");
            b.Property<string>("Nome").IsRequired().HasMaxLength(120).HasColumnType("TEXT");
            b.Property<string>("Parentesco").IsRequired().HasMaxLength(60).HasColumnType("TEXT");
            b.Property<string>("Telefone").IsRequired().HasMaxLength(25).HasColumnType("TEXT");
            b.Property<int>("UsuarioId").HasColumnType("INTEGER"); b.HasKey("Id");
            b.HasIndex("UsuarioId", "Telefone").IsUnique(); b.ToTable("ContatosConfianca");
        });
        modelBuilder.Entity("ProtegePlus.Api.Domain.Ocorrencia", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER").HasAnnotation("Sqlite:Autoincrement", true);
            b.Property<string>("Canal").IsRequired().HasMaxLength(40).HasColumnType("TEXT");
            b.Property<string>("Classificacao").IsRequired().HasMaxLength(30).HasColumnType("TEXT");
            b.Property<string>("Justificativa").IsRequired().HasMaxLength(1000).HasColumnType("TEXT");
            b.Property<string>("MensagemRecebida").IsRequired().HasMaxLength(4000).HasColumnType("TEXT");
            b.Property<int>("NivelRisco").HasColumnType("INTEGER"); b.Property<DateTime>("RegistradaEm").HasColumnType("TEXT");
            b.Property<int>("UsuarioId").HasColumnType("INTEGER"); b.HasKey("Id"); b.HasIndex("UsuarioId"); b.ToTable("Ocorrencias");
        });
        modelBuilder.Entity("ProtegePlus.Api.Domain.SolicitacaoAjuda", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("INTEGER").HasAnnotation("Sqlite:Autoincrement", true);
            b.Property<int?>("ContatoConfiancaId").HasColumnType("INTEGER");
            b.Property<DateTime?>("FinalizadaEm").HasColumnType("TEXT");
            b.Property<DateTime>("CriadaEm").HasColumnType("TEXT");
            b.Property<string>("Motivo").IsRequired().HasMaxLength(500).HasColumnType("TEXT");
            b.Property<string>("Status").IsRequired().HasMaxLength(20).HasColumnType("TEXT");
            b.Property<int>("UsuarioId").HasColumnType("INTEGER"); b.HasKey("Id");
            b.HasIndex("ContatoConfiancaId"); b.HasIndex("UsuarioId"); b.ToTable("SolicitacoesAjuda");
        });
        modelBuilder.Entity("ProtegePlus.Api.Domain.ContatoConfianca", b =>
            b.HasOne("ProtegePlus.Api.Domain.Usuario", "Usuario").WithMany("Contatos").HasForeignKey("UsuarioId").OnDelete(DeleteBehavior.Cascade).IsRequired());
        modelBuilder.Entity("ProtegePlus.Api.Domain.Ocorrencia", b =>
            b.HasOne("ProtegePlus.Api.Domain.Usuario", "Usuario").WithMany("Ocorrencias").HasForeignKey("UsuarioId").OnDelete(DeleteBehavior.Restrict).IsRequired());
        modelBuilder.Entity("ProtegePlus.Api.Domain.SolicitacaoAjuda", b =>
        {
            b.HasOne("ProtegePlus.Api.Domain.ContatoConfianca", "ContatoConfianca").WithMany().HasForeignKey("ContatoConfiancaId").OnDelete(DeleteBehavior.SetNull);
            b.HasOne("ProtegePlus.Api.Domain.Usuario", "Usuario").WithMany("Solicitacoes").HasForeignKey("UsuarioId").OnDelete(DeleteBehavior.Restrict).IsRequired();
        });
        modelBuilder.Entity("ProtegePlus.Api.Domain.ContatoConfianca", b => b.Navigation("Usuario"));
        modelBuilder.Entity("ProtegePlus.Api.Domain.Ocorrencia", b => b.Navigation("Usuario"));
        modelBuilder.Entity("ProtegePlus.Api.Domain.SolicitacaoAjuda", b => { b.Navigation("ContatoConfianca"); b.Navigation("Usuario"); });
        modelBuilder.Entity("ProtegePlus.Api.Domain.Usuario", b => { b.Navigation("Contatos"); b.Navigation("Ocorrencias"); b.Navigation("Solicitacoes"); });
    }
}
