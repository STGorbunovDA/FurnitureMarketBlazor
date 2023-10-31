using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureMarketBlazor.Server.Migrations
{
    public partial class ProductSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Описание кухни 1", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/GWwc/CUW5BiG5e", 389999.99m, "Отличная кухня 1" },
                    { 2, "Описание кухни 2", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Mxek/CqEtuDEe6", 180000.99m, "Отличная кухня 2" },
                    { 3, "Описание кухни 3", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Xe88/Zj4mZ4pvP", 301000.00m, "Отличная кухня 3" },
                    { 4, "Описание кухни 4", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/foWx/771H2QoSZ", 90101.00m, "Отличная кухня 4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
