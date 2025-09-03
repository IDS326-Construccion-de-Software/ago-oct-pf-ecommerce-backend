using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Revenge.Infrestructure.Entities;

namespace Revenge.Data.Context
{
    public sealed class RevengeDbContext : DbContext
    {
        public RevengeDbContext(DbContextOptions<RevengeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cartitem> Cartitems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Invoiceitem> Invoiceitems { get; set; }
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

            modelBuilder.Entity<Cartitem>(entity =>
            {
                entity.HasKey(e => e.id).HasName("cartitems_pkey");

                entity.ToTable("cartitems");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.addedAt)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("addedat");
                entity.Property(e => e.cartId).HasColumnName("cartid");
                entity.Property(e => e.productId).HasColumnName("productid");
                entity.Property(e => e.quantity).HasColumnName("quantity");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");

                entity.HasOne(d => d.cart).WithMany(p => p.cartItems)
                .HasForeignKey(d => d.cartId)
                .HasConstraintName("cartitems_cartid_fkey");

                entity.HasOne(d => d.product).WithMany(p => p.cartItems)
                    .HasForeignKey(d => d.productId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("cartitems_productid_fkey");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.id).HasName("categories_pkey");

                entity.ToTable("categories");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.createdAt)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("createdat");
                entity.Property(e => e.description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.id).HasName("invoices_pkey");

                entity.ToTable("invoices");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.issuedAt)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("issuedat");
                entity.Property(e => e.notes).HasColumnName("notes");
                entity.Property(e => e.orderId).HasColumnName("orderid");
                entity.Property(e => e.tax)
                    .HasPrecision(12, 2)
                    .HasDefaultValueSql("0")
                    .HasColumnName("tax");
                entity.Property(e => e.total)
                    .HasPrecision(12, 2)
                    .HasColumnName("total");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");
                entity.Property(e => e.userId).HasColumnName("userid");

                entity.HasOne(d => d.order).WithMany(p => p.invoices)
                .HasForeignKey(d => d.orderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoices_orderid_fkey");

                entity.HasOne(d => d.user).WithMany(p => p.invoices)
                    .HasForeignKey(d => d.userId)
                    .HasConstraintName("invoices_userid_fkey");
            });

            modelBuilder.Entity<Invoiceitem>(entity =>
            {
                entity.HasKey(e => e.id).HasName("invoiceitems_pkey");

                entity.ToTable("invoiceitems");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.invoiceId).HasColumnName("invoiceid");
                entity.Property(e => e.productId).HasColumnName("productid");
                entity.Property(e => e.quantity).HasColumnName("quantity");
                entity.Property(e => e.subtotal)
                    .HasPrecision(12, 2)
                    .HasColumnName("subtotal");
                entity.Property(e => e.unitPrice)
                    .HasPrecision(12, 2)
                    .HasColumnName("unitprice");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");

                entity.HasOne(d => d.invoice).WithMany(p => p.invoiceItems)
                    .HasForeignKey(d => d.invoiceId)
                    .HasConstraintName("invoiceitems_invoiceid_fkey");

                entity.HasOne(d => d.product).WithMany(p => p.invoiceItems)
                    .HasForeignKey(d => d.productId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("invoiceitems_productid_fkey");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.id).HasName("orders_pkey");

                entity.ToTable("orders");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.addressId).HasColumnName("addressid");
                entity.Property(e => e.placedAt)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("placedat");
                entity.Property(e => e.total)
                    .HasPrecision(12, 2)
                    .HasColumnName("total");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");
                entity.Property(e => e.userId).HasColumnName("userid");

