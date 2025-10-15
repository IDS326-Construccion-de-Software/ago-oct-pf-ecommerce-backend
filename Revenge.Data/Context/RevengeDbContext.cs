using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Revenge.Infrestructure.Entities;

namespace Revenge.Data.Context
{
    public class RevengeDbContext : DbContext
    {
        public RevengeDbContext()
        {
        }

        public RevengeDbContext(DbContextOptions<RevengeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cartitem> Cartitems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Orderitem> Orderitems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Paymentmethod> Paymentmethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Productimage> Productimages { get; set; }
        public DbSet<Shoppingcart> Shoppingcarts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasPostgresEnum("orderstatus", new[] { "pending", "processing", "completed", "cancelled" })
                .HasPostgresEnum("paymentstatus", new[] { "pending", "completed", "failed", "refunded" })
                .HasPostgresEnum("typeCategory", new[] { "snacks", "pastry", "frozen", "drinks", "fish", "meats", "dairy", "pets", "babies", "bakery", "deliMeats" })
                .HasPostgresExtension("pgcrypto");

            // CARTITEMS
            modelBuilder.Entity<Cartitem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("cartitems_pkey");
                entity.ToTable("cartitems");

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.AddedAt).HasDefaultValueSql("now()").HasColumnName("AddedAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
                entity.Property(e => e.CartId).HasColumnName("CartId");
                entity.Property(e => e.ProductId).HasColumnName("ProductId");
                entity.Property(e => e.Quantity).HasColumnName("Quantity");

                entity.HasOne(d => d.Cart).WithMany(p => p.Cartitems)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("cartitems_cartid_fkey");

                entity.HasOne(d => d.Product).WithMany(p => p.Cartitems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("cartitems_productid_fkey");
            });

            // CATEGORIES
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("categories_pkey");
                entity.ToTable("categories");

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("Name");
                entity.Property(e => e.Description).HasMaxLength(255).HasColumnName("Description");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()").HasColumnName("CreatedAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
            });

            // INVOICES
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("invoices_pkey");
                entity.ToTable("invoices");

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.IssuedAt).HasDefaultValueSql("now()").HasColumnName("IssuedAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
                entity.Property(e => e.Url).HasColumnName("Url");
                entity.Property(e => e.OrderId).HasColumnName("OrderId");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.Tax).HasPrecision(12, 2).HasDefaultValueSql("0").HasColumnName("Tax");
                entity.Property(e => e.Total).HasPrecision(12, 2).HasColumnName("Total");

                entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("invoices_orderid_fkey");

                entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("invoices_userid_fkey");
            });

            // ORDERS
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("orders_pkey");
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.AddressId).HasColumnName("AddressId");
                entity.Property(e => e.PlacedAt).HasDefaultValueSql("now()").HasColumnName("PlacedAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
                entity.Property(e => e.Total).HasPrecision(12, 2).HasColumnName("Total");

                entity.HasOne(d => d.User).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("orders_userid_fkey");
            });

            // ORDERITEMS
            modelBuilder.Entity<Orderitem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("orderitems_pkey");
                entity.ToTable("orderitems");

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.OrderId).HasColumnName("OrderId");
                entity.Property(e => e.ProductId).HasColumnName("ProductId");
                entity.Property(e => e.Quantity).HasColumnName("Quantity");
                entity.Property(e => e.Subtotal).HasPrecision(12, 2).HasColumnName("Subtotal");
                entity.Property(e => e.UnitPrice).HasPrecision(12, 2).HasColumnName("UnitPrice");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");

                entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("orderitems_orderid_fkey");

                entity.HasOne(d => d.Product).WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("orderitems_productid_fkey");
            });

            // PAYMENTS
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("payments_pkey");
                entity.ToTable("payments");

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceId");
                entity.Property(e => e.OrderId).HasColumnName("OrderId");
                entity.Property(e => e.PaymentMethodId).HasColumnName("PaymentMethodId");
                entity.Property(e => e.Amount).HasPrecision(12, 2).HasColumnName("Amount");
                entity.Property(e => e.TransactionReference).HasMaxLength(200).HasColumnName("TransactionReference");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()").HasColumnName("CreatedAt");
                entity.Property(e => e.PaidAt).HasColumnName("PaidAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");

                entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("payments_invoiceid_fkey");

                entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("payments_orderid_fkey");

                entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("payments_paymentmethodid_fkey");

                entity.HasOne(d => d.User).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("payments_userid_fkey");
            });

            // PAYMENTMETHODS
            modelBuilder.Entity<Paymentmethod>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("paymentmethods_pkey");
                entity.ToTable("paymentmethods");

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("Name");
                entity.Property(e => e.Provider).HasMaxLength(100).HasColumnName("Provider");
                entity.Property(e => e.Metadata).HasColumnType("json").HasColumnName("Metadata");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
            });

            // PRODUCTS
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("products_pkey");
                entity.ToTable("products");

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryId");
                entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("Name");
                entity.Property(e => e.Brand).HasMaxLength(50).HasColumnName("Brand");
                entity.Property(e => e.Description).HasMaxLength(500).HasColumnName("Description");
                entity.Property(e => e.Price).HasPrecision(12, 2).HasColumnName("Price");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()").HasColumnName("CreatedAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");

                entity.HasOne(d => d.Category).WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("products_categoryid_fkey");
            });

            // PRODUCTIMAGES
            modelBuilder.Entity<Productimage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("productimages_pkey");
                entity.ToTable("productimages");

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.ProductId).HasColumnName("ProductId");
                entity.Property(e => e.Url).HasColumnName("Url");
                entity.Property(e => e.IsPrimary).HasDefaultValue(false).HasColumnName("IsPrimary");
                entity.Property(e => e.Order).HasColumnName("Order");

                entity.HasOne(d => d.Product).WithMany(p => p.Productimages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("productimages_productid_fkey");
            });

            // SHOPPINGCART
            modelBuilder.Entity<Shoppingcart>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("shoppingcart_pkey");
                entity.ToTable("shoppingcart");

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()").HasColumnName("CreatedAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");

                entity.HasOne(d => d.User).WithMany(p => p.Shoppingcarts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("shoppingcart_userid_fkey");
            });

            // USERS
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pkey");
                entity.ToTable("users");
                entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("Id");
                entity.Property(e => e.Name).HasMaxLength(100).HasColumnName("Name");
                entity.Property(e => e.Email).HasMaxLength(150).HasColumnName("Email");
                entity.Property(e => e.Password).HasMaxLength(255).HasColumnName("Password");
                entity.Property(e => e.Cellphone).HasMaxLength(20).HasColumnName("Cellphone");
                entity.Property(e => e.Birthdate).HasColumnName("Birthdate");
                entity.Property(e => e.NumIdentification).HasColumnName("NumIdentification");
                entity.Property(e => e.Directions).HasColumnType("json").HasColumnName("Directions");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()").HasColumnName("CreatedAt");
                entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}