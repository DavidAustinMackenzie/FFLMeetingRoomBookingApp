using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFLMeetingRoomBookingApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class CombinedUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BookedBy_FirstName",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookedBy_LastName",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BookedFor_LastName",
                table: "Bookings",
                newName: "BookedFor_Name");

            migrationBuilder.RenameColumn(
                name: "BookedFor_FirstName",
                table: "Bookings",
                newName: "BookedBy_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "BookedFor_Name",
                table: "Bookings",
                newName: "BookedFor_LastName");

            migrationBuilder.RenameColumn(
                name: "BookedBy_Name",
                table: "Bookings",
                newName: "BookedFor_FirstName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookedBy_FirstName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookedBy_LastName",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
