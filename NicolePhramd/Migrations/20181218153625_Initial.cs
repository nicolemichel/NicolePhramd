using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NicolePhramd.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(maxLength: 100, nullable: false),
                    email = table.Column<string>(maxLength: 100, nullable: false),
                    password = table.Column<string>(maxLength: 100, nullable: false),
                    status = table.Column<string>(maxLength: 1, nullable: false, defaultValue: "A"),
                    signupdate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    country = table.Column<string>(nullable: false, defaultValue: "CA"),
                    city = table.Column<string>(nullable: false, defaultValue: "London"),
                    unit = table.Column<string>(nullable: false, defaultValue: "metric"),
                    status = table.Column<string>(nullable: false, defaultValue: "A"),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.id);
                    table.ForeignKey(
                        name: "FK_Weather_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weather_userId",
                table: "Weather",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