                entity.HasOne(d => d.user).WithMany(p => p.orders)
                    .HasForeignKey(d => d.userId)
                    .HasConstraintName("orders_userid_fkey");
            });

            modelBuilder.Entity<Orderitem>(entity =>
            {
                entity.HasKey(e => e.id).HasName("orderitems_pkey");

                entity.ToTable("orderitems");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.orderId).HasColumnName("orderid");
                entity.Property(e => e.productId).HasColumnName("productid");
                entity.Property(e => e.quantity).HasColumnName("quantity");
                entity.Property(e => e.subtotal)
                    .HasPrecision(12, 2)
                    .HasColumnName("subtotal");
                entity.Property(e => e.unitPrice)
                    .HasPrecision(12, 2)
                    .HasColumnName("unitprice");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");

                entity.HasOne(d => d.order).WithMany(p => p.orderItems)
                    .HasForeignKey(d => d.orderId)
                    .HasConstraintName("orderitems_orderid_fkey");

                entity.HasOne(d => d.product).WithMany(p => p.orderItems)
                    .HasForeignKey(d => d.productId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("orderitems_productid_fkey");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.id).HasName("payments_pkey");

                entity.ToTable("payments");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.amount)
                    .HasPrecision(12, 2)
                    .HasColumnName("amount");
                entity.Property(e => e.createdAt)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("createdat");
                entity.Property(e => e.invoiceId).HasColumnName("invoiceid");
                entity.Property(e => e.orderId).HasColumnName("orderid");
                entity.Property(e => e.paidAt).HasColumnName("paidat");
                entity.Property(e => e.paymentMethodId).HasColumnName("paymentmethodid");
                entity.Property(e => e.transactionReference)
                    .HasMaxLength(200)
                    .HasColumnName("transactionreference");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");
                entity.Property(e => e.userId).HasColumnName("userid");

                entity.HasOne(d => d.invoice).WithMany(p => p.payments)
                    .HasForeignKey(d => d.invoiceId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("payments_invoiceid_fkey");

                entity.HasOne(d => d.order).WithMany(p => p.payments)
                    .HasForeignKey(d => d.orderId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("payments_orderid_fkey");

                entity.HasOne(d => d.paymentMethod).WithMany(p => p.payments)
                    .HasForeignKey(d => d.paymentMethodId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("payments_paymentmethodid_fkey");

                entity.HasOne(d => d.user).WithMany(p => p.payments)
                    .HasForeignKey(d => d.userId)
                    .HasConstraintName("payments_userid_fkey");
            });

            modelBuilder.Entity<Paymentmethod>(entity =>
            {
                entity.HasKey(e => e.id).HasName("paymentmethods_pkey");

                entity.ToTable("paymentmethods");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.metadata)
                    .HasColumnType("json")
                    .HasColumnName("metadata");
                entity.Property(e => e.name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.provider)
                    .HasMaxLength(100)
                    .HasColumnName("provider");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.id).HasName("products_pkey");

                entity.ToTable("products");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.brand)
                    .HasMaxLength(50)
                    .HasColumnName("brand");
                entity.Property(e => e.categoryId).HasColumnName("categoryid");
                entity.Property(e => e.createdAt)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("createdat");
                entity.Property(e => e.description)
                    .HasMaxLength(500)
                    .HasColumnName("description");
                entity.Property(e => e.name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
                entity.Property(e => e.price)
                    .HasPrecision(12, 2)
                    .HasColumnName("price");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");
                entity.Property(e => e.url).HasColumnName("url");

                entity.HasOne(d => d.category).WithMany(p => p.products)
                    .HasForeignKey(d => d.categoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("products_categoryid_fkey");
            });

            modelBuilder.Entity<Productimage>(entity =>
            {
                entity.HasKey(e => e.id).HasName("productimages_pkey");

                entity.ToTable("productimages");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.altText)
                    .HasMaxLength(150)
                    .HasColumnName("alttext");
                entity.Property(e => e.isPrimary)
                    .HasDefaultValue(false)
                    .HasColumnName("isprimary");
                entity.Property(e => e.productId).HasColumnName("productid");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");
                entity.Property(e => e.url).HasColumnName("url");

                entity.HasOne(d => d.product).WithMany(p => p.productImages)
                    .HasForeignKey(d => d.productId)
                    .HasConstraintName("productimages_productid_fkey");
            });

            modelBuilder.Entity<Shoppingcart>(entity =>
            {
                entity.HasKey(e => e.id).HasName("shoppingcart_pkey");

                entity.ToTable("shoppingcart");

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.createdAt)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("createdat");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");
                entity.Property(e => e.userId).HasColumnName("userid");

                entity.HasOne(d => d.user).WithMany(p => p.shoppingCarts)
                    .HasForeignKey(d => d.userId)
                    .HasConstraintName("shoppingcart_userid_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.id).HasName("users_pkey");

                entity.ToTable("users");

                entity.HasIndex(e => e.email, "users_email_key").IsUnique();

                entity.Property(e => e.id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.birthdate).HasColumnName("birthdate");
                entity.Property(e => e.cellphone)
                    .HasMaxLength(20)
                    .HasColumnName("cellphone");
                entity.Property(e => e.createdAt)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("createdat");
                entity.Property(e => e.directions)
                    .HasColumnType("json")
                    .HasColumnName("directions");
                entity.Property(e => e.email)
                    .HasMaxLength(150)
                    .HasColumnName("email");
                entity.Property(e => e.name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
                entity.Property(e => e.password)
                    .HasMaxLength(255)
                    .HasColumnName("password");
                entity.Property(e => e.numIdentification).HasColumnName("numidentification");
                entity.Property(e => e.updatedAt).HasColumnName("updatedat");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}