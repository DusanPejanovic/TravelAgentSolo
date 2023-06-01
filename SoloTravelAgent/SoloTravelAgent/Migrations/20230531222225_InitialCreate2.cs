using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoloTravelAgent.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TouristAttractions_Trips_TripId",
                table: "TouristAttractions");

            migrationBuilder.DropIndex(
                name: "IX_TouristAttractions_TripId",
                table: "TouristAttractions");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "TouristAttractions");

            migrationBuilder.CreateTable(
                name: "TripTouristAttractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TripId = table.Column<int>(type: "INTEGER", nullable: false),
                    TouristAttractionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripTouristAttractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripTouristAttractions_TouristAttractions_TouristAttractionId",
                        column: x => x.TouristAttractionId,
                        principalTable: "TouristAttractions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripTouristAttractions_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripTouristAttractions_TouristAttractionId",
                table: "TripTouristAttractions",
                column: "TouristAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_TripTouristAttractions_TripId",
                table: "TripTouristAttractions",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripTouristAttractions");

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "TouristAttractions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TouristAttractions_TripId",
                table: "TouristAttractions",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_TouristAttractions_Trips_TripId",
                table: "TouristAttractions",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");
        }
    }
}
