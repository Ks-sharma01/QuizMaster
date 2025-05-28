using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class deleteIsAttempted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttemptedQuestions_Answers_AnswerId",
                table: "AttemptedQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_AttemptedQuestions_QuizAttempts_AttemptId",
                table: "AttemptedQuestions");

            migrationBuilder.DropTable(
                name: "QuizAttempts");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "AttemptedQuestions");

            migrationBuilder.RenameColumn(
                name: "AttemptId",
                table: "AttemptedQuestions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AnsweredAt",
                table: "AttemptedQuestions",
                newName: "AttemptedOn");

            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "AttemptedQuestions",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AttemptedQuestions_AttemptId",
                table: "AttemptedQuestions",
                newName: "IX_AttemptedQuestions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AttemptedQuestions_AnswerId",
                table: "AttemptedQuestions",
                newName: "IX_AttemptedQuestions_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttemptedQuestions_Category_CategoryId",
                table: "AttemptedQuestions",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttemptedQuestions_Users_UserId",
                table: "AttemptedQuestions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttemptedQuestions_Category_CategoryId",
                table: "AttemptedQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_AttemptedQuestions_Users_UserId",
                table: "AttemptedQuestions");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "AttemptedQuestions",
                newName: "AttemptId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "AttemptedQuestions",
                newName: "AnswerId");

            migrationBuilder.RenameColumn(
                name: "AttemptedOn",
                table: "AttemptedQuestions",
                newName: "AnsweredAt");

            migrationBuilder.RenameIndex(
                name: "IX_AttemptedQuestions_UserId",
                table: "AttemptedQuestions",
                newName: "IX_AttemptedQuestions_AttemptId");

            migrationBuilder.RenameIndex(
                name: "IX_AttemptedQuestions_CategoryId",
                table: "AttemptedQuestions",
                newName: "IX_AttemptedQuestions_AnswerId");

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "AttemptedQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "QuizAttempts",
                columns: table => new
                {
                    AttemptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempts", x => x.AttemptId);
                    table.ForeignKey(
                        name: "FK_QuizAttempts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_UserId",
                table: "QuizAttempts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttemptedQuestions_Answers_AnswerId",
                table: "AttemptedQuestions",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "AnswerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttemptedQuestions_QuizAttempts_AttemptId",
                table: "AttemptedQuestions",
                column: "AttemptId",
                principalTable: "QuizAttempts",
                principalColumn: "AttemptId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
