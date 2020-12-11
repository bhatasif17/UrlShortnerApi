using Microsoft.EntityFrameworkCore.Migrations;

namespace URLShortner.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "URLStore",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URLStore", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "URLStore");
        }
    }
}
