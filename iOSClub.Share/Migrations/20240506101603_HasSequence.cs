using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iOSClub.Share.Migrations
{
    /// <inheritdoc />
    public partial class HasSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "ProjectModelId");

            migrationBuilder.CreateSequence<int>(
                name: "ResourceModelId");

            migrationBuilder.CreateSequence<int>(
                name: "TaskModelId");

            migrationBuilder.CreateSequence<int>(
                name: "ToolModelId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tools",
                type: "INTEGER",
                nullable: false,
                defaultValueSql: "nextval('\"ToolModelId\"')",
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValueSql: "nextval('\"TaskModelId\"')",
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Resources",
                type: "INTEGER",
                nullable: false,
                defaultValueSql: "nextval('\"ResourceModelId\"')",
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValueSql: "nextval('\"ProjectModelId\"')",
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "ProjectModelId");

            migrationBuilder.DropSequence(
                name: "ResourceModelId");

            migrationBuilder.DropSequence(
                name: "TaskModelId");

            migrationBuilder.DropSequence(
                name: "ToolModelId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tools",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValueSql: "nextval('\"ToolModelId\"')")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValueSql: "nextval('\"TaskModelId\"')")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Resources",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValueSql: "nextval('\"ResourceModelId\"')")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValueSql: "nextval('\"ProjectModelId\"')")
                .Annotation("Sqlite:Autoincrement", true)
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
