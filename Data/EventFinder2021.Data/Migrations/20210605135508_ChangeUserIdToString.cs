using Microsoft.EntityFrameworkCore.Migrations;

namespace EventFinder2021.Data.Migrations
{
    public partial class ChangeUserIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentaries_AspNetUsers_UserId1",
                table: "Comentaries");

            migrationBuilder.DropIndex(
                name: "IX_Comentaries_UserId1",
                table: "Comentaries");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Comentaries");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Comentaries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Comentaries_UserId",
                table: "Comentaries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentaries_AspNetUsers_UserId",
                table: "Comentaries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentaries_AspNetUsers_UserId",
                table: "Comentaries");

            migrationBuilder.DropIndex(
                name: "IX_Comentaries_UserId",
                table: "Comentaries");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Comentaries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Comentaries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comentaries_UserId1",
                table: "Comentaries",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentaries_AspNetUsers_UserId1",
                table: "Comentaries",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
