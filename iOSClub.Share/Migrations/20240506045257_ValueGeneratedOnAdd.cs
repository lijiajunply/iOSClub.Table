using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iOSClub.Share.Migrations
{
    /// <inheritdoc />
    public partial class ValueGeneratedOnAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Tasks",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "BOOLEAN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Tasks",
                type: "BOOLEAN",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }
    }
}
