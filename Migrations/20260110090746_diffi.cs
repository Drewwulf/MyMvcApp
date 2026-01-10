using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class diffi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Directions_DirectionId",
                table: "Test");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Test",
                table: "Test");

            migrationBuilder.RenameTable(
                name: "Test",
                newName: "Tests");

            migrationBuilder.RenameIndex(
                name: "IX_Test_DirectionId",
                table: "Tests",
                newName: "IX_Tests_DirectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tests",
                table: "Tests",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Directions_DirectionId",
                table: "Tests",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "DirectionId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Directions_DirectionId",
                table: "Tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tests",
                table: "Tests");

            migrationBuilder.RenameTable(
                name: "Tests",
                newName: "Test");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_DirectionId",
                table: "Test",
                newName: "IX_Test_DirectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Test",
                table: "Test",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Directions_DirectionId",
                table: "Test",
                column: "DirectionId",
                principalTable: "Directions",
                principalColumn: "DirectionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
