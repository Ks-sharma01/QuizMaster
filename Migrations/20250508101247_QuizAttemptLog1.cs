using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class QuizAttemptLog1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttemptedQuestions",
                table: "AttemptedQuestions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AttemptedQuestions");

            migrationBuilder.AlterColumn<int>(
                name: "AttemptId",
                table: "AttemptedQuestions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttemptedQuestions",
                table: "AttemptedQuestions",
                column: "AttemptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AttemptedQuestions",
                table: "AttemptedQuestions");

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
        }
    }
}
