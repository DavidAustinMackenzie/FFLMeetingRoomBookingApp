using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFLMeetingRoomBookingApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class BookingParticipants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Participants",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Participants",
                table: "Booking");
        }
    }
}
