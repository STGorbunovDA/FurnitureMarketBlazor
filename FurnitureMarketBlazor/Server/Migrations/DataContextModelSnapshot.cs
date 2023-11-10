﻿// <auto-generated />
using System;
using FurnitureMarketBlazor.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FurnitureMarketBlazor.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.CartFolder.CartItem", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProductId", "ProductTypeId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.OrderFolder.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.OrderFolder.OrderItem", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId", "ProductId", "ProductTypeId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.ProductsFolder.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Deleted = false,
                            Name = "Кухни",
                            Url = "kitchens",
                            Visible = true
                        },
                        new
                        {
                            Id = 2,
                            Deleted = false,
                            Name = "Мебель",
                            Url = "furniture",
                            Visible = true
                        },
                        new
                        {
                            Id = 3,
                            Deleted = false,
                            Name = "Санузел",
                            Url = "bathroom",
                            Visible = true
                        });
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.ProductsFolder.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Featured")
                        .HasColumnType("bit");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Описание кухни 1",
                            Featured = true,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/GWwc/CUW5BiG5e",
                            Title = "Отличная кухня 1"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Описание кухни 2",
                            Featured = false,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Mxek/CqEtuDEe6",
                            Title = "Отличная кухня 2"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Описание кухни 3",
                            Featured = false,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Xe88/Zj4mZ4pvP",
                            Title = "Отличная кухня 3"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Описание кухни 4",
                            Featured = false,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/foWx/771H2QoSZ",
                            Title = "Отличная кухня 4"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            Description = "Описание мебель 1",
                            Featured = false,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/53EG/r1KcH6Crf",
                            Title = "Отличная мебель 1"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 2,
                            Description = "Описание мебель 2",
                            Featured = true,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/9GtZ/3gqAyzbJs",
                            Title = "Отличная мебель 2"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 2,
                            Description = "Описание мебель 3",
                            Featured = false,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/5eAR/sS9etkMaD",
                            Title = "Отличная мебель 3"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 2,
                            Description = "Описание мебель 4",
                            Featured = false,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/t5cS/QGsHKccVa",
                            Title = "Отличная мебель 4"
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 3,
                            Description = "Отличный Санузел 1",
                            Featured = false,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/9zj5/hwRJU1eUw",
                            Title = "Отличный Санузел 1"
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 3,
                            Description = "Отличный Санузел 2",
                            Featured = true,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/KmZ4/tSXu8WCsa",
                            Title = "Отличный Санузел 2"
                        },
                        new
                        {
                            Id = 11,
                            CategoryId = 3,
                            Description = "Отличный Санузел 3",
                            Featured = false,
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/VarG/D2RfZ8hFd",
                            Title = "Отличный Санузел 3"
                        });
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.ProductsFolder.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Эконом"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Бюджет"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Стандарт"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Люкс"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Премиум"
                        });
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.ProductsFolder.ProductVariant", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId", "ProductTypeId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("ProductVariants");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            ProductTypeId = 1,
                            OriginalPrice = 0m,
                            Price = 90000.00m
                        },
                        new
                        {
                            ProductId = 1,
                            ProductTypeId = 2,
                            OriginalPrice = 120000.00m,
                            Price = 110000.00m
                        },
                        new
                        {
                            ProductId = 1,
                            ProductTypeId = 3,
                            OriginalPrice = 180000.00m,
                            Price = 150000.00m
                        },
                        new
                        {
                            ProductId = 1,
                            ProductTypeId = 4,
                            OriginalPrice = 250000.00m,
                            Price = 200000.00m
                        },
                        new
                        {
                            ProductId = 1,
                            ProductTypeId = 5,
                            OriginalPrice = 380000.00m,
                            Price = 300000.00m
                        },
                        new
                        {
                            ProductId = 2,
                            ProductTypeId = 1,
                            OriginalPrice = 0m,
                            Price = 70000.00m
                        },
                        new
                        {
                            ProductId = 2,
                            ProductTypeId = 2,
                            OriginalPrice = 110000.00m,
                            Price = 100000.00m
                        },
                        new
                        {
                            ProductId = 2,
                            ProductTypeId = 3,
                            OriginalPrice = 140000.00m,
                            Price = 120000.00m
                        },
                        new
                        {
                            ProductId = 2,
                            ProductTypeId = 4,
                            OriginalPrice = 200000.00m,
                            Price = 150000.00m
                        },
                        new
                        {
                            ProductId = 2,
                            ProductTypeId = 5,
                            OriginalPrice = 260000.00m,
                            Price = 180000.00m
                        },
                        new
                        {
                            ProductId = 3,
                            ProductTypeId = 1,
                            OriginalPrice = 0m,
                            Price = 90000.00m
                        },
                        new
                        {
                            ProductId = 3,
                            ProductTypeId = 2,
                            OriginalPrice = 150000.00m,
                            Price = 130000.00m
                        },
                        new
                        {
                            ProductId = 3,
                            ProductTypeId = 3,
                            OriginalPrice = 210000.00m,
                            Price = 180000.00m
                        },
                        new
                        {
                            ProductId = 3,
                            ProductTypeId = 4,
                            OriginalPrice = 240000.00m,
                            Price = 210000.00m
                        },
                        new
                        {
                            ProductId = 3,
                            ProductTypeId = 5,
                            OriginalPrice = 320000.00m,
                            Price = 240000.00m
                        },
                        new
                        {
                            ProductId = 4,
                            ProductTypeId = 1,
                            OriginalPrice = 0m,
                            Price = 75000.00m
                        },
                        new
                        {
                            ProductId = 4,
                            ProductTypeId = 2,
                            OriginalPrice = 130000.00m,
                            Price = 115000.00m
                        },
                        new
                        {
                            ProductId = 4,
                            ProductTypeId = 3,
                            OriginalPrice = 210000.00m,
                            Price = 160000.00m
                        },
                        new
                        {
                            ProductId = 4,
                            ProductTypeId = 4,
                            OriginalPrice = 290000.00m,
                            Price = 240000.00m
                        },
                        new
                        {
                            ProductId = 4,
                            ProductTypeId = 5,
                            OriginalPrice = 390000.00m,
                            Price = 340000.00m
                        },
                        new
                        {
                            ProductId = 5,
                            ProductTypeId = 3,
                            OriginalPrice = 0m,
                            Price = 60000.00m
                        },
                        new
                        {
                            ProductId = 6,
                            ProductTypeId = 3,
                            OriginalPrice = 0m,
                            Price = 50000.00m
                        },
                        new
                        {
                            ProductId = 7,
                            ProductTypeId = 3,
                            OriginalPrice = 0m,
                            Price = 80000.00m
                        },
                        new
                        {
                            ProductId = 8,
                            ProductTypeId = 3,
                            OriginalPrice = 0m,
                            Price = 99000.00m
                        },
                        new
                        {
                            ProductId = 9,
                            ProductTypeId = 3,
                            OriginalPrice = 0m,
                            Price = 50000.00m
                        },
                        new
                        {
                            ProductId = 10,
                            ProductTypeId = 3,
                            OriginalPrice = 0m,
                            Price = 40000.00m
                        },
                        new
                        {
                            ProductId = 11,
                            ProductTypeId = 3,
                            OriginalPrice = 0m,
                            Price = 50000.00m
                        });
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.UserFolder.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.UserFolder.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.OrderFolder.OrderItem", b =>
                {
                    b.HasOne("FurnitureMarketBlazor.Shared.OrderFolder.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FurnitureMarketBlazor.Shared.ProductsFolder.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FurnitureMarketBlazor.Shared.ProductsFolder.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.ProductsFolder.Product", b =>
                {
                    b.HasOne("FurnitureMarketBlazor.Shared.ProductsFolder.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.ProductsFolder.ProductVariant", b =>
                {
                    b.HasOne("FurnitureMarketBlazor.Shared.ProductsFolder.Product", "Product")
                        .WithMany("Variants")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FurnitureMarketBlazor.Shared.ProductsFolder.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.UserFolder.Address", b =>
                {
                    b.HasOne("FurnitureMarketBlazor.Shared.UserFolder.User", null)
                        .WithOne("Address")
                        .HasForeignKey("FurnitureMarketBlazor.Shared.UserFolder.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.OrderFolder.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.ProductsFolder.Product", b =>
                {
                    b.Navigation("Variants");
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.UserFolder.User", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
