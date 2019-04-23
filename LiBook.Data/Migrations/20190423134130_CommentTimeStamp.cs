using Microsoft.EntityFrameworkCore.Migrations;

namespace LiBook.Data.Migrations
{
    public partial class CommentTimeStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimaStamp",
                table: "Comments",
                newName: "TimeStamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Comments",
                newName: "TimaStamp");
        }
    }
}
