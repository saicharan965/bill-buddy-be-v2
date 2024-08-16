using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillBuddy.API.Migrations
{
    /// <inheritdoc />
    public partial class addedmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicIndentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "SplitTransactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicIndentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionDateTIme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SplitDateTIme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SplitTransactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_SplitTransactions_Users_PaidByUserId",
                        column: x => x.PaidByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                    ParticipantUserId = table.Column<int>(type: "int", nullable: false),
                    SplitTransactionid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SplitTransactionParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SplitTransactionParticipants_SplitTransactions_SplitTransactionid",
                        column: x => x.SplitTransactionid,
                        principalTable: "SplitTransactions",
                        principalColumn: "id");
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

            migrationBuilder.CreateIndex(
                name: "IX_SplitTransactionParticipants_SplitTransactionid",
                table: "SplitTransactionParticipants",
                column: "SplitTransactionid");

            migrationBuilder.CreateIndex(
                name: "IX_SplitTransactions_PaidByUserId",
                table: "SplitTransactions",
                column: "PaidByUserId");
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
