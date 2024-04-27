using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class localizations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Localizations",
                columns: table => new
                {
                    ResourceKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    LocalizedValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => x.ResourceKey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localizations");
        }
    }
}
