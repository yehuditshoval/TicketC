using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket.Data.Migrations
{
    public partial class tlast1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LineId",
                table: "Seat",
                newName: "numLine");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Event",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "HoleId",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoleId",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "numLine",
                table: "Seat",
                newName: "LineId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Event",
                newName: "price");
        }
    }
}
