using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class attempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttemptedQuestions",
                table: "AttemptedQuestions");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "AttemptedQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "AttemptedQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AttemptId",
                table: "AttemptedQuestions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AttemptedQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttemptedQuestions",
                table: "AttemptedQuestions",
                column: "Id");

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
                name: "IX_UserAnswer_UserId",
                table: "UserAnswer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptedQuestions_AnswerId",
                table: "AttemptedQuestions",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptedQuestions_AttemptId",
                table: "AttemptedQuestions",
                column: "AttemptId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Users_UserId",
                table: "UserAnswer",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttemptedQuestions_Answers_AnswerId",
                table: "AttemptedQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_AttemptedQuestions_QuizAttempts_AttemptId",
                table: "AttemptedQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Users_UserId",
                table: "UserAnswer");

            migrationBuilder.DropTable(
                name: "QuizAttempts");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswer_UserId",
                table: "UserAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttemptedQuestions",
                table: "AttemptedQuestions");

            migrationBuilder.DropIndex(
                name: "IX_AttemptedQuestions_AnswerId",
                table: "AttemptedQuestions");

            migrationBuilder.DropIndex(
                name: "IX_AttemptedQuestions_AttemptId",
                table: "AttemptedQuestions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AttemptedQuestions");

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrect",
                table: "AttemptedQuestions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "AttemptId",
                table: "AttemptedQuestions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "AttemptedQuestions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttemptedQuestions",
                table: "AttemptedQuestions",
                column: "AttemptId");
        }
    }
}
