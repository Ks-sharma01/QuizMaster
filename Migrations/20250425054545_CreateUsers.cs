using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class CreateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Answers_SelectedAnswerId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Category_CategoryId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_QuizAttempt_QuizAttemptId",
                table: "UserAnswers");

            migrationBuilder.DropTable(
                name: "QuizAttempt");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_QuizAttemptId",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "QuizAttemptId",
                table: "UserAnswers");

            migrationBuilder.RenameTable(
                name: "UserAnswers",
                newName: "UserAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_SelectedAnswerId",
                table: "UserAnswer",
                newName: "IX_UserAnswer_SelectedAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswer",
                newName: "IX_UserAnswer_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_CategoryId",
                table: "UserAnswer",
                newName: "IX_UserAnswer_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "AdminUserId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswer",
                table: "UserAnswer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AdminUserId",
                table: "Users",
                column: "AdminUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Answers_SelectedAnswerId",
                table: "UserAnswer",
                column: "SelectedAnswerId",
                principalTable: "Answers",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Category_CategoryId",
                table: "UserAnswer",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Questions_QuestionId",
                table: "UserAnswer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_IdentityUser_AdminUserId",
                table: "Users",
                column: "AdminUserId",
                principalTable: "IdentityUser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Answers_SelectedAnswerId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Category_CategoryId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Questions_QuestionId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_IdentityUser_AdminUserId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropIndex(
                name: "IX_Users_AdminUserId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswer",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "AdminUserId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "UserAnswer",
                newName: "UserAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswer_SelectedAnswerId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_SelectedAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswer_QuestionId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswer_CategoryId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "QuizAttemptId",
                table: "UserAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "QuizAttempt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttemptedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempt", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuizAttemptId",
                table: "UserAnswers",
                column: "QuizAttemptId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Answers_SelectedAnswerId",
                table: "UserAnswers",
                column: "SelectedAnswerId",
                principalTable: "Answers",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Category_CategoryId",
                table: "UserAnswers",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId",
                table: "UserAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_QuizAttempt_QuizAttemptId",
                table: "UserAnswers",
                column: "QuizAttemptId",
                principalTable: "QuizAttempt",
                principalColumn: "Id");
        }
    }
}
