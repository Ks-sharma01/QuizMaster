using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class Retrivingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "Questions");
        }
    }
}
