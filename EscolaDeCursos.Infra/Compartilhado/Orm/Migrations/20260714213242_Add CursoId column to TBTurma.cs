using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class AddCursoIdcolumntoTBTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CursoId",
                table: "TBTurma",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBTurma_CursoId",
                table: "TBTurma",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TBTurma_TBCurso_CursoId",
                table: "TBTurma",
                column: "CursoId",
                principalTable: "TBCurso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBTurma_TBCurso_CursoId",
                table: "TBTurma");

            migrationBuilder.DropIndex(
                name: "IX_TBTurma_CursoId",
                table: "TBTurma");

            migrationBuilder.DropColumn(
                name: "CursoId",
                table: "TBTurma");
        }
    }
}
