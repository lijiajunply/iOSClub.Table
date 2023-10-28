using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iOSClub.Table.Migrations
{
    /// <inheritdoc />
    public partial class IdVarChar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Students",
                type: "varchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Staffs",
                type: "varchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Students",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Staffs",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)");
        }
    }
}
