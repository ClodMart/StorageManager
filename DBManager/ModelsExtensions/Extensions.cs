using DBManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DBManager.Models
{
    public partial class UnitsOfMesure
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Id = {Id}");
            sb.AppendLine($"Description = {Description}");
            return sb.ToString();
        }
    }

    public partial class IsUsedValue
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Id = {Id}");
            sb.AppendLine($"Description = {Description}");
            return sb.ToString();
        }
    }

    public partial class Menu
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Id = {Id}");
            sb.AppendLine($"MenuEntry = {MenuEntry}");
            sb.AppendLine($"Category = {Category}");
            sb.AppendLine($"SellingPrice = {SellingPrice}");
            return sb.ToString();
        }
    }

    public partial class MenuPreparation
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"EntryId = {EntryId}");
            sb.AppendLine($"MenuProductId = {MenuProductId}");
            sb.AppendLine($"IngedientId = {IngedientId}");
            sb.AppendLine($"IngredientQuantity = {IngredientQuantity}");
            sb.AppendLine($"UnitOfMesure = {UnitOfMesure}");
            return sb.ToString();
        }
    }

    public partial class Supplier
    {
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Id = {Id}");
            sb.AppendLine($"SupplierName = {SupplierName}");
            if (PtIva != null)
                sb.AppendLine($"PtIva = {PtIva}");
            if (Telefono != null)
                sb.AppendLine($"Telefono = {Telefono}");
            if (Email != null)
                sb.AppendLine($"Email = {Email}");
            if (Note != null)
                sb.AppendLine($"Note = {Note}");
            return sb.ToString();
        }
    }

    public partial class DrinkIngredient
    {
        GestioneMagazzinoContext context = new GestioneMagazzinoContext();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Id = {Id}");
            sb.AppendLine($"DrinkName = {DrinkName}");
            sb.AppendLine($"Category = {Category}");
            string used = context.IsUsedValues.FirstOrDefault(x => x.Id == IsUsed).Description;
            sb.AppendLine($"IsUsed = {used}");
            string SUP = context.Suppliers.FirstOrDefault(x => x.Id == SupplierId).SupplierName;
            sb.AppendLine($"SupplierId = {SUP}");
            sb.AppendLine($"Cost = {Cost}");
            sb.AppendLine($"SizeLiters = {SizeLiters}");
            sb.AppendLine($"SizeUnits = {SizeUnits}");
            sb.AppendLine($"CostLiter = {CostLiter}");
            sb.AppendLine($"CostUnit = {CostUnit}");
            sb.AppendLine($"QuantityNeeded = {QuantityNeeded}");
            sb.AppendLine($"Notes = {Notes}");
            return sb.ToString();
        }
    }

    public partial class Ingredient
    {
        GestioneMagazzinoContext context = new GestioneMagazzinoContext();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Id = {Id}");
            sb.AppendLine($"Ingredient1 = {Ingredient1}");
            sb.AppendLine($"Category = {Category}");
            string used = IsUsed.HasValue ? context.IsUsedValues.FirstOrDefault(x => x.Id == IsUsed.Value).Description : "N/A";
            sb.AppendLine($"IsUsed = {used}");
            string supplier = context.Suppliers.FirstOrDefault(x => x.Id == SupplierId).SupplierName;
            sb.AppendLine($"SupplierId = {supplier}");
            sb.AppendLine($"SizeKg = {SizeKg}");
            sb.AppendLine($"SizeUnits = {SizeUnits}");
            sb.AppendLine($"Cost = {Cost}");
            sb.AppendLine($"CostKg = {CostKg}");
            sb.AppendLine($"CostUnit = {CostUnit}");
            sb.AppendLine($"QuantityNeeded = {QuantityNeeded}");
            sb.AppendLine($"Notes = {Notes}");
            return sb.ToString();
        }
    }

    public partial class UseMaterial
    {
        GestioneMagazzinoContext context = new GestioneMagazzinoContext();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Id = {Id}");
            sb.AppendLine($"MaterialName = {MaterialName}");
            sb.AppendLine($"Category = {Category}");
            string used = context.IsUsedValues.FirstOrDefault(x => x.Id == IsUsed).Description;
            sb.AppendLine($"IsUsed = {used}");
            string supplier = context.Suppliers.FirstOrDefault(x => x.Id == SupplierId).SupplierName;
            sb.AppendLine($"SupplierId = {supplier}");
            sb.AppendLine($"SizeUnits = {SizeUnits}");
            sb.AppendLine($"Cost = {Cost}");
            sb.AppendLine($"CostUnit = {CostUnit}");
            sb.AppendLine($"QuantityNeeded = {QuantityNeeded}");
            sb.AppendLine($"Notes = {Notes}");
            return sb.ToString();
        }
    }



}
