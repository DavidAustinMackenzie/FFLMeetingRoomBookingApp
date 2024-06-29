using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FFLMeetingRoomBookingApp.Web.Migrations
{
    /// <inheritdoc />
    public partial class IntialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MeetingRoom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfPeople = table.Column<int>(type: "int", nullable: false),
                    MeetingDuration = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookedBy_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedBy_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedBy_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedBy_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedBy_UserId = table.Column<int>(type: "int", nullable: false),
                    BookedBy_UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedFor_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedFor_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedFor_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedFor_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookedFor_UserId = table.Column<int>(type: "int", nullable: false),
                    BookedFor_UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
