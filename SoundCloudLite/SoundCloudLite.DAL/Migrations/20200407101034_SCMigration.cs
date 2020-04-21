using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoundCloudLite.DAL.Migrations
{
    public partial class SCMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Genre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Album = table.Column<string>(nullable: true),
                    ArtistId = table.Column<Guid>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    EmbeddingHtml = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tracks_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Country", "Genre", "Name" },
                values: new object[] { new Guid("b875b2df-d265-41a0-b8ea-c47392f741e1"), "Russia", "Post-hardcore", "Wildways" });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Country", "Genre", "Name" },
                values: new object[] { new Guid("128f2c37-4f7d-4a33-964f-758c8746b087"), "USA", "Blues-rock", "The Black Keys" });

            migrationBuilder.InsertData(
                table: "Tracks",
                columns: new[] { "Id", "Album", "ArtistId", "EmbeddingHtml", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("dc82d2d4-fa64-4d63-b003-9de9ee2418b0"), "Day X", new Guid("b875b2df-d265-41a0-b8ea-c47392f741e1"), "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/419111404&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>", new DateTime(2018, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sky" },
                    { new Guid("4f0bb24e-9795-4c95-8291-296269976dfc"), "Day X", new Guid("b875b2df-d265-41a0-b8ea-c47392f741e1"), "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/419115020&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>", new DateTime(2018, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lost" },
                    { new Guid("f395018f-4e08-460f-948d-47076fa15792"), "El Camino", new Guid("128f2c37-4f7d-4a33-964f-758c8746b087"), "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/28177029&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>", new DateTime(2011, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Run Right Back" },
                    { new Guid("206a36a3-44e3-44db-ab75-3909306b7bda"), "Turn Blue", new Guid("128f2c37-4f7d-4a33-964f-758c8746b087"), "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/255881185&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>", new DateTime(2015, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "In Time" },
                    { new Guid("de260092-b0a3-447a-b50c-b2cee90e5a15"), "Lo/Hi", new Guid("128f2c37-4f7d-4a33-964f-758c8746b087"), "<iframe width=\"100%\" height=\"300\" scrolling=\"no\" frameborder=\"no\" allow=\"autoplay\" src=\"https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/584680629&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true\"></iframe>", new DateTime(2019, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lo/Hi" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_ArtistId",
                table: "Tracks",
                column: "ArtistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}
