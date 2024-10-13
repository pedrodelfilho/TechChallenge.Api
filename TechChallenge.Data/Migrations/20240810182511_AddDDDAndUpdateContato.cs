using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallenge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDDDAndUpdateContato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DDDId",
                table: "Contato",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<byte>(
                name: "Ddd",
                table: "Contato",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "DDD",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrDDD = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DDD", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contato_DDDId",
                table: "Contato",
                column: "DDDId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_DDD_DDDId",
                table: "Contato",
                column: "DDDId",
                principalTable: "DDD",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato_DDD_DDDId",
                table: "Contato");

            migrationBuilder.DropTable(
                name: "DDD");

            migrationBuilder.DropIndex(
                name: "IX_Contato_DDDId",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "DDDId",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "Ddd",
                table: "Contato");
        }
    }
}
