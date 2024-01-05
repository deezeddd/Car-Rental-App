using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRent.DataAccessLayer.Migrations
{
    public partial class agreementupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "RentAgreement",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarId",
                table: "RentAgreement");
        }
    }
}
