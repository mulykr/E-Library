using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiBook.Data.Migrations
{
    public partial class StringId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Authors_AuthorId1",
                table: "AuthorBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Books_BookId1",
                table: "AuthorBooks");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBooks_AuthorId1",
                table: "AuthorBooks");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBooks_BookId1",
                table: "AuthorBooks");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "AuthorBooks");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "AuthorBooks");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Books",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Authors",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "AuthorBooks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "AuthorBooks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AuthorBooks",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistredOn",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_AuthorId",
                table: "AuthorBooks",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_BookId",
                table: "AuthorBooks",
                column: "BookId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Authors_AuthorId",
                table: "AuthorBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBooks_Books_BookId",
                table: "AuthorBooks");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBooks_AuthorId",
                table: "AuthorBooks");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBooks_BookId",
                table: "AuthorBooks");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegistredOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Books",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Authors",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "AuthorBooks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "AuthorBooks",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AuthorBooks",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId1",
                table: "AuthorBooks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookId1",
                table: "AuthorBooks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_AuthorId1",
                table: "AuthorBooks",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBooks_BookId1",
                table: "AuthorBooks",
                column: "BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Authors_AuthorId1",
                table: "AuthorBooks",
                column: "AuthorId1",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBooks_Books_BookId1",
                table: "AuthorBooks",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
