﻿using System;
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

        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<IngredientsFormat> IngredientsFormats { get; set; } = null!;
        public virtual DbSet<IsUsedValue> IsUsedValues { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<UnitsOfMeasure> UnitsOfMeasures { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=172.21.64.1,5432;Database=StorageManagerDB;Username=postgres;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}