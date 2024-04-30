using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherForecast.DAL.Migrations
{
    public partial class location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryId = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false),
                    stateId = table.Column<int>(type: "int", nullable: true),
                    cityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_CityId",
                        column: x => x.cityId,
                        principalTable: "cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Location_CountryId",
                        column: x => x.countryId,
                        principalTable: "countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Location_StateId",
                        column: x => x.stateId,
                        principalTable: "states",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_locations_cityId",
                table: "locations",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_locations_countryId",
                table: "locations",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_locations_stateId_cityId",
                table: "locations",
                columns: new[] { "stateId", "cityId" },
                unique: true,
                filter: "[stateId] IS NOT NULL AND [cityId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
