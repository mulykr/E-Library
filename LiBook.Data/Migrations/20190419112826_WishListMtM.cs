using Microsoft.EntityFrameworkCore.Migrations;

namespace LiBook.Data.Migrations
{
    public partial class WishListMtM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WishListItems",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_BookId",
                table: "WishListItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishListItems",
                table: "WishListItems",
                columns: new[] { "BookId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_UserId",
                table: "WishListItems",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WishListItems",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_UserId",
                table: "WishListItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishListItems",
                table: "WishListItems",
                columns: new[] { "UserId", "BookId" });

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_BookId",
                table: "WishListItems",
                column: "BookId");
        }
    }
}
