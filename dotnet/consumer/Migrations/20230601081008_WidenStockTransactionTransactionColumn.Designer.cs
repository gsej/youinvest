﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using consumer;
using consumer.Database;

#nullable disable

namespace consumer.Migrations
{
    [DbContext(typeof(ConsumerDbContext))]
    [Migration("20230601081008_WidenStockTransactionTransactionColumn")]
    partial class WidenStockTransactionTransactionColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("consumer.Entities.CashStatementItem", b =>
                {
                    b.Property<Guid>("CashStatementItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("PaymentAmountGbp")
                        .HasPrecision(19, 5)
                        .HasColumnType("decimal(19,5)");

                    b.Property<decimal>("ReceiptAmountGbp")
                        .HasPrecision(19, 5)
                        .HasColumnType("decimal(19,5)");

                    b.HasKey("CashStatementItemId");

                    b.ToTable("CashStatementItem", (string)null);
                });

            modelBuilder.Entity("consumer.Entities.Contributions", b =>
                {
                    b.Property<string>("Account")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalReceived")
                        .HasPrecision(19, 5)
                        .HasColumnType("decimal(19,5)");

                    b.HasKey("Account", "Year");

                    b.ToTable("Contributions", (string)null);
                });

            modelBuilder.Entity("consumer.Entities.Dividends", b =>
                {
                    b.Property<string>("Account")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalReceived")
                        .HasPrecision(19, 5)
                        .HasColumnType("decimal(19,5)");

                    b.HasKey("Account", "Year");

                    b.ToTable("Dividends", (string)null);
                });

            modelBuilder.Entity("consumer.Entities.StockTransaction", b =>
                {
                    b.Property<Guid>("StockTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("newid()");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("AmountGbp")
                        .HasPrecision(19, 5)
                        .HasColumnType("decimal(19,5)");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Transaction")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StockTransactionId");

                    b.ToTable("StockTransaction", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
