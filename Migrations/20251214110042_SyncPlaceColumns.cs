using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMvcApp.Migrations
{
    /// <inheritdoc />
    public partial class SyncPlaceColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Places",
                newName: "DestinationName");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Places",
                newName: "DestinationAddress");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Places",
                newName: "PlaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DestinationName",
                table: "Places",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DestinationAddress",
                table: "Places",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "PlaceId",
                table: "Places",
                newName: "Id");
        }
    }
}
