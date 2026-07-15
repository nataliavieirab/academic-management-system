using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class AddIsRequiredtoCursoIdandInstrutorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove turmas incompletas antes de tornar CursoId/InstrutorId obrigatórios.
            // O EF preencheria NULLs com Guid.Empty, o que viola as FKs.
            migrationBuilder.Sql("""
                DELETE FROM [TBMatricula]
                WHERE [TurmaId] IN (
                    SELECT [Id] FROM [TBTurma]
                    WHERE [CursoId] IS NULL OR [InstrutorId] IS NULL
                );

                DELETE FROM [TBTurma]
                WHERE [CursoId] IS NULL OR [InstrutorId] IS NULL;
                """);

            migrationBuilder.AlterColumn<Guid>(
                name: "InstrutorId",
                table: "TBTurma",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CursoId",
                table: "TBTurma",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "InstrutorId",
                table: "TBTurma",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CursoId",
                table: "TBTurma",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
