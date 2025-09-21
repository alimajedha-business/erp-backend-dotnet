using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Warehouse");

            migrationBuilder.CreateTable(
                name: "ProductCodes",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstLevelCode = table.Column<int>(type: "int", nullable: false),
                    FirstLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SecondLevelCode = table.Column<int>(type: "int", nullable: false),
                    SecondLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThirdLevelCode = table.Column<int>(type: "int", nullable: true),
                    ThirdLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ThirdNextLevel = table.Column<bool>(type: "bit", nullable: true),
                    FourthLevelCode = table.Column<int>(type: "int", nullable: true),
                    FourthLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FourthNextLevel = table.Column<bool>(type: "bit", nullable: true),
                    FifthLevelCode = table.Column<int>(type: "int", nullable: true),
                    FifthLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FifthNextLevel = table.Column<bool>(type: "bit", nullable: true),
                    SixthLevelCode = table.Column<int>(type: "int", nullable: true),
                    SixthLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SixthNextLevel = table.Column<bool>(type: "bit", nullable: true),
                    SeventhLevelCode = table.Column<int>(type: "int", nullable: true),
                    SeventhLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SeventhNextLevel = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCodes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCodes",
                schema: "Warehouse");
        }
    }
}
