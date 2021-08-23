using Microsoft.EntityFrameworkCore.Migrations;

namespace Laborlance_API.Migrations
{
    public partial class ToolsImagesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Tools");

            migrationBuilder.CreateTable(
                name: "ToolImage",
                columns: table => new
                {
                    ToolImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolId = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    PublicId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolImage", x => x.ToolImageId);
                    table.ForeignKey(
                        name: "FK_ToolImage_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "ToolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToolImage_ToolId",
                table: "ToolImage",
                column: "ToolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToolImage");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
