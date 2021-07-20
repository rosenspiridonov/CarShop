using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShop.Data.Migrations
{
    public partial class MakeSomeDbColumsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_EuroStandards_EuroStandardId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "HorsePower",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EuroStandardId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_EuroStandards_EuroStandardId",
                table: "Cars",
                column: "EuroStandardId",
                principalTable: "EuroStandards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_EuroStandards_EuroStandardId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "HorsePower",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EuroStandardId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_EuroStandards_EuroStandardId",
                table: "Cars",
                column: "EuroStandardId",
                principalTable: "EuroStandards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
