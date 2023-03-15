using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DBManager.Models
{
    public partial class GestioneMagazzinoContext : DbContext
    {
        public GestioneMagazzinoContext()
        {
        }

        public GestioneMagazzinoContext(DbContextOptions<GestioneMagazzinoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DrinkIngredient> DrinkIngredients { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<IsUsedValue> IsUsedValues { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuPreparation> MenuPreparations { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<UnitsOfMesure> UnitsOfMesures { get; set; } = null!;
        public virtual DbSet<UseMaterial> UseMaterials { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=172.17.64.1,5432;Database=GestioneMagazzino;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrinkIngredient>(entity =>
            {
                entity.ToTable("Drink_Ingredients", "dbo");

                entity.Property(e => e.ActualQuantity)
                    .HasColumnName("Actual_Quantity")
                    .HasDefaultValueSql("'-1'::integer");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Cost)
                    .HasPrecision(20, 2)
                    .HasColumnName("Cost__");

                entity.Property(e => e.CostDifference).HasPrecision(21, 2);

                entity.Property(e => e.CostLiter)
                    .HasPrecision(40, 20)
                    .HasColumnName("Cost___Liter");

                entity.Property(e => e.CostUnit)
                    .HasPrecision(40, 20)
                    .HasColumnName("Cost___Unit");

                entity.Property(e => e.DrinkName)
                    .HasMaxLength(255)
                    .HasColumnName("Drink_Name");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OldCost)
                    .HasPrecision(20, 2)
                    .HasColumnName("OldCost__");

                entity.Property(e => e.QuantityNeeded).HasColumnName("Quantity_Needed");

                entity.Property(e => e.SizeLiters)
                    .HasPrecision(20, 2)
                    .HasColumnName("Size_Liters")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.SizeUnits)
                    .HasPrecision(20, 2)
                    .HasColumnName("Size_Units")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredients", "dbo");

                entity.Property(e => e.ActualQuantity)
                    .HasColumnName("Actual_Quantity")
                    .HasDefaultValueSql("'-1'::integer");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Cost)
                    .HasPrecision(20, 2)
                    .HasColumnName("Cost__");

                entity.Property(e => e.CostDifference).HasPrecision(21, 2);

                entity.Property(e => e.CostKg)
                    .HasPrecision(40, 19)
                    .HasColumnName("Cost___Kg");

                entity.Property(e => e.CostUnit)
                    .HasPrecision(31, 13)
                    .HasColumnName("Cost___Unit");

                entity.Property(e => e.Ingredient1)
                    .HasMaxLength(150)
                    .HasColumnName("Ingredient");

                entity.Property(e => e.IsUsed).HasColumnName("Is_Used");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OldCost)
                    .HasPrecision(20, 2)
                    .HasColumnName("OldCost__");

                entity.Property(e => e.QuantityNeeded).HasColumnName("Quantity_Needed");

                entity.Property(e => e.SizeKg)
                    .HasPrecision(20, 3)
                    .HasColumnName("Size_Kg")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.SizeUnits).HasColumnName("Size_Units");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");
            });

            modelBuilder.Entity<IsUsedValue>(entity =>
            {
                entity.ToTable("IsUsed_Values", "dbo");

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu", "dbo");

                entity.Property(e => e.Category).HasMaxLength(255);

                entity.Property(e => e.MenuEntry)
                    .HasMaxLength(255)
                    .HasColumnName("Menu_Entry");

                entity.Property(e => e.SellingPrice).HasPrecision(20, 2);
            });

            modelBuilder.Entity<MenuPreparation>(entity =>
            {
                entity.HasKey(e => e.EntryId);

                entity.ToTable("MenuPreparations", "dbo");

                entity.Property(e => e.EntryId).HasColumnName("Entry_Id");

                entity.Property(e => e.IngedientId).HasColumnName("Ingedient_Id");

                entity.Property(e => e.IngredientQuantity).HasPrecision(20, 2);

                entity.Property(e => e.MenuProductId).HasColumnName("Menu_Product_Id");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Suppliers", "dbo");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.PtIva)
                    .HasMaxLength(50)
                    .HasColumnName("PT_IVA");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(150)
                    .HasColumnName("Supplier_Name");

                entity.Property(e => e.Telefono).HasMaxLength(50);
            });

            modelBuilder.Entity<UnitsOfMesure>(entity =>
            {
                entity.ToTable("UnitsOfMesure", "dbo");

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<UseMaterial>(entity =>
            {
                entity.ToTable("Use_Materials", "dbo");

                entity.Property(e => e.ActualQuantity)
                    .HasColumnName("Actual_Quantity")
                    .HasDefaultValueSql("'-1'::integer");

                entity.Property(e => e.Category).HasMaxLength(255);

                entity.Property(e => e.Cost)
                    .HasPrecision(20, 2)
                    .HasColumnName("Cost__");

                entity.Property(e => e.CostDifference).HasPrecision(21, 2);

                entity.Property(e => e.CostUnit)
                    .HasPrecision(40, 20)
                    .HasColumnName("Cost___Unit");

                entity.Property(e => e.MaterialName)
                    .HasMaxLength(255)
                    .HasColumnName("Material_Name");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OldCost)
                    .HasPrecision(20, 2)
                    .HasColumnName("OldCost__");

                entity.Property(e => e.QuantityNeeded).HasColumnName("Quantity_Needed");

                entity.Property(e => e.SizeUnits)
                    .HasPrecision(20, 2)
                    .HasColumnName("Size_Units")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
