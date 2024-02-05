using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iOSClub.Share.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Link = table.Column<string>(type: "varchar(256)", nullable: false),
                    Title = table.Column<string>(type: "varchar(256)", nullable: false),
                    Cover = table.Column<string>(type: "varchar(256)", nullable: false),
                    Digest = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Link);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(256)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Identity = table.Column<string>(type: "varchar(20)", nullable: false),
                    Tag = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(10)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Academy = table.Column<string>(type: "varchar(50)", nullable: false),
                    PoliticalLandscape = table.Column<string>(type: "varchar(10)", nullable: false),
                    Gender = table.Column<string>(type: "varchar(2)", nullable: false),
                    ClassName = table.Column<string>(type: "varchar(20)", nullable: false),
                    PhoneNum = table.Column<string>(type: "varchar(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
