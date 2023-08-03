using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NaanNCurry.Migrations
{
    public partial class AddScheduleBuffetToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleBuffet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuffetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuffetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleBuffet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleBuffet_Buffet_BuffetId",
                        column: x => x.BuffetId,
                        principalTable: "Buffet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleBuffet_BuffetId",
                table: "ScheduleBuffet",
                column: "BuffetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleBuffet");
        }
    }
}
