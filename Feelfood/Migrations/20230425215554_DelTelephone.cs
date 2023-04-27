using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feelfood.Migrations
{
    /// <inheritdoc />
    public partial class DelTelephone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                nullable: true);
        }
    }
}
