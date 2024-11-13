using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personnel_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLichNghi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LichNghis",
                columns: table => new
                {
                    LichNghiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LyDo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichNghis", x => x.LichNghiId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "LichNghis");

        }
    }
}
