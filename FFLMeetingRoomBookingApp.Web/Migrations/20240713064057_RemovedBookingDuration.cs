using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFLMeetingRoomBookingApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBookingDuration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetingDuration",
                table: "Booking");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MeetingDuration",
                table: "Booking",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
