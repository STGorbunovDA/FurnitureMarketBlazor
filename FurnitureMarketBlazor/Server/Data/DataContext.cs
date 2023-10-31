namespace FurnitureMarketBlazor.Server.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Кухни",
                    Url = "kitchens"
                },
                new Category
                {
                    Id = 2,
                    Name = "Мебель",
                    Url = "furniture"
                },
                new Category
                {
                    Id = 3,
                    Name = "Санузел",
                    Url = "bathroom"
                });

            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = 1,
                        Title = "Отличная кухня 1",
                        Description = "Описание кухни 1",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/GWwc/CUW5BiG5e",
                        Price = 389999.99m,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Id = 2,
                        Title = "Отличная кухня 2",
                        Description = "Описание кухни 2",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Mxek/CqEtuDEe6",
                        Price = 180000.99m,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Id = 3,
                        Title = "Отличная кухня 3",
                        Description = "Описание кухни 3",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Xe88/Zj4mZ4pvP",
                        Price = 301000.00m,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Id = 4,
                        Title = "Отличная кухня 4",
                        Description = "Описание кухни 4",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/foWx/771H2QoSZ",
                        Price = 90101.00m,
                        CategoryId = 1
                    },
                    new Product
                    {
                        Id = 5,
                        Title = "Отличная мебель 1",
                        Description = "Описание мебель 1",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/53EG/r1KcH6Crf",
                        Price = 22500.00m,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Id = 6,
                        Title = "Отличная мебель 2",
                        Description = "Описание мебель 2",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/9GtZ/3gqAyzbJs",
                        Price = 33000.00m,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Id = 7,
                        Title = "Отличная мебель 3",
                        Description = "Описание мебель 3",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/5eAR/sS9etkMaD",
                        Price = 44100.99m,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Id = 8,
                        Title = "Отличная мебель 4",
                        Description = "Описание мебель 4",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/t5cS/QGsHKccVa",
                        Price = 64100.00m,
                        CategoryId = 2
                    },
                    new Product
                    {
                        Id = 9,
                        Title = "Отличный Санузел 1",
                        Description = "Отличный Санузел 1",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/9zj5/hwRJU1eUw",
                        Price = 74300.10m,
                        CategoryId = 3
                    },
                    new Product
                    {
                        Id = 10,
                        Title = "Отличный Санузел 2",
                        Description = "Отличный Санузел 2",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/KmZ4/tSXu8WCsa",
                        Price = 74300.10m,
                        CategoryId = 3
                    },
                    new Product
                    {
                        Id = 11,
                        Title = "Отличный Санузел 3",
                        Description = "Отличный Санузел 3",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/VarG/D2RfZ8hFd",
                        Price = 48800.10m,
                        CategoryId = 3
                    }
                );
        }
    }
}
