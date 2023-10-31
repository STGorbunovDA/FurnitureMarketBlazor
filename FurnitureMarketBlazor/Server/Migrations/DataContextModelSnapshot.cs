﻿// <auto-generated />
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

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Кухни",
                            Url = "kitchens"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Мебель",
                            Url = "furniture"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Санузел",
                            Url = "bathroom"
                        });
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.Product", b =>
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

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

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
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/GWwc/CUW5BiG5e",
                            Price = 389999.99m,
                            Title = "Отличная кухня 1"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Описание кухни 2",
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Mxek/CqEtuDEe6",
                            Price = 180000.99m,
                            Title = "Отличная кухня 2"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Описание кухни 3",
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/Xe88/Zj4mZ4pvP",
                            Price = 301000.00m,
                            Title = "Отличная кухня 3"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Описание кухни 4",
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/foWx/771H2QoSZ",
                            Price = 90101.00m,
                            Title = "Отличная кухня 4"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            Description = "Описание мебель 1",
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/53EG/r1KcH6Crf",
                            Price = 22500.00m,
                            Title = "Отличная мебель 1"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 2,
                            Description = "Описание мебель 2",
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/9GtZ/3gqAyzbJs",
                            Price = 33000.00m,
                            Title = "Отличная мебель 2"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 2,
                            Description = "Описание мебель 3",
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/5eAR/sS9etkMaD",
                            Price = 44100.99m,
                            Title = "Отличная мебель 3"
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 2,
                            Description = "Описание мебель 4",
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/t5cS/QGsHKccVa",
                            Price = 64100.00m,
                            Title = "Отличная мебель 4"
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 3,
                            Description = "Отличный Санузел 1",
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/9zj5/hwRJU1eUw",
                            Price = 74300.10m,
                            Title = "Отличный Санузел 1"
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 3,
                            Description = "Отличный Санузел 2",
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/KmZ4/tSXu8WCsa",
                            Price = 74300.10m,
                            Title = "Отличный Санузел 2"
                        },
                        new
                        {
                            Id = 11,
                            CategoryId = 3,
                            Description = "Отличный Санузел 3",
                            ImageUrl = "https://thumb.cloud.mail.ru/weblink/thumb/xw1/VarG/D2RfZ8hFd",
                            Price = 48800.10m,
                            Title = "Отличный Санузел 3"
                        });
                });

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.Product", b =>
                {
                    b.HasOne("FurnitureMarketBlazor.Shared.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
