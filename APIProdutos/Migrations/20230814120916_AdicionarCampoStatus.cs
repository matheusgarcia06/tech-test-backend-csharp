using Microsoft.EntityFrameworkCore.Migrations;
using TechTestBackendCSharp.Enums;

#nullable disable

namespace TechTestBackendCSharp.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCampoStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: (int)StatusProduto.Ativo);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Produtos");
        }
    }
}
