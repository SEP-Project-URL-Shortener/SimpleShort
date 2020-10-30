using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleShort.Migrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortenedUrls",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    OriginalUrl = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Expiration = table.Column<string>(nullable: true),
                    LastUsed = table.Column<string>(nullable: true),
                    Created = table.Column<string>(nullable: true),
                    NumberOfUses = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortenedUrls", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortenedUrls");
        }
    }
}
