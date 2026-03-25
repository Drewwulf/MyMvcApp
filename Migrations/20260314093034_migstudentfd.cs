using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class migstudentfd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_studyGroups_groupStudyGroupId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_groupStudyGroupId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "groupStudyGroupId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "StudyGroupId",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudyGroupId",
                table: "Students",
                column: "StudyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_studyGroups_StudyGroupId",
                table: "Students",
                column: "StudyGroupId",
                principalTable: "studyGroups",
                principalColumn: "StudyGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_studyGroups_StudyGroupId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudyGroupId",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "StudyGroupId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "groupStudyGroupId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_groupStudyGroupId",
                table: "Students",
                column: "groupStudyGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_studyGroups_groupStudyGroupId",
                table: "Students",
                column: "groupStudyGroupId",
                principalTable: "studyGroups",
                principalColumn: "StudyGroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
