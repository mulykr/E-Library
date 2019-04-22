using Microsoft.EntityFrameworkCore.Migrations;

namespace LiBook.Data.Migrations
{
    public partial class WishListUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_Books_BookId",
                table: "WishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_AspNetUsers_UserId",
                table: "WishListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishListItems",
                table: "WishListItems");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "WishListItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WishListItems",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "WishListItems",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishListItems",
                table: "WishListItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_BookId",
                table: "WishListItems",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_Books_BookId",
                table: "WishListItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_AspNetUsers_UserId",
                table: "WishListItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_Books_BookId",
                table: "WishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishListItems_AspNetUsers_UserId",
                table: "WishListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishListItems",
                table: "WishListItems");

            migrationBuilder.DropIndex(
                name: "IX_WishListItems_BookId",
                table: "WishListItems");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "WishListItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "WishListItems",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "WishListItems",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WishListItems",
                table: "WishListItems",
                columns: new[] { "BookId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_Books_BookId",
                table: "WishListItems",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishListItems_AspNetUsers_UserId",
                table: "WishListItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
