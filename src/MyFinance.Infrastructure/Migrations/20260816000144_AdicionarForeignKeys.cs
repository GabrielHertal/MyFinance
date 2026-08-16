using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFinance.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_ParcelaId",
                table: "Transacoes",
                column: "ParcelaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cartao_UsuarioId",
                table: "Cartao",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cartao_AspNetUsers_UsuarioId",
                table: "Cartao",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Parcelamento_ParcelaId",
                table: "Transacoes",
                column: "ParcelaId",
                principalTable: "Parcelamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cartao_AspNetUsers_UsuarioId",
                table: "Cartao");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Parcelamento_ParcelaId",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_ParcelaId",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Cartao_UsuarioId",
                table: "Cartao");
        }
    }
}
