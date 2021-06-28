using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sportclub.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sporters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geslacht = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Geboortedatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LengteCm = table.Column<int>(type: "int", nullable: false),
                    GewichtKg = table.Column<double>(type: "float", nullable: false),
                    ImgPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sporters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sporters");
        }
    }
}
