namespace EventFinder2021.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ComentaryAndReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Events_EventId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Events_EventId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EventId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EventId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EventId1",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ComentaryId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoingUsersId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NotGoingUsersId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Comentaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentaries_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentaries_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoingUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoingUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoingUsers_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotGoingUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotGoingUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotGoingUsers_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ComentaryId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Replies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Replies_Comentaries_ComentaryId",
                        column: x => x.ComentaryId,
                        principalTable: "Comentaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserGoingUsers",
                columns: table => new
                {
                    GoingUsersId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGoingUsers", x => new { x.GoingUsersId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGoingUsers_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGoingUsers_GoingUsers_GoingUsersId",
                        column: x => x.GoingUsersId,
                        principalTable: "GoingUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserNotGoingUsers",
                columns: table => new
                {
                    NotGoingUsersId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserNotGoingUsers", x => new { x.NotGoingUsersId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserNotGoingUsers_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserNotGoingUsers_NotGoingUsers_NotGoingUsersId",
                        column: x => x.NotGoingUsersId,
                        principalTable: "NotGoingUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGoingUsers_UsersId",
                table: "ApplicationUserGoingUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserNotGoingUsers_UsersId",
                table: "ApplicationUserNotGoingUsers",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentaries_EventId",
                table: "Comentaries",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentaries_IsDeleted",
                table: "Comentaries",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Comentaries_UserId1",
                table: "Comentaries",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_GoingUsers_EventId",
                table: "GoingUsers",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoingUsers_IsDeleted",
                table: "GoingUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_NotGoingUsers_EventId",
                table: "NotGoingUsers",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotGoingUsers_IsDeleted",
                table: "NotGoingUsers",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ComentaryId",
                table: "Replies",
                column: "ComentaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_IsDeleted",
                table: "Replies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_UserId",
                table: "Replies",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserGoingUsers");

            migrationBuilder.DropTable(
                name: "ApplicationUserNotGoingUsers");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "GoingUsers");

            migrationBuilder.DropTable(
                name: "NotGoingUsers");

            migrationBuilder.DropTable(
                name: "Comentaries");

            migrationBuilder.DropColumn(
                name: "ComentaryId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GoingUsersId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "NotGoingUsersId",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EventId",
                table: "AspNetUsers",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EventId1",
                table: "AspNetUsers",
                column: "EventId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Events_EventId",
                table: "AspNetUsers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Events_EventId1",
                table: "AspNetUsers",
                column: "EventId1",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
