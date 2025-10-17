using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Add_EstadisticasUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadisticasUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    TotalTareasCompletadas = table.Column<int>(type: "int", nullable: false),
                    TotalHabitosBuenos = table.Column<int>(type: "int", nullable: false),
                    TotalHabitosMalos = table.Column<int>(type: "int", nullable: false),
                    UltimaConexion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    ExperienciaTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadisticasUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadisticasUsuarios_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadisticasUsuarios_IdUsuario",
                table: "EstadisticasUsuarios",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstadisticasUsuarios");
        }
    }
}
