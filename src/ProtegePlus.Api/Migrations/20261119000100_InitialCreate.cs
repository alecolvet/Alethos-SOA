using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProtegePlus.Api.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Usuarios",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                Nome = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Email = table.Column<string>(type: "TEXT", maxLength: 180, nullable: false),
                CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
            }, constraints: table => table.PrimaryKey("PK_Usuarios", x => x.Id));

        migrationBuilder.CreateTable(
            name: "ContatosConfianca",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                Nome = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Telefone = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                Parentesco = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
            }, constraints: table =>
            {
                table.PrimaryKey("PK_ContatosConfianca", x => x.Id);
                table.ForeignKey("FK_ContatosConfianca_Usuarios_UsuarioId", x => x.UsuarioId, "Usuarios", "Id", onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Ocorrencias",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                MensagemRecebida = table.Column<string>(type: "TEXT", maxLength: 4000, nullable: false),
                Canal = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false),
                Classificacao = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                NivelRisco = table.Column<int>(type: "INTEGER", nullable: false),
                Justificativa = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                RegistradaEm = table.Column<DateTime>(type: "TEXT", nullable: false)
            }, constraints: table =>
            {
                table.PrimaryKey("PK_Ocorrencias", x => x.Id);
                table.ForeignKey("FK_Ocorrencias_Usuarios_UsuarioId", x => x.UsuarioId, "Usuarios", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "SolicitacoesAjuda",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false).Annotation("Sqlite:Autoincrement", true),
                UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                ContatoConfiancaId = table.Column<int>(type: "INTEGER", nullable: true),
                Motivo = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                CriadaEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                FinalizadaEm = table.Column<DateTime>(type: "TEXT", nullable: true)
            }, constraints: table =>
            {
                table.PrimaryKey("PK_SolicitacoesAjuda", x => x.Id);
                table.ForeignKey("FK_SolicitacoesAjuda_Usuarios_UsuarioId", x => x.UsuarioId, "Usuarios", "Id", onDelete: ReferentialAction.Restrict);
                table.ForeignKey("FK_SolicitacoesAjuda_ContatosConfianca_ContatoConfiancaId", x => x.ContatoConfiancaId, "ContatosConfianca", "Id", onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateIndex("IX_Usuarios_Email", "Usuarios", "Email", unique: true);
        migrationBuilder.CreateIndex("IX_ContatosConfianca_UsuarioId_Telefone", "ContatosConfianca", new[] { "UsuarioId", "Telefone" }, unique: true);
        migrationBuilder.CreateIndex("IX_Ocorrencias_UsuarioId", "Ocorrencias", "UsuarioId");
        migrationBuilder.CreateIndex("IX_SolicitacoesAjuda_UsuarioId", "SolicitacoesAjuda", "UsuarioId");
        migrationBuilder.CreateIndex("IX_SolicitacoesAjuda_ContatoConfiancaId", "SolicitacoesAjuda", "ContatoConfiancaId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("SolicitacoesAjuda");
        migrationBuilder.DropTable("Ocorrencias");
        migrationBuilder.DropTable("ContatosConfianca");
        migrationBuilder.DropTable("Usuarios");
    }
}
