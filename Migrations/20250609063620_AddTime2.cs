using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizDishtv.Migrations
{
    /// <inheritdoc />
    public partial class AddTime2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TimeTaken",
                table: "UserAnswer",
                type: "int",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "TimeTaken",
                table: "UserAnswer",
                type: "time",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
