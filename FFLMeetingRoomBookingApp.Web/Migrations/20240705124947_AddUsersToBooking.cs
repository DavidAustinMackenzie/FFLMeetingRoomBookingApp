using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFLMeetingRoomBookingApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BookingId",
                table: "Users",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Bookings_BookingId",
                table: "Users",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Bookings_BookingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BookingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Users");
        }
    }
}
