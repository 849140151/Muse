using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muse.DB.Migrations
{
    /// <inheritdoc />
    public partial class SongBasic_Duration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "SongBasic",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "SongBasic",
                type: "nvarchar(5)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
