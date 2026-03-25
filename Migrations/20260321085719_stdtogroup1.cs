using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class stdtogroup1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyGroupId",
                table: "StudentToGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentToGroups_StudentId",
                table: "StudentToGroups",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentToGroups_StudyGroupId",
                table: "StudentToGroups",
                column: "StudyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentToGroups_Students_StudentId",
                table: "StudentToGroups",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentToGroups_studyGroups_StudyGroupId",
                table: "StudentToGroups",
                column: "StudyGroupId",
                principalTable: "studyGroups",
                principalColumn: "StudyGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentToGroups_Students_StudentId",
                table: "StudentToGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentToGroups_studyGroups_StudyGroupId",
                table: "StudentToGroups");

            migrationBuilder.DropIndex(
                name: "IX_StudentToGroups_StudentId",
                table: "StudentToGroups");

            migrationBuilder.DropIndex(
                name: "IX_StudentToGroups_StudyGroupId",
                table: "StudentToGroups");

            migrationBuilder.DropColumn(
                name: "StudyGroupId",
                table: "StudentToGroups");
        }
    }
}
