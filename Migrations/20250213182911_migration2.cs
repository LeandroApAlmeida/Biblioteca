using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    StringValue = table.Column<string>(type: "text", nullable: true),
                    IntValue = table.Column<int>(type: "integer", nullable: true),
                    LongValue = table.Column<long>(type: "bigint", nullable: true),
                    FloatValue = table.Column<float>(type: "real", nullable: true),
                    DoubleValue = table.Column<double>(type: "double precision", nullable: true),
                    BoolValue = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Settings", x => new { x.Id, x.UserId });
                    table.ForeignKey(
                        name: "FK_Settings_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_UserId2",
                table: "Settings",
                column: "UserId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
