using Microsoft.EntityFrameworkCore.Migrations;

namespace EventFinder2021.Data.Migrations
{
    public partial class UserIdAndLikeIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Dislikes_DislikeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Likes_LikeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DislikeId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LikeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DislikeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LikeId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationUserDislike",
                columns: table => new
                {
                    DislikesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserDislike", x => new { x.DislikesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserDislike_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserDislike_Dislikes_DislikesId",
                        column: x => x.DislikesId,
                        principalTable: "Dislikes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserLike",
                columns: table => new
                {
                    LikesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserLike", x => new { x.LikesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserLike_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserLike_Likes_LikesId",
                        column: x => x.LikesId,
                        principalTable: "Likes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDislike_UsersId",
                table: "ApplicationUserDislike",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserLike_UsersId",
                table: "ApplicationUserLike",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserDislike");

            migrationBuilder.DropTable(
                name: "ApplicationUserLike");

            migrationBuilder.AddColumn<int>(
                name: "DislikeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DislikeId",
                table: "AspNetUsers",
                column: "DislikeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LikeId",
                table: "AspNetUsers",
                column: "LikeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Dislikes_DislikeId",
                table: "AspNetUsers",
                column: "DislikeId",
                principalTable: "Dislikes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Likes_LikeId",
                table: "AspNetUsers",
                column: "LikeId",
                principalTable: "Likes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
