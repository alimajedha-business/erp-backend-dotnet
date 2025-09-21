using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateProductHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductHierarchies",
                schema: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstLevelSize = table.Column<byte>(type: "tinyint", nullable: false),
                    FirstLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SecondLevelSize = table.Column<byte>(type: "tinyint", nullable: false),
                    SecondLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ThirdLevelSize = table.Column<byte>(type: "tinyint", nullable: true),
                    ThirdLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FourthLevelSize = table.Column<byte>(type: "tinyint", nullable: true),
                    FourthLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FifthLevelSize = table.Column<byte>(type: "tinyint", nullable: true),
                    FifthLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SixthLevelSize = table.Column<byte>(type: "tinyint", nullable: true),
                    SixthLevelType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductHierarchies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductHierarchies",
                schema: "Warehouse");
        }
    }
}
