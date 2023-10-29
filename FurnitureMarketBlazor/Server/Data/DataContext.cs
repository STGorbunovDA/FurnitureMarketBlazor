namespace FurnitureMarketBlazor.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        Id = 1,
                        Title = "Отличная кухня 1",
                        Description = "Описание кухни 1",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/GWwc/CUW5BiG5e",
                        Price = 389999.99m
                    },
                    new Product
                    {
                        Id = 2,
                        Title = "Отличная кухня 2",
                        Description = "Описание кухни 2",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Mxek/CqEtuDEe6",
                        Price = 189999.99m
                    },
                    new Product
                    {
                        Id = 3,
                        Title = "Отличная кухня 3",
                        Description = "Описание кухни 3",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Xe88/Zj4mZ4pvP",
                        Price = 289999.99m
                    },
                    new Product
                    {
                        Id = 4,
                        Title = "Отличная кухня 4",
                        Description = "Описание кухни 4",
                        ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/foWx/771H2QoSZ",
                        Price = 89999.99m
                    }
                );
        }

        public DbSet<Product> Products { get; set; }
    }
}
