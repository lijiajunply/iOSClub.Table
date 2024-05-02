using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iOSClub.Share.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Resources");

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Resources",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resources",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(256)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Resources",
                type: "varchar(512)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(256)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Resources",
                type: "varchar(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resources",
                type: "varchar(256)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Resources",
                type: "varchar(256)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(512)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Resources",
                type: "varchar(256)",
                nullable: false,
                defaultValue: "");
        }
    }
}
