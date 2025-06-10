using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class RemovingTimeTaken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeTaken",
                table: "UserAnswer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeTaken",
                table: "UserAnswer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
