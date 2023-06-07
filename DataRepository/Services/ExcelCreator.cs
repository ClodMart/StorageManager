using DBManager.Interfacce;
using DBManager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Net.NetworkInformation;

namespace DataRepository.Services
{
    public class ExcelCreator
    {
        private static readonly StorageManagerDBContext context = DBService.Instance.DbContext;
        private static readonly IngredientsFormatsRepository FormatRepository = new IngredientsFormatsRepository(context);
        private static readonly SuppliersRepository SuppliersRepository = new SuppliersRepository(context);
        private static readonly IngredientsRepository Ingredients = new IngredientsRepository(context);


        public static DataSet CreateExcel(List<CategoryIngredientList> order)
        {
            //Create the data set and table
            DataSet ds = new DataSet("New_DataSet");            
            ds.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

            List<IngredientsFormat> ingredientsFormats = new List<IngredientsFormat>();
            foreach (CategoryIngredientList x in order)
            {
                ingredientsFormats.Add(FormatRepository.GetById(x.SelectedFormatId ?? 0));
            }
            List<Supplier> suppliers = new List<Supplier>();
            foreach (IngredientsFormat x in ingredientsFormats)
            {
                Supplier sup = SuppliersRepository.GetById(x.SupplierId);
                if (!suppliers.Contains(sup))
                {
                    suppliers.Add(sup);
                }                
            }
            //order.Select(x => x.SelectedFormat.Supplier).Distinct().ToList();
            foreach (Supplier supplier in suppliers)
            {
                int i = 0;
                DataTable dt = new DataTable(supplier.SupplierName);
                dt.Locale = System.Threading.Thread.CurrentThread.CurrentCulture;

                DataColumn Name = new DataColumn();
                Name.DataType = typeof(string);
                Name.ColumnName = "Prodotto";
                dt.Columns.Add(Name);

                DataColumn Category = new DataColumn();
                Category.DataType = typeof(string);
                Category.ColumnName = "Categoria";
                dt.Columns.Add(Category);

                DataColumn Quantity = new DataColumn();
                Quantity.DataType = typeof(string);
                Quantity.ColumnName = "Quantità";
                dt.Columns.Add(Quantity);

                List<CategoryIngredientList> FromSupplier = order.Where(x=>x.SelectedFormat.Supplier== supplier).ToList();
                foreach(CategoryIngredientList x in FromSupplier)
                {
                    Ingredient ing = Ingredients.GetById(x.IngredientId);
                    DataRow row = dt.NewRow();
                    row["Prodotto"] = ing.Name;
                    row["Categoria"] = ing.Category;
                    row["Quantità"] = x.Quantity;
                    dt.Rows.Add(row);
                    i++;
                }
                //while(i < 100)
                //{
                //    DataRow row = dt.NewRow();
                //    row["Prodotto"] = "";
                //    row["Categoria"] = "";
                //    row["Quantità"] = "";
                //    dt.Rows.Add(row);
                //    i++;
                //}
                
                ds.Tables.Add(dt);
                //dt.Rows.Clear();
            }

            return ds;
        }
    }
}
