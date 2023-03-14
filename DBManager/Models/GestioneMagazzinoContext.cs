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
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=GestioneMagazzino;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DrinkIngredient>(entity =>
            {
                entity.ToTable("Drink_Ingredients");

                entity.Property(e => e.ActualQuantity)
                    .HasColumnName("Actual_Quantity")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Cost)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("Cost_€");

                entity.Property(e => e.CostDifference)
                    .HasColumnType("numeric(19, 2)")
                    .HasComputedColumnSql("([OldCost_€]-[Cost_€])", true);

                entity.Property(e => e.CostLiter)
                    .HasColumnType("numeric(38, 20)")
                    .HasColumnName("Cost_€/Liter")
                    .HasComputedColumnSql("([Cost_€]/[Size_Liters])", false);

                entity.Property(e => e.CostUnit)
                    .HasColumnType("numeric(38, 20)")
                    .HasColumnName("Cost_€/Unit")
                    .HasComputedColumnSql("([Cost_€]/[Size_Units])", false);

                entity.Property(e => e.DrinkName)
                    .HasMaxLength(255)
                    .HasColumnName("Drink_Name");

                entity.Property(e => e.IsEnough).HasComputedColumnSql("(case when [Quantity_Needed]>[Actual_Quantity] then (0) when [Quantity_Needed]<=[Actual_Quantity] then (1)  end)", true);

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OldCost)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("OldCost_€");

                entity.Property(e => e.QuantityNeeded).HasColumnName("Quantity_Needed");

                entity.Property(e => e.SizeLiters)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("Size_Liters")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SizeUnits)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("Size_Units")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

                entity.HasOne(d => d.IsUsedNavigation)
                    .WithMany(p => p.DrinkIngredients)
                    .HasForeignKey(d => d.IsUsed)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Drink_Ingredients_IsUsed_Values");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.DrinkIngredients)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Drink_Ingredients_Suppliers");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.Property(e => e.ActualQuantity)
                    .HasColumnName("Actual_Quantity")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Cost)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("Cost_€");

                entity.Property(e => e.CostDifference)
                    .HasColumnType("numeric(19, 2)")
                    .HasComputedColumnSql("([OldCost_€]-[Cost_€])", true);

                entity.Property(e => e.CostKg)
                    .HasColumnType("numeric(38, 19)")
                    .HasColumnName("Cost_€/Kg")
                    .HasComputedColumnSql("([Cost_€]/[Size_Kg])", true);

                entity.Property(e => e.CostUnit)
                    .HasColumnType("numeric(29, 13)")
                    .HasColumnName("Cost_€/Unit")
                    .HasComputedColumnSql("([Cost_€]/[Size_Units])", true);

                entity.Property(e => e.Ingredient1)
                    .HasMaxLength(150)
                    .HasColumnName("Ingredient");

                entity.Property(e => e.IsEnough).HasComputedColumnSql("(case when [Quantity_Needed]>[Actual_Quantity] then (0) when [Quantity_Needed]<=[Actual_Quantity] then (1)  end)", true);

                entity.Property(e => e.IsUsed).HasColumnName("Is_Used");

                entity.Property(e => e.LastOrderDateTime).HasColumnType("date");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OldCost)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("OldCost_€");

                entity.Property(e => e.QuantityNeeded).HasColumnName("Quantity_Needed");

                entity.Property(e => e.SizeKg)
                    .HasColumnType("numeric(18, 3)")
                    .HasColumnName("Size_Kg")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SizeUnits).HasColumnName("Size_Units");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

                entity.HasOne(d => d.IsUsedNavigation)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.IsUsed)
                    .HasConstraintName("FK_Ingredients_IsUsed_Values");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingredients_Suppliers");
            });

            modelBuilder.Entity<IsUsedValue>(entity =>
            {
                entity.ToTable("IsUsed_Values");

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.Category).HasMaxLength(255);

                entity.Property(e => e.MenuEntry)
                    .HasMaxLength(255)
                    .HasColumnName("Menu_Entry");

                entity.Property(e => e.SellingPrice).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<MenuPreparation>(entity =>
            {
                entity.HasKey(e => e.EntryId);

                entity.Property(e => e.EntryId).HasColumnName("Entry_Id");

                entity.Property(e => e.IngedientId).HasColumnName("Ingedient_Id");

                entity.Property(e => e.IngredientQuantity).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.MenuProductId).HasColumnName("Menu_Product_Id");

                entity.HasOne(d => d.MenuProduct)
                    .WithMany(p => p.MenuPreparations)
                    .HasForeignKey(d => d.MenuProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuPreparations_Menu");

                entity.HasOne(d => d.UnitOfMesureNavigation)
                    .WithMany(p => p.MenuPreparations)
                    .HasForeignKey(d => d.UnitOfMesure)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuPreparations_UnitsOfMesure");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
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
                entity.ToTable("UnitsOfMesure");

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<UseMaterial>(entity =>
            {
                entity.ToTable("Use_Materials");

                entity.Property(e => e.ActualQuantity)
                    .HasColumnName("Actual_Quantity")
                    .HasDefaultValueSql("((-1))");

                entity.Property(e => e.Category).HasMaxLength(255);

                entity.Property(e => e.Cost)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("Cost_€");

                entity.Property(e => e.CostDifference)
                    .HasColumnType("numeric(19, 2)")
                    .HasComputedColumnSql("([OldCost_€]-[Cost_€])", true);

                entity.Property(e => e.CostUnit)
                    .HasColumnType("numeric(38, 20)")
                    .HasColumnName("Cost_€/Unit")
                    .HasComputedColumnSql("([Cost_€]/[Size_Units])", false);

                entity.Property(e => e.IsEnough).HasComputedColumnSql("(case when [Quantity_Needed]>[Actual_Quantity] then (0) when [Quantity_Needed]<=[Actual_Quantity] then (1)  end)", true);

                entity.Property(e => e.MaterialName)
                    .HasMaxLength(255)
                    .HasColumnName("Material_Name");

                entity.Property(e => e.Notes).HasMaxLength(255);

                entity.Property(e => e.OldCost)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("OldCost_€");

                entity.Property(e => e.QuantityNeeded).HasColumnName("Quantity_Needed");

                entity.Property(e => e.SizeUnits)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("Size_Units")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

                entity.HasOne(d => d.IsUsedNavigation)
                    .WithMany(p => p.UseMaterials)
                    .HasForeignKey(d => d.IsUsed)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Use_Materials_IsUsed_Values");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.UseMaterials)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Use_Materials_Suppliers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
