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

            modelBuilder.Entity("FurnitureMarketBlazor.Shared.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

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

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Описание кухни 1",
                            ImageUrl = "https://cloud.mail.ru/public/GWwc/CUW5BiG5e",
                            Price = 389999.99m,
                            Title = "Отличная кухня 1"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Описание кухни 2",
                            ImageUrl = "https://cloud.mail.ru/public/Mxek/CqEtuDEe6",
                            Price = 189999.99m,
                            Title = "Отличная кухня 2"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Описание кухни 3",
                            ImageUrl = "https://cloud.mail.ru/public/foWx/771H2QoSZ",
                            Price = 289999.99m,
                            Title = "Отличная кухня 3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
