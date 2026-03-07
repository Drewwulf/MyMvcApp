using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class MIGRATIONN3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studyGroups_Teachers_teachersId",
                table: "studyGroups");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "studyGroups");

            migrationBuilder.RenameColumn(
                name: "teachersId",
                table: "studyGroups",
                newName: "TeachersId");

            migrationBuilder.RenameIndex(
                name: "IX_studyGroups_teachersId",
                table: "studyGroups",
                newName: "IX_studyGroups_TeachersId");

            migrationBuilder.AddForeignKey(
                name: "FK_studyGroups_Teachers_TeachersId",
                table: "studyGroups",
                column: "TeachersId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studyGroups_Teachers_TeachersId",
                table: "studyGroups");

            migrationBuilder.RenameColumn(
                name: "TeachersId",
                table: "studyGroups",
                newName: "teachersId");

            migrationBuilder.RenameIndex(
                name: "IX_studyGroups_TeachersId",
                table: "studyGroups",
                newName: "IX_studyGroups_teachersId");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "studyGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_studyGroups_Teachers_teachersId",
                table: "studyGroups",
                column: "teachersId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
