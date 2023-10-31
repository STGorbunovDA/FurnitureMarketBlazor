using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureMarketBlazor.Server.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[] { 1, "Кухни", "kitchens" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[] { 2, "Мебель", "furniture" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[] { 3, "Санузел", "bathroom" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CategoryId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 5, 2, "Описание мебель 1", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/53EG/r1KcH6Crf", 22500.00m, "Отличная мебель 1" },
                    { 6, 2, "Описание мебель 2", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/9GtZ/3gqAyzbJs", 33000.00m, "Отличная мебель 2" },
                    { 7, 2, "Описание мебель 3", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/5eAR/sS9etkMaD", 44100.99m, "Отличная мебель 3" },
                    { 8, 2, "Описание мебель 4", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/t5cS/QGsHKccVa", 64100.00m, "Отличная мебель 4" },
                    { 9, 3, "Отличный Санузел 1", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/9zj5/hwRJU1eUw", 74300.10m, "Отличный Санузел 1" },
                    { 10, 3, "Отличный Санузел 2", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/KmZ4/tSXu8WCsa", 74300.10m, "Отличный Санузел 2" },
                    { 11, 3, "Отличный Санузел 3", "https://thumb.cloud.mail.ru/weblink/thumb/xw1/VarG/D2RfZ8hFd", 48800.10m, "Отличный Санузел 3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");
        }
    }
}
