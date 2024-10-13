using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechChallenge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDDDAndUpdateContato_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ddd",
                table: "Contato");

            migrationBuilder.AlterColumn<byte>(
                name: "NrDDD",
                table: "DDD",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NrDDD",
                table: "DDD",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<byte>(
                name: "Ddd",
                table: "Contato",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
