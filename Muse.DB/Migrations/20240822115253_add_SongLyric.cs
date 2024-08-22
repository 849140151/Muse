using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muse.DB.Migrations
{
    /// <inheritdoc />
    public partial class add_SongLyric : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lyrics",
                table: "SongDetail");

            migrationBuilder.CreateTable(
                name: "SongLyrics",
                columns: table => new
                {
                    SongLyricId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LyricTimeStamp = table.Column<double>(type: "REAL", nullable: false),
                    Japanese = table.Column<string>(type: "TEXT", nullable: true),
                    Chinese = table.Column<string>(type: "TEXT", nullable: true),
                    English = table.Column<string>(type: "TEXT", nullable: true),
                    SongBasicId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongLyrics", x => x.SongLyricId);
                    table.ForeignKey(
                        name: "FK_SongLyrics_SongBasic_SongBasicId",
                        column: x => x.SongBasicId,
                        principalTable: "SongBasic",
                        principalColumn: "SongBasicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongLyrics_SongBasicId",
                table: "SongLyrics",
                column: "SongBasicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongLyrics");

            migrationBuilder.AddColumn<string>(
                name: "Lyrics",
                table: "SongDetail",
                type: "TEXT",
                nullable: true);
        }
    }
}
