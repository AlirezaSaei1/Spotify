using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewConnectorTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtistFollowers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArtistId = table.Column<string>(type: "TEXT", nullable: false),
                    FollowerUsertId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistFollowers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ArtistMusics",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArtistId = table.Column<string>(type: "TEXT", nullable: false),
                    PublishedMusicId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistMusics", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserFollowings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    FollowingArtistId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserMusics",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    SavedMusicId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMusics", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistFollowers");

            migrationBuilder.DropTable(
                name: "ArtistMusics");

            migrationBuilder.DropTable(
                name: "UserFollowings");

            migrationBuilder.DropTable(
                name: "UserMusics");
        }
    }
}
