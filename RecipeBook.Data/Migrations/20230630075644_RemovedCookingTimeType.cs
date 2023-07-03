using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeBook.Data.Migrations
{
    public partial class RemovedCookingTimeType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CookingTimeType",
                table: "Recipes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 30, 7, 56, 44, 687, DateTimeKind.Utc).AddTicks(1023),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 29, 14, 25, 42, 753, DateTimeKind.Utc).AddTicks(4218));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 30, 7, 56, 44, 686, DateTimeKind.Utc).AddTicks(9320),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 29, 14, 25, 42, 753, DateTimeKind.Utc).AddTicks(2797));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 29, 14, 25, 42, 753, DateTimeKind.Utc).AddTicks(4218),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 30, 7, 56, 44, 687, DateTimeKind.Utc).AddTicks(1023));

            migrationBuilder.AddColumn<int>(
                name: "CookingTimeType",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 6, 29, 14, 25, 42, 753, DateTimeKind.Utc).AddTicks(2797),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 6, 30, 7, 56, 44, 686, DateTimeKind.Utc).AddTicks(9320));
        }
    }
}
