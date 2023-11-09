namespace FurnitureMarketBlazor.Server.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId, oi.ProductTypeId });

            modelBuilder.Entity<CartItem>()
               .HasKey(ci => new { ci.UserId, ci.ProductId, ci.ProductTypeId });

            /*
                * В данном случае, HasKey указывает, что составным ключом для сущности ProductVariant являются два поля: 
                  ProductId и ProductTypeId. Это означает, что комбинация значений этих двух полей должна быть уникальной 
                  для каждой записи в таблице, представляющей сущность ProductVariant.

                * Такое определение составного ключа позволяет нам оперировать с записями в таблице ProductVariant 
                  по уникальной комбинации значений ProductId и ProductTypeId, а также гарантирует целостность данных.
            */
            modelBuilder.Entity<ProductVariant>()
                .HasKey(p => new { p.ProductId, p.ProductTypeId });

            modelBuilder.Entity<ProductType>().HasData(
                    new ProductType { Id = 1, Name = "Эконом" },
                    new ProductType { Id = 2, Name = "Бюджет" },
                    new ProductType { Id = 3, Name = "Стандарт" },
                    new ProductType { Id = 4, Name = "Люкс" },
                    new ProductType { Id = 5, Name = "Премиум" }
                );

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
                        CategoryId = 1,
                        Featured = true
                    },
                    new Product
                    {
                        Id = 2,
                        Title = "Отличная кухня 2",
                        Description = "Описание кухни 2",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Mxek/CqEtuDEe6",
                        CategoryId = 1
                    },
                    new Product
                    {
                        Id = 3,
                        Title = "Отличная кухня 3",
                        Description = "Описание кухни 3",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Xe88/Zj4mZ4pvP",
                        CategoryId = 1
                    },
                    new Product
                    {
                        Id = 4,
                        Title = "Отличная кухня 4",
                        Description = "Описание кухни 4",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/foWx/771H2QoSZ",
                        CategoryId = 1
                    },
                    new Product
                    {
                        Id = 5,
                        Title = "Отличная мебель 1",
                        Description = "Описание мебель 1",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/53EG/r1KcH6Crf",
                        CategoryId = 2
                    },
                    new Product
                    {
                        Id = 6,
                        Title = "Отличная мебель 2",
                        Description = "Описание мебель 2",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/9GtZ/3gqAyzbJs",
                        CategoryId = 2,
                        Featured = true
                    },
                    new Product
                    {
                        Id = 7,
                        Title = "Отличная мебель 3",
                        Description = "Описание мебель 3",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/5eAR/sS9etkMaD",
                        CategoryId = 2
                    },
                    new Product
                    {
                        Id = 8,
                        Title = "Отличная мебель 4",
                        Description = "Описание мебель 4",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/t5cS/QGsHKccVa",
                        CategoryId = 2
                    },
                    new Product
                    {
                        Id = 9,
                        Title = "Отличный Санузел 1",
                        Description = "Отличный Санузел 1",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/9zj5/hwRJU1eUw",
                        CategoryId = 3
                    },
                    new Product
                    {
                        Id = 10,
                        Title = "Отличный Санузел 2",
                        Description = "Отличный Санузел 2",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/KmZ4/tSXu8WCsa",
                        CategoryId = 3,
                        Featured = true
                    },
                    new Product
                    {
                        Id = 11,
                        Title = "Отличный Санузел 3",
                        Description = "Отличный Санузел 3",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/VarG/D2RfZ8hFd",
                        CategoryId = 3
                    }
                );

            modelBuilder.Entity<ProductVariant>().HasData(
               new ProductVariant
               {
                   ProductId = 1,
                   ProductTypeId = 1,
                   Price = 90000.00m
               },
               new ProductVariant
               {
                   ProductId = 1,
                   ProductTypeId = 2,
                   Price = 110000.00m,
                   OriginalPrice = 120000.00m
               },
               new ProductVariant
               {
                   ProductId = 1,
                   ProductTypeId = 3,
                   Price = 150000.00m,
                   OriginalPrice = 180000.00m
               },
               new ProductVariant
               {
                   ProductId = 1,
                   ProductTypeId = 4,
                   Price = 200000.00m,
                   OriginalPrice = 250000.00m
               },
               new ProductVariant
               {
                   ProductId = 1,
                   ProductTypeId = 5,
                   Price = 300000.00m,
                   OriginalPrice = 380000.00m
               },

               new ProductVariant
               {
                   ProductId = 2,
                   ProductTypeId = 1,
                   Price = 70000.00m
               },
               new ProductVariant
               {
                   ProductId = 2,
                   ProductTypeId = 2,
                   Price = 100000.00m,
                   OriginalPrice = 110000.00m
               },
               new ProductVariant
               {
                   ProductId = 2,
                   ProductTypeId = 3,
                   Price = 120000.00m,
                   OriginalPrice = 140000.00m
               },
               new ProductVariant
               {
                   ProductId = 2,
                   ProductTypeId = 4,
                   Price = 150000.00m,
                   OriginalPrice = 200000.00m
               },
               new ProductVariant
               {
                   ProductId = 2,
                   ProductTypeId = 5,
                   Price = 180000.00m,
                   OriginalPrice = 260000.00m
               },

               new ProductVariant
               {
                   ProductId = 3,
                   ProductTypeId = 1,
                   Price = 90000.00m
               },
               new ProductVariant
               {
                   ProductId = 3,
                   ProductTypeId = 2,
                   Price = 130000.00m,
                   OriginalPrice = 150000.00m
               },
               new ProductVariant
               {
                   ProductId = 3,
                   ProductTypeId = 3,
                   Price = 180000.00m,
                   OriginalPrice = 210000.00m
               },
               new ProductVariant
               {
                   ProductId = 3,
                   ProductTypeId = 4,
                   Price = 210000.00m,
                   OriginalPrice = 240000.00m
               },
               new ProductVariant
               {
                   ProductId = 3,
                   ProductTypeId = 5,
                   Price = 240000.00m,
                   OriginalPrice = 320000.00m
               },

               new ProductVariant
               {
                   ProductId = 4,
                   ProductTypeId = 1,
                   Price = 75000.00m
               },
               new ProductVariant
               {
                   ProductId = 4,
                   ProductTypeId = 2,
                   Price = 115000.00m,
                   OriginalPrice = 130000.00m
               },
               new ProductVariant
               {
                   ProductId = 4,
                   ProductTypeId = 3,
                   Price = 160000.00m,
                   OriginalPrice = 210000.00m
               },
               new ProductVariant
               {
                   ProductId = 4,
                   ProductTypeId = 4,
                   Price = 240000.00m,
                   OriginalPrice = 290000.00m
               },
               new ProductVariant
               {
                   ProductId = 4,
                   ProductTypeId = 5,
                   Price = 340000.00m,
                   OriginalPrice = 390000.00m
               },
               new ProductVariant
               {
                   ProductId = 5,
                   ProductTypeId = 3,
                   Price = 60000.00m
               },
               new ProductVariant
               {
                   ProductId = 6,
                   ProductTypeId = 3,
                   Price = 50000.00m
               },
               new ProductVariant
               {
                   ProductId = 7,
                   ProductTypeId = 3,
                   Price = 80000.00m
               },
               new ProductVariant
               {
                   ProductId = 8,
                   ProductTypeId = 3,
                   Price = 99000.00m
               },
               new ProductVariant
               {
                   ProductId = 9,
                   ProductTypeId = 3,
                   Price = 50000.00m
               },
               new ProductVariant
               {
                   ProductId = 10,
                   ProductTypeId = 3,
                   Price = 40000.00m
               },
                new ProductVariant
                {
                    ProductId = 11,
                    ProductTypeId = 3,
                    Price = 50000.00m
                }
           );
        }
    }
}
