using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Revenge.Infrestructure.Entities;

namespace Revenge.Data.Context
{
    public sealed class RevengeDbContext : DbContext
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
                entity.HasKey(e => e.Id).HasName("cartitems_pkey");

                entity.ToTable("cartitems");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Addedat)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("addedat");
                entity.Property(e => e.Cartid).HasColumnName("cartid");
                entity.Property(e => e.Productid).HasColumnName("productid");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");

                entity.HasOne(d => d.Cart).WithMany(p => p.Cartitems)
                .HasForeignKey(d => d.Cartid)
                .HasConstraintName("cartitems_cartid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Cartitems)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("cartitems_productid_fkey");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("categories_pkey");

                entity.ToTable("categories");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Createdat)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("createdat");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("invoices_pkey");

                entity.ToTable("invoices");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Issuedat)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("issuedat");
                entity.Property(e => e.Notes).HasColumnName("notes");
                entity.Property(e => e.Orderid).HasColumnName("orderid");
                entity.Property(e => e.Tax)
                    .HasPrecision(12, 2)
                    .HasDefaultValueSql("0")
                    .HasColumnName("tax");
                entity.Property(e => e.Total)
                    .HasPrecision(12, 2)
                    .HasColumnName("total");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");
                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoices_orderid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("invoices_userid_fkey");
            });

            modelBuilder.Entity<Invoiceitem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("invoiceitems_pkey");

                entity.ToTable("invoiceitems");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.Invoiceid).HasColumnName("invoiceid");
                entity.Property(e => e.Productid).HasColumnName("productid");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Subtotal)
                    .HasPrecision(12, 2)
                    .HasColumnName("subtotal");
                entity.Property(e => e.Unitprice)
                    .HasPrecision(12, 2)
                    .HasColumnName("unitprice");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");

                entity.HasOne(d => d.Invoice).WithMany(p => p.Invoiceitems)
                    .HasForeignKey(d => d.Invoiceid)
                    .HasConstraintName("invoiceitems_invoiceid_fkey");

                entity.HasOne(d => d.Product).WithMany(p => p.Invoiceitems)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("invoiceitems_productid_fkey");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("orders_pkey");

                entity.ToTable("orders");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Addressid).HasColumnName("addressid");
                entity.Property(e => e.Placedat)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("placedat");
                entity.Property(e => e.Total)
                    .HasPrecision(12, 2)
                    .HasColumnName("total");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");
                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("orders_userid_fkey");
            });

            modelBuilder.Entity<Orderitem>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("orderitems_pkey");

                entity.ToTable("orderitems");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Orderid).HasColumnName("orderid");
                entity.Property(e => e.Productid).HasColumnName("productid");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Subtotal)
                    .HasPrecision(12, 2)
                    .HasColumnName("subtotal");
                entity.Property(e => e.Unitprice)
                    .HasPrecision(12, 2)
                    .HasColumnName("unitprice");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");

                entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.Orderid)
                    .HasConstraintName("orderitems_orderid_fkey");

                entity.HasOne(d => d.Product).WithMany(p => p.Orderitems)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("orderitems_productid_fkey");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("payments_pkey");

                entity.ToTable("payments");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Amount)
                    .HasPrecision(12, 2)
                    .HasColumnName("amount");
                entity.Property(e => e.Createdat)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("createdat");
                entity.Property(e => e.Invoiceid).HasColumnName("invoiceid");
                entity.Property(e => e.Orderid).HasColumnName("orderid");
                entity.Property(e => e.Paidat).HasColumnName("paidat");
                entity.Property(e => e.Paymentmethodid).HasColumnName("paymentmethodid");
                entity.Property(e => e.Transactionreference)
                    .HasMaxLength(200)
                    .HasColumnName("transactionreference");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");
                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Invoice).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Invoiceid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("payments_invoiceid_fkey");

                entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Orderid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("payments_orderid_fkey");

                entity.HasOne(d => d.Paymentmethod).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Paymentmethodid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("payments_paymentmethodid_fkey");

                entity.HasOne(d => d.User).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("payments_userid_fkey");
            });

            modelBuilder.Entity<Paymentmethod>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("paymentmethods_pkey");

                entity.ToTable("paymentmethods");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Metadata)
                    .HasColumnType("json")
                    .HasColumnName("metadata");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.Provider)
                    .HasMaxLength(100)
                    .HasColumnName("provider");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("products_pkey");

                entity.ToTable("products");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .HasColumnName("brand");
                entity.Property(e => e.Categoryid).HasColumnName("categoryid");
                entity.Property(e => e.Createdat)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("createdat");
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
                entity.Property(e => e.Price)
                    .HasPrecision(12, 2)
                    .HasColumnName("price");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");
                entity.Property(e => e.Url).HasColumnName("url");

                entity.HasOne(d => d.Category).WithMany(p => p.Products)
                    .HasForeignKey(d => d.Categoryid)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("products_categoryid_fkey");
            });

            modelBuilder.Entity<Productimage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("productimages_pkey");

                entity.ToTable("productimages");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Alttext)
                    .HasMaxLength(150)
                    .HasColumnName("alttext");
                entity.Property(e => e.Isprimary)
                    .HasDefaultValue(false)
                    .HasColumnName("isprimary");
                entity.Property(e => e.Productid).HasColumnName("productid");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");
                entity.Property(e => e.Url).HasColumnName("url");

                entity.HasOne(d => d.Product).WithMany(p => p.Productimages)
                    .HasForeignKey(d => d.Productid)
                    .HasConstraintName("productimages_productid_fkey");
            });

            modelBuilder.Entity<Shoppingcart>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("shoppingcart_pkey");

                entity.ToTable("shoppingcart");

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Createdat)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("createdat");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");
                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User).WithMany(p => p.Shoppingcarts)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("shoppingcart_userid_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pkey");

                entity.ToTable("users");

                entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("id");
                entity.Property(e => e.Birthdate).HasColumnName("birthdate");
                entity.Property(e => e.Cellphone)
                    .HasMaxLength(20)
                    .HasColumnName("cellphone");
                entity.Property(e => e.Createdat)
                    .HasDefaultValueSql("now()")
                    .HasColumnName("createdat");
                entity.Property(e => e.Directions)
                    .HasColumnType("json")
                    .HasColumnName("directions");
                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");
                entity.Property(e => e.Numidentification).HasColumnName("numidentification");
                entity.Property(e => e.Updatedat).HasColumnName("updatedat");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}