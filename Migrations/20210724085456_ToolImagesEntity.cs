using Microsoft.EntityFrameworkCore.Migrations;

namespace Laborlance_API.Migrations
{
    public partial class ToolImagesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToolImage_Tools_ToolId",
                table: "ToolImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToolImage",
                table: "ToolImage");

            migrationBuilder.RenameTable(
                name: "ToolImage",
                newName: "ToolImages");

            migrationBuilder.RenameIndex(
                name: "IX_ToolImage_ToolId",
                table: "ToolImages",
                newName: "IX_ToolImages_ToolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToolImages",
                table: "ToolImages",
                column: "ToolImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToolImages_Tools_ToolId",
                table: "ToolImages",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "ToolId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToolImages_Tools_ToolId",
                table: "ToolImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToolImages",
                table: "ToolImages");

            migrationBuilder.RenameTable(
                name: "ToolImages",
                newName: "ToolImage");

            migrationBuilder.RenameIndex(
                name: "IX_ToolImages_ToolId",
                table: "ToolImage",
                newName: "IX_ToolImage_ToolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToolImage",
                table: "ToolImage",
                column: "ToolImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToolImage_Tools_ToolId",
                table: "ToolImage",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "ToolId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
