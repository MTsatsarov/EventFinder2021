namespace EventFinder2021.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class LikesAndDislikeTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Dislikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComentaryId = table.Column<int>(type: "int", nullable: true),
                    ReplyId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dislikes_Comentaries_ComentaryId",
                        column: x => x.ComentaryId,
                        principalTable: "Comentaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dislikes_Replies_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "Replies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComentaryId = table.Column<int>(type: "int", nullable: true),
                    ReplyId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Comentaries_ComentaryId",
                        column: x => x.ComentaryId,
                        principalTable: "Comentaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_Replies_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "Replies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DislikeId",
                table: "AspNetUsers",
                column: "DislikeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LikeId",
                table: "AspNetUsers",
                column: "LikeId");

            migrationBuilder.CreateIndex(
                name: "IX_Dislikes_ComentaryId",
                table: "Dislikes",
                column: "ComentaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Dislikes_IsDeleted",
                table: "Dislikes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Dislikes_ReplyId",
                table: "Dislikes",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ComentaryId",
                table: "Likes",
                column: "ComentaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_IsDeleted",
                table: "Likes",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ReplyId",
                table: "Likes",
                column: "ReplyId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Dislikes_DislikeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Likes_LikeId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Dislikes");

            migrationBuilder.DropTable(
                name: "Likes");

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
        }
    }
}
