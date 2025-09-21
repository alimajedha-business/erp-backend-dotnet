using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addforinkeywarehousestock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE warehouse.WarehouseStocks 
            ADD CONSTRAINT FK_WarehouseStocks_Companies_CompanyId 
            FOREIGN KEY (CompanyId) REFERENCES general.Companies(Id)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE WarehouseStocks DROP CONSTRAINT FK_WarehouseStocks_Companies_CompanyId");
        }
    }
}
