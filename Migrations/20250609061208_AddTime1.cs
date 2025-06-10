using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class AddTime1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeTaken",
                table: "UserAnswer",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeTaken",
                table: "UserAnswer");
        }
    }
}
