using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedModelProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Music_AspNetUsers_ArtistId",
                table: "Music");

            migrationBuilder.DropTable(
                name: "ArtistFollowers");

            migrationBuilder.DropTable(
                name: "UserSavedMusics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Music",
                table: "Music");

            migrationBuilder.RenameTable(
                name: "Music",
                newName: "Musics");

            migrationBuilder.RenameIndex(
                name: "IX_Music_ArtistId",
                table: "Musics",
                newName: "IX_Musics_ArtistId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Musics",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musics",
                table: "Musics",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ArtistUser",
                columns: table => new
                {
                    FollowedArtistsId = table.Column<string>(type: "TEXT", nullable: false),
                    FollowersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistUser", x => new { x.FollowedArtistsId, x.FollowersId });
                    table.ForeignKey(
                        name: "FK_ArtistUser_AspNetUsers_FollowedArtistsId",
                        column: x => x.FollowedArtistsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistUser_AspNetUsers_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Musics_UserId",
                table: "Musics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistUser_FollowersId",
                table: "ArtistUser",
                column: "FollowersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_AspNetUsers_ArtistId",
                table: "Musics",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_AspNetUsers_UserId",
                table: "Musics",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_AspNetUsers_ArtistId",
                table: "Musics");

            migrationBuilder.DropForeignKey(
                name: "FK_Musics_AspNetUsers_UserId",
                table: "Musics");

            migrationBuilder.DropTable(
                name: "ArtistUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musics",
                table: "Musics");

            migrationBuilder.DropIndex(
                name: "IX_Musics_UserId",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Musics");

            migrationBuilder.RenameTable(
                name: "Musics",
                newName: "Music");

            migrationBuilder.RenameIndex(
                name: "IX_Musics_ArtistId",
                table: "Music",
                newName: "IX_Music_ArtistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Music",
                table: "Music",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ArtistFollowers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ArtistId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistFollowers", x => new { x.UserId, x.ArtistId });
                    table.ForeignKey(
                        name: "FK_ArtistFollower_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistFollower_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSavedMusics",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    MusicId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSavedMusics", x => new { x.UserId, x.MusicId });
                    table.ForeignKey(
                        name: "FK_UserSavedMusic_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSavedMusic_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistFollowers_ArtistId",
                table: "ArtistFollowers",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSavedMusics_MusicId",
                table: "UserSavedMusics",
                column: "MusicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Music_AspNetUsers_ArtistId",
                table: "Music",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
