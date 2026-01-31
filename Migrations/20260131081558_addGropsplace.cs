using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class addGropsplace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaceId",
                table: "studyGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlaceStudyGroup",
                columns: table => new
                {
                    PlacesPlaceId = table.Column<int>(type: "int", nullable: false),
                    StudyGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceStudyGroup", x => new { x.PlacesPlaceId, x.StudyGroupId });
                    table.ForeignKey(
                        name: "FK_PlaceStudyGroup_Places_PlacesPlaceId",
                        column: x => x.PlacesPlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceStudyGroup_studyGroups_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "studyGroups",
                        principalColumn: "StudyGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceStudyGroup_StudyGroupId",
                table: "PlaceStudyGroup",
                column: "StudyGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceStudyGroup");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "studyGroups");
        }
    }
}
