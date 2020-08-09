using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentSIMS.Migrations.Address
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    addressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentId = table.Column<int>(nullable: false),
                    streetNumber = table.Column<int>(nullable: false),
                    street = table.Column<string>(nullable: true),
                    suburb = table.Column<string>(nullable: false),
                    city = table.Column<string>(nullable: true),
                    postCode = table.Column<int>(nullable: false),
                    country = table.Column<string>(nullable: true),
                    timeCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.addressId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
