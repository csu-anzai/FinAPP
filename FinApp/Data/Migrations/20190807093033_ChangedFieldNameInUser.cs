using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangedFieldNameInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ConfirmationCodes_ConfirmationCodeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ConfirmationCodes");

            migrationBuilder.DropIndex(
                name: "IX_Users_ConfirmationCodeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConfirmationCodeId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "PasswordConfirmationCodeId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PasswordConfirmationCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordConfirmationCodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PasswordConfirmationCodeId",
                table: "Users",
                column: "PasswordConfirmationCodeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PasswordConfirmationCodes_PasswordConfirmationCodeId",
                table: "Users",
                column: "PasswordConfirmationCodeId",
                principalTable: "PasswordConfirmationCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PasswordConfirmationCodes_PasswordConfirmationCodeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "PasswordConfirmationCodes");

            migrationBuilder.DropIndex(
                name: "IX_Users_PasswordConfirmationCodeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordConfirmationCodeId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmationCodeId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConfirmationCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmationCodes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ConfirmationCodeId",
                table: "Users",
                column: "ConfirmationCodeId",
                unique: true,
                filter: "[ConfirmationCodeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ConfirmationCodes_ConfirmationCodeId",
                table: "Users",
                column: "ConfirmationCodeId",
                principalTable: "ConfirmationCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
