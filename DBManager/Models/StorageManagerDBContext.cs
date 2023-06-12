using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBManager.Models
{
    public partial class StorageManagerDBContext : DbContext
    {
        public StorageManagerDBContext()
        {
        }

        public StorageManagerDBContext(DbContextOptions<StorageManagerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryIngredientList> CategoryIngredientLists { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<IngredientsFormat> IngredientsFormats { get; set; } = null!;
        public virtual DbSet<IsUsedValue> IsUsedValues { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderCategory> OrderCategories { get; set; } = null!;
        public virtual DbSet<OrdersList> OrdersLists { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductComposition> ProductCompositions { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<UnitsOfMeasure> UnitsOfMeasures { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=10.147.18.219,5432;Database=StorageManagerDB;Username=StorageManagerAPP;Password=StorageManagerAPP");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryIngredientList>(entity =>
            {
                entity.HasKey(e => e.EntryId)
                    .HasName("PK_CategoryList");

                entity.ToTable("CategoryIngredientList");

                entity.Property(e => e.EntryId).UseIdentityAlwaysColumn();

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.IngredientId).HasColumnName("Ingredient_id");

                entity.Property(e => e.Quantity).HasDefaultValueSql("1");

                entity.Property(e => e.SelectedFormatId).HasColumnName("SelectedFormat_Id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryIngredientLists)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Category");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.CategoryIngredientLists)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ingredients");

                entity.HasOne(d => d.SelectedFormat)
                    .WithMany(p => p.CategoryIngredientLists)
                    .HasForeignKey(d => d.SelectedFormatId)
                    .HasConstraintName("fk_Format");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.IsEnough).HasComputedColumnSql("\nCASE\n    WHEN (\"QuantityNeeded\" > \"ActualQuantity\") THEN false\n    ELSE true\nEND", true);

                entity.HasOne(d => d.IsUsedValueNavigation)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.IsUsedValue)
                    .HasConstraintName("fk_UsedValue");
            });

            modelBuilder.Entity<IngredientsFormat>(entity =>
            {
                entity.ToTable("Ingredients_Format");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Cost).HasColumnName("Cost_€");

                entity.Property(e => e.CostDifference).HasComputedColumnSql("(\"Cost_€\" - \"PastCost1\")", true);

                entity.Property(e => e.CostKg)
                    .HasColumnName("Cost_€/Kg")
                    .HasComputedColumnSql("(\"Cost_€\" / \"Size_Kg\")", true);

                entity.Property(e => e.CostUnit)
                    .HasColumnName("Cost_€/Unit")
                    .HasComputedColumnSql("(\"Cost_€\" / (\"Size_Unit\")::numeric)", true);

                entity.Property(e => e.IngredientId).HasColumnName("Ingredient_Id");

                entity.Property(e => e.PastCost1).HasDefaultValueSql("0");

                entity.Property(e => e.SizeKg).HasColumnName("Size_Kg");

                entity.Property(e => e.SizeUnit)
                    .HasColumnName("Size_Unit")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.IngredientsFormats)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ingredients");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.IngredientsFormats)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Supplier");
            });

            modelBuilder.Entity<IsUsedValue>(entity =>
            {
                entity.ToTable("IsUsedValue");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.OrderDateTime).HasColumnType("timestamp without time zone");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Suppliers");
            });

            modelBuilder.Entity<OrderCategory>(entity =>
            {
                entity.ToTable("OrderCategory");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            });

            modelBuilder.Entity<OrdersList>(entity =>
            {
                entity.HasKey(e => e.EntryId)
                    .HasName("pk_OrderList");

                entity.ToTable("OrdersList");

                entity.Property(e => e.EntryId)
                    .HasColumnName("Entry_Id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IngredientId).HasColumnName("Ingredient_Id");

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.Quantity).HasDefaultValueSql("1");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.OrdersLists)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ingredient");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersLists)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Order");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.ProductCategory).HasColumnName("Product_Category");

                entity.Property(e => e.ProductCost).HasColumnName("Product_Cost");

                entity.Property(e => e.ProductName).HasColumnName("Product_Name");

                entity.Property(e => e.ProductPrice).HasColumnName("Product_Price");
            });

            modelBuilder.Entity<ProductComposition>(entity =>
            {
                entity.ToTable("Product_Composition");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.IngredientId).HasColumnName("Ingredient_Id");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.Quantity).HasDefaultValueSql("1");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.ProductCompositions)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Ingredient_Id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCompositions)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Id");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.PtIva).HasColumnName("PT_IVA");

                entity.Property(e => e.SupplierName).HasColumnName("Supplier_Name");
            });

            modelBuilder.Entity<UnitsOfMeasure>(entity =>
            {
                entity.ToTable("UnitsOfMeasure");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
