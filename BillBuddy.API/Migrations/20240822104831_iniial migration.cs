using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillBuddy.API.Migrations
{
    /// <inheritdoc />
    public partial class iniialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SplitTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDateTIme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SplitDateTIme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantPublicIdentifiers = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SplitTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Auth0Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "SplitTransactionParticipants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SplitTransactionParticipantIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SplitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastPaidDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SettlementStatus = table.Column<int>(type: "int", nullable: false),
                    ParticipantUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SplitTransactionParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SplitTransactionParticipants_Users_ParticipantUserId",
                        column: x => x.ParticipantUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SplitTransactionParticipants_ParticipantUserId",
                table: "SplitTransactionParticipants",
                column: "ParticipantUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SplitTransactionParticipants");

            migrationBuilder.DropTable(
                name: "SplitTransactions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
