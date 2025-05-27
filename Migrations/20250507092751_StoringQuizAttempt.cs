using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class StoringQuizAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionAnswerDto",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

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
                });

            migrationBuilder.CreateTable(
                name: "AttemptedQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttemptId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true),
                    AnsweredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuizAttemptAttemptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttemptedQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttemptedQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttemptedQuestions_QuizAttempts_QuizAttemptAttemptId",
                        column: x => x.QuizAttemptAttemptId,
                        principalTable: "QuizAttempts",
                        principalColumn: "AttemptId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttemptedQuestions_QuestionId",
                table: "AttemptedQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptedQuestions_QuizAttemptAttemptId",
                table: "AttemptedQuestions",
                column: "QuizAttemptAttemptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttemptedQuestions");

            migrationBuilder.DropTable(
                name: "QuestionAnswerDto");

            migrationBuilder.DropTable(
                name: "QuizAttempts");
        }
    }
}
