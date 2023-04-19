using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_baz_lab.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfMeal = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Carbohydrates = table.Column<double>(type: "float", nullable: false),
                    Fats = table.Column<double>(type: "float", nullable: false),
                    Proteins = table.Column<double>(type: "float", nullable: false),
                    Kkal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodDayMeal",
                columns: table => new
                {
                    FoodDayId = table.Column<int>(type: "int", nullable: false),
                    MealsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodDayMeal", x => new { x.FoodDayId, x.MealsId });
                    table.ForeignKey(
                        name: "FK_FoodDayMeal_FoodDay_FoodDayId",
                        column: x => x.FoodDayId,
                        principalTable: "FoodDay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodDayMeal_Meal_MealsId",
                        column: x => x.MealsId,
                        principalTable: "Meal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodDayMeal_MealsId",
                table: "FoodDayMeal",
                column: "MealsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodDayMeal");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "FoodDay");

            migrationBuilder.DropTable(
                name: "Meal");
        }
    }
}
