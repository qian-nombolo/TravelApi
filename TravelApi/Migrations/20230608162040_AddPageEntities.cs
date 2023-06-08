using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApi.Migrations
{
    public partial class AddPageEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Travels",
                columns: table => new
                {
                    TravelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Destination = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Country = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Review = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.TravelId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Travels",
                columns: new[] { "TravelId", "City", "Country", "Date", "Destination", "Rating", "Review" },
                values: new object[,]
                {
                    { 1, "Siem Reap", "Cambodia", new DateTime(2009, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Angkor Wat", 7, "Spiritual" },
                    { 2, "Beijing", "China", new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Great Wall", 9, "Great" },
                    { 3, "Paris", "France", new DateTime(2023, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Louvre Museum", 4, "Crowded" },
                    { 4, "London", "United Kingdom", new DateTime(2023, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Big Ben", 9, "Great" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Travels");
        }
    }
}
