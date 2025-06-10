using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class AddingTimeTaken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeTakenForQuestion",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TotalTimeTaken",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "TimeTaken",
                table: "UserAnswer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeTaken",
                table: "UserAnswer");

            migrationBuilder.AddColumn<int>(
                name: "TimeTakenForQuestion",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalTimeTaken",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
