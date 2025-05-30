using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feelfood.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Jobs_JobId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_JobId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_JobId",
                table: "Orders",
                column: "JobId",
                unique: true,
                filter: "[JobId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Jobs_JobId",
                table: "Orders",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Jobs_JobId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_JobId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_JobId",
                table: "Orders",
                column: "JobId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Jobs_JobId",
                table: "Orders",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
