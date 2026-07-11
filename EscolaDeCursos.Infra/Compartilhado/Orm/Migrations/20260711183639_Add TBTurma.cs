using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaDeCursos.Infra.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class AddTBTurma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBTurma",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InstrutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CapacidadeMaxima = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataTermino = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTurma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBTurma_TBInstrutor_InstrutorId",
                        column: x => x.InstrutorId,
                        principalTable: "TBInstrutor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBTurmaAluno",
                columns: table => new
                {
                    AlunosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TurmaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTurmaAluno", x => new { x.AlunosId, x.TurmaId });
                    table.ForeignKey(
                        name: "FK_TBTurmaAluno_TBAluno_AlunosId",
                        column: x => x.AlunosId,
                        principalTable: "TBAluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBTurmaAluno_TBTurma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "TBTurma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBTurma_InstrutorId",
                table: "TBTurma",
                column: "InstrutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TBTurmaAluno_TurmaId",
                table: "TBTurmaAluno",
                column: "TurmaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBTurmaAluno");

            migrationBuilder.DropTable(
                name: "TBTurma");
        }
    }
}
