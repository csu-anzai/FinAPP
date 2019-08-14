using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedNewConfigToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PasswordConfirmationCodes_PasswordConfirmationCodeId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tokens_TokenId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PasswordConfirmationCodeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TokenId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordConfirmationCodeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tokens",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PasswordConfirmationCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PasswordConfirmationCodes_UserId",
                table: "PasswordConfirmationCodes",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PasswordConfirmationCodes_Users_UserId",
                table: "PasswordConfirmationCodes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasswordConfirmationCodes_Users_UserId",
                table: "PasswordConfirmationCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_PasswordConfirmationCodes_UserId",
                table: "PasswordConfirmationCodes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tokens");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PasswordConfirmationCodes");

            migrationBuilder.AddColumn<int>(
                name: "PasswordConfirmationCodeId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TokenId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PasswordConfirmationCodeId",
                table: "Users",
                column: "PasswordConfirmationCodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_TokenId",
                table: "Users",
                column: "TokenId",
                unique: true,
                filter: "[TokenId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PasswordConfirmationCodes_PasswordConfirmationCodeId",
                table: "Users",
                column: "PasswordConfirmationCodeId",
                principalTable: "PasswordConfirmationCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tokens_TokenId",
                table: "Users",
                column: "TokenId",
                principalTable: "Tokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
