using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Transportadora.Migrations
{
    /// <inheritdoc />
    public partial class Autobuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoraInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EnUso = table.Column<bool>(type: "boolean", nullable: false),
                    CiclosDeCarga = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EsHorarioPico = table.Column<bool>(type: "boolean", nullable: false),
                    BusesEnOperacion = table.Column<int>(type: "integer", nullable: false),
                    CargadoresEnUso = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Autobuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Placa = table.Column<string>(type: "text", nullable: false),
                    EnOperacion = table.Column<bool>(type: "boolean", nullable: false),
                    TiempoUltimoMantenimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HorasEnOperacion = table.Column<int>(type: "integer", nullable: false),
                    CargadorId = table.Column<int>(type: "integer", nullable: false),
                    EnUso = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autobuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Autobuses_Cargadores_CargadorId",
                        column: x => x.CargadorId,
                        principalTable: "Cargadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsosAutobus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoraInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AutobusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsosAutobus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsosAutobus_Autobuses_AutobusId",
                        column: x => x.AutobusId,
                        principalTable: "Autobuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autobuses_CargadorId",
                table: "Autobuses",
                column: "CargadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Autobuses_Id",
                table: "Autobuses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cargadores_Id",
                table: "Cargadores",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_Id",
                table: "Horarios",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsosAutobus_AutobusId",
                table: "UsosAutobus",
                column: "AutobusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "UsosAutobus");

            migrationBuilder.DropTable(
                name: "Autobuses");

            migrationBuilder.DropTable(
                name: "Cargadores");
        }
    }
}
