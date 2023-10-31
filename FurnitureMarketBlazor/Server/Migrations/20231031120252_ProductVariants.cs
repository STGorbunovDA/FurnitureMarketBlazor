using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureMarketBlazor.Server.Migrations
{
    public partial class ProductVariants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => new { x.ProductId, x.ProductTypeId });
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductVariants_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Эконом" },
                    { 2, "Бюджет" },
                    { 3, "Стандарт" },
                    { 4, "Люкс" },
                    { 5, "Премиум" }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "ProductId", "ProductTypeId", "OriginalPrice", "Price" },
                values: new object[,]
                {
                    { 1, 1, 0m, 90000.00m },
                    { 1, 2, 120000.00m, 110000.00m },
                    { 1, 3, 180000.00m, 150000.00m },
                    { 1, 4, 250000.00m, 200000.00m },
                    { 1, 5, 380000.00m, 300000.00m },
                    { 2, 1, 0m, 70000.00m },
                    { 2, 2, 110000.00m, 100000.00m },
                    { 2, 3, 140000.00m, 120000.00m },
                    { 2, 4, 200000.00m, 150000.00m },
                    { 2, 5, 260000.00m, 180000.00m },
                    { 3, 1, 0m, 90000.00m },
                    { 3, 2, 150000.00m, 130000.00m },
                    { 3, 3, 210000.00m, 180000.00m },
                    { 3, 4, 240000.00m, 210000.00m },
                    { 3, 5, 320000.00m, 240000.00m },
                    { 4, 1, 0m, 75000.00m },
                    { 4, 2, 130000.00m, 115000.00m },
                    { 4, 3, 210000.00m, 160000.00m },
                    { 4, 4, 290000.00m, 240000.00m },
                    { 4, 5, 390000.00m, 340000.00m },
                    { 5, 3, 0m, 60000.00m },
                    { 6, 3, 0m, 50000.00m },
                    { 7, 3, 0m, 80000.00m },
                    { 8, 3, 0m, 99000.00m },
                    { 9, 3, 0m, 50000.00m },
                    { 10, 3, 0m, 40000.00m },
                    { 11, 3, 0m, 50000.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductTypeId",
                table: "ProductVariants",
                column: "ProductTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Price",
                value: 389999.99m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Price",
                value: 180000.99m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Price",
                value: 301000.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Price",
                value: 90101.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Price",
                value: 22500.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "Price",
                value: 33000.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "Price",
                value: 44100.99m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "Price",
                value: 64100.00m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "Price",
                value: 74300.10m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "Price",
                value: 74300.10m);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "Price",
                value: 48800.10m);
        }
    }
}
