using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillBuddy.API.Migrations
{
    /// <inheritdoc />
    public partial class secndmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Groups_GroupId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_GroupId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_GroupId",
                table: "Transactions",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Groups_GroupId",
                table: "Transactions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
