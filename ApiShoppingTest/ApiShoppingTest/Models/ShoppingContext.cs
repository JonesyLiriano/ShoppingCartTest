using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiShoppingTest.Models
{
    public partial class ShoppingContext : DbContext
    {
        public ShoppingContext()
        {
        }

        public ShoppingContext(DbContextOptions<ShoppingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<InvoiceHeader> InvoiceHeader { get; set; }
        public virtual DbSet<InvoiceItems> InvoiceItems { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductsActivity> ProductsActivity { get; set; }
        public virtual DbSet<ProductsCategory> ProductsCategory { get; set; }
        public virtual DbSet<ShoppingCartHeader> ShoppingCartHeader { get; set; }
        public virtual DbSet<ShoppingCartItems> ShoppingCartItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Shopping;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.CreditLimit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DocumentNumber)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<InvoiceHeader>(entity =>
            {
                entity.Property(e => e.InvoiceAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InvoiceDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.InvoiceHeader)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceHeader_Customers");
            });

            modelBuilder.Entity<InvoiceItems>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdInvoiceNavigation)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.IdInvoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceItems_InvoiceHeader");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InvoiceItems_Products");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IdProductCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdProductCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ProductsCategory");
            });

            modelBuilder.Entity<ProductsActivity>(entity =>
            {
                entity.HasKey(e => new { e.IdProduct, e.IdCustomer, e.TypeTransaction, e.ActivityNumber });

                entity.Property(e => e.TypeTransaction)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ActivityNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ActivityAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ActivityDate).HasColumnType("datetime");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.ProductsActivity)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsActivity_Customers");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ProductsActivity)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsActivity_ProductsActivity");
            });

            modelBuilder.Entity<ProductsCategory>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<ShoppingCartHeader>(entity =>
            {
                entity.Property(e => e.OrderAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.ShoppingCartHeader)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartHeader_Customers");
            });

            modelBuilder.Entity<ShoppingCartItems>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.ShoppingCartItems)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartItems_ShoppingCartHeader");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.ShoppingCartItems)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShoppingCartItems_Products");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
