﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using test_do_projektu.Data;

#nullable disable

namespace Sklep_Internetowy_.NET.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250116084701_ProductSale")]
    partial class ProductSale
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-rc.2.24474.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedData")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("integer");

                    b.Property<int?>("ShippingMethodId")
                        .HasColumnType("integer");

                    b.Property<int?>("StatusId")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("ShippingMethodId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StatusName = "Nowe"
                        },
                        new
                        {
                            Id = 2,
                            StatusName = "W trakcie realizacji"
                        },
                        new
                        {
                            Id = 3,
                            StatusName = "Zrealizowane"
                        },
                        new
                        {
                            Id = 4,
                            StatusName = "Anulowane"
                        });
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MethodName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethods");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MethodName = "Blik"
                        },
                        new
                        {
                            Id = 2,
                            MethodName = "Przelew bankowy"
                        },
                        new
                        {
                            Id = 3,
                            MethodName = "PayU"
                        });
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedData")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsOnSale")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("PreviousPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.ShippingMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ShippingName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ShippingMethods");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ShippingName = "InPost"
                        },
                        new
                        {
                            Id = 2,
                            ShippingName = "DHL"
                        },
                        new
                        {
                            Id = 3,
                            ShippingName = "Odbiór osobisty"
                        });
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.Order", b =>
                {
                    b.HasOne("Sklep_Internetowy_.NET.Models.Entity.PaymentMethod", "PaymentMethod")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentMethodId");

                    b.HasOne("Sklep_Internetowy_.NET.Models.Entity.ShippingMethod", "ShippingMethod")
                        .WithMany("Orders")
                        .HasForeignKey("ShippingMethodId");

                    b.HasOne("Sklep_Internetowy_.NET.Models.Entity.OrderStatus", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId");

                    b.HasOne("Sklep_Internetowy_.NET.Models.Entity.User", "User")
                        .WithMany("Ordes")
                        .HasForeignKey("UserId");

                    b.Navigation("PaymentMethod");

                    b.Navigation("ShippingMethod");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.OrderProduct", b =>
                {
                    b.HasOne("Sklep_Internetowy_.NET.Models.Entity.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sklep_Internetowy_.NET.Models.Entity.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.PaymentMethod", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.ShippingMethod", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Sklep_Internetowy_.NET.Models.Entity.User", b =>
                {
                    b.Navigation("Ordes");
                });
#pragma warning restore 612, 618
        }
    }
}
