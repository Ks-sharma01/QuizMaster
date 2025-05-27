using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class QuizAttemptLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttemptedQuestions_QuizAttempts_QuizAttemptAttemptId",
                table: "AttemptedQuestions");

            migrationBuilder.DropTable(
                name: "QuizAttempts");

            migrationBuilder.DropIndex(
                name: "IX_AttemptedQuestions_QuizAttemptAttemptId",
                table: "AttemptedQuestions");

            migrationBuilder.DropColumn(
                name: "QuizAttemptAttemptId",
                table: "AttemptedQuestions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizAttemptAttemptId",
                table: "AttemptedQuestions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuizAttempts",
                columns: table => new
                {
                    AttemptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempts", x => x.AttemptId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttemptedQuestions_QuizAttemptAttemptId",
                table: "AttemptedQuestions",
                column: "QuizAttemptAttemptId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttemptedQuestions_QuizAttempts_QuizAttemptAttemptId",
                table: "AttemptedQuestions",
                column: "QuizAttemptAttemptId",
                principalTable: "QuizAttempts",
                principalColumn: "AttemptId");
        }
    }
}
