using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iOSClub.Table.Migrations
{
    /// <inheritdoc />
    public partial class AddKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Staffs",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "Staffs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Key");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Staffs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Students",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Staffs",
                newName: "Id");
        }
    }
}
