using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiBook.Data.Migrations
{
    public partial class WishListInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Authors_AuthorId",
                table: "AuthorBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorBooks",
                table: "AuthorBooks");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBooks_AuthorId",
                table: "AuthorBooks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AuthorBooks");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "AuthorBooks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "AuthorBooks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorBooks",
                table: "AuthorBooks",
                columns: new[] { "AuthorId", "BookId" });

            migrationBuilder.CreateTable(
                name: "WishListItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: true),
                    BookId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItems", x => new { x.UserId, x.BookId });
                    table.ForeignKey(
                        name: "FK_WishListItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_BookId",
                table: "WishListItems",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Authors_AuthorId",
                table: "AuthorBooks",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Authors_AuthorId",
                table: "AuthorBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks");

            migrationBuilder.DropTable(
                name: "WishListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorBooks",
                table: "AuthorBooks");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "AuthorBooks",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "AuthorBooks",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AuthorBooks",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorBooks",
                table: "AuthorBooks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_AuthorId",
                table: "AuthorBooks",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Authors_AuthorId",
                table: "AuthorBooks",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
