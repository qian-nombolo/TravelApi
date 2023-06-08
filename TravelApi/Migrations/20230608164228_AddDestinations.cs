using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelApi.Migrations
{
    public partial class AddDestinations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Travels",
                columns: new[] { "TravelId", "City", "Country", "Date", "Destination", "Rating", "Review" },
                values: new object[] { 5, "Beijing", "China", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Summer Palace", 7, "Good" });

            migrationBuilder.InsertData(
                table: "Travels",
                columns: new[] { "TravelId", "City", "Country", "Date", "Destination", "Rating", "Review" },
                values: new object[] { 6, "Beijing", "China", new DateTime(2022, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Temple of Heaven", 8, "Great" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "TravelId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "TravelId",
                keyValue: 6);
        }
    }
}
