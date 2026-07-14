using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class CascadedeleteMatriculawhenTurmadeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBMatricula_TBTurma_TurmaId",
                table: "TBMatricula");

            migrationBuilder.AddForeignKey(
                name: "FK_TBMatricula_TBTurma_TurmaId",
                table: "TBMatricula",
                column: "TurmaId",
                principalTable: "TBTurma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBMatricula_TBTurma_TurmaId",
                table: "TBMatricula");

            migrationBuilder.AddForeignKey(
                name: "FK_TBMatricula_TBTurma_TurmaId",
                table: "TBMatricula",
                column: "TurmaId",
                principalTable: "TBTurma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
