using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarShop.Web.Data.Migrations
{
    public partial class RemovePostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedOn",
                table: "Cars",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_OwnerId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_OwnerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PublishedOn",
                table: "Cars");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CarId",
                table: "Posts",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_OwnerId",
                table: "Posts",
                column: "OwnerId");
        }
    }
}
