using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BillBuddy.API.Migrations
{
    /// <inheritdoc />
    public partial class thirdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Transactions_TransactionId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_UserDetailsId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_PaidById",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Participants_TransactionId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_UserDetailsId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transactions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PaidById",
                table: "Transactions",
                newName: "PaidByUserId");

            migrationBuilder.RenameColumn(
                name: "DateTIme",
                table: "Transactions",
                newName: "TransactionDateTIme");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_PaidById",
                table: "Transactions",
                newName: "IX_Transactions_PaidByUserId");

            migrationBuilder.RenameColumn(
                name: "UserDetailsId",
                table: "Participants",
                newName: "SettlementStatus");

            migrationBuilder.RenameColumn(
                name: "ContributionAmount",
                table: "Participants",
                newName: "SplitAmount");

            migrationBuilder.AddColumn<string>(
                name: "Auth0Identifier",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PublicIndentifier",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDateTime",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PublicIndentifier",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SplitDateTIme",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "Participants",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BalanceAmount",
                table: "Participants",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPaidDate",
                table: "Participants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ParticipantUserId",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PublicIdentifier",
                table: "Participants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SplitTransactionParticipantIdentifier",
                table: "Participants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SplitTransactionid",
                table: "Participants",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_ParticipantUserId",
                table: "Participants",
                column: "ParticipantUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_SplitTransactionid",
                table: "Participants",
                column: "SplitTransactionid");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Transactions_SplitTransactionid",
                table: "Participants",
                column: "SplitTransactionid",
                principalTable: "Transactions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Users_ParticipantUserId",
                table: "Participants",
                column: "ParticipantUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_PaidByUserId",
                table: "Transactions",
                column: "PaidByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Transactions_SplitTransactionid",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_ParticipantUserId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_PaidByUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Participants_ParticipantUserId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_SplitTransactionid",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Auth0Identifier",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PublicIndentifier",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DueDateTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PublicIndentifier",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SplitDateTIme",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "BalanceAmount",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "LastPaidDate",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "ParticipantUserId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "PublicIdentifier",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "SplitTransactionParticipantIdentifier",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "SplitTransactionid",
                table: "Participants");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Transactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TransactionDateTIme",
                table: "Transactions",
                newName: "DateTIme");

            migrationBuilder.RenameColumn(
                name: "PaidByUserId",
                table: "Transactions",
                newName: "PaidById");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_PaidByUserId",
                table: "Transactions",
                newName: "IX_Transactions_PaidById");

            migrationBuilder.RenameColumn(
                name: "SplitAmount",
                table: "Participants",
                newName: "ContributionAmount");

            migrationBuilder.RenameColumn(
                name: "SettlementStatus",
                table: "Participants",
                newName: "UserDetailsId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Participants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_TransactionId",
                table: "Participants",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserDetailsId",
                table: "Participants",
                column: "UserDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Transactions_TransactionId",
                table: "Participants",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Users_UserDetailsId",
                table: "Participants",
                column: "UserDetailsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_PaidById",
                table: "Transactions",
                column: "PaidById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
