using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SebGlow.DataAccess.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    owner_login = table.Column<string>(nullable: true),
                    owner_id = table.Column<int>(nullable: false),
                    owner_url = table.Column<string>(nullable: true),
                    letters = table.Column<string>(nullable: true),
                    avgStargazers = table.Column<decimal>(nullable: false),
                    avgWatchers = table.Column<decimal>(nullable: false),
                    avgForks = table.Column<decimal>(nullable: false),
                    avgSize = table.Column<decimal>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}
