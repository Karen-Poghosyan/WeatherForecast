using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForecast.DAL.Migrations
{
    public partial class Id_Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "stateId",
                table: "states",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "countryId",
                table: "countries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "cityId",
                table: "cities",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "states",
                newName: "stateId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "countries",
                newName: "countryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cities",
                newName: "cityId");
        }
    }
}
