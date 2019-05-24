using Microsoft.EntityFrameworkCore.Migrations;

namespace LiBook.Data.Migrations
{
    public partial class cascadecommentlikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_AspNetUsers_UserProfileId",
                table: "CommentLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_AspNetUsers_UserProfileId",
                table: "CommentLikes",
                column: "UserProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_AspNetUsers_UserProfileId",
                table: "CommentLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_AspNetUsers_UserProfileId",
                table: "CommentLikes",
                column: "UserProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
