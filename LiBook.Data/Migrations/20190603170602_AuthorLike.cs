using Microsoft.EntityFrameworkCore.Migrations;

namespace LiBook.Data.Migrations
{
    public partial class AuthorLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorLikes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserProfileId = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true),
                    Liked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthorLikes_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuthorLikes_AspNetUsers_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorLikes_AuthorId",
                table: "AuthorLikes",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorLikes_UserProfileId",
                table: "AuthorLikes",
                column: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorLikes");
        }
    }
}
