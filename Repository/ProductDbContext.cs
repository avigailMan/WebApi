using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Entities;
namespace Repository;

public partial class ProductDbContext : DbContext
{
    public ProductDbContext()
    {
    }

    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrederItem> OrederItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("Data Source=srv2\\PUPILS;Initial Catalog=Product_db;Trusted_Connection=True;TrustServerCertificate=True");
    }
      

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK_category");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedNever()
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)

                .IsFixedLength()
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Oreders__userid__45F365D3");
        });

        modelBuilder.Entity<OrederItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemid);

            entity.ToTable("Oreder_item");

            entity.Property(e => e.OrderItemid).HasColumnName("orderItemid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrederItems)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Oreder_it__order__4D94879B");

            entity.HasOne(d => d.Product).WithMany(p => p.OrederItems)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Oreder_it__produ__4CA06362");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("image");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("product_name");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__catego__3F466844");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Email)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
