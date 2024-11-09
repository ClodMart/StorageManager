using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class IngredientsFormatsRepository : IRepository<IngredientsFormat>
    {
        private readonly StorageManagerDBContext _dbContext;

        public IngredientsFormatsRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<IngredientsFormat> GetAll()
        {
            return _dbContext.IngredientsFormats.ToList();
        }

        public IngredientsFormat GetById(long id)
        {
            return _dbContext.IngredientsFormats.Find(id);
        }

        public List<IngredientsFormat> GetFormatsFromIngredientId(long id)
        {
            List<IngredientsFormat> Out = _dbContext.IngredientsFormats.Include(x => x.Ingredient).Where(x=>x.IngredientId==id).ToList();
            return Out;
        }

        public List<IngredientsFormat> GetDefaultFormatFromCategoryIngredientList(List<CategoryIngredientList> ids)
        {
            List<IngredientsFormat> OUT = new List<IngredientsFormat>();
            foreach (long id in ids.Select(x => x.IngredientId))
            {
                OUT.Add(_dbContext.IngredientsFormats.FirstOrDefault(x=>x.IngredientId.Equals(id) && x.IsDefault));
            }
            return OUT;
        }

        public IngredientsFormat GetDefaultFormatFromCategoryIngredient(CategoryIngredientList id)
        {
            return _dbContext.IngredientsFormats.FirstOrDefault(x => x.IngredientId.Equals(id.IngredientId) && x.IsDefault);
        }

        public long Add(IngredientsFormat entity)
        {
            _dbContext.IngredientsFormats.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public string AddAll(List<IngredientsFormat> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.IngredientsFormats.Add(entity);
                    }
                    _dbContext.SaveChanges();
                    dbContextTrans.Commit();
                    return "Succesful";
                }
                catch (Exception e)
                {
                    dbContextTrans.Rollback();
                    string inner = e.InnerException.Message;
                    if (inner != null)
                    {
                        return e.Message + "\nInner Exeption: " + inner;
                    }
                    else
                    {
                        return e.Message;
                    }
                }
            }
        }

        public void Update(IngredientsFormat entity)
        {
            IngredientsFormat OldRecord = _dbContext.IngredientsFormats.FirstOrDefault(x => x.Id == entity.Id);
            if (OldRecord != null)
            {
                if (entity.Cost != OldRecord.Cost)
                {
                    entity.PastCost3 = OldRecord.PastCost2;
                    entity.PastCost2 = OldRecord.PastCost1;
                    entity.PastCost1 = OldRecord.Cost;
                    entity.LastPriceChange= DateOnly.FromDateTime(DateTime.Now);
                }
            }
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(IngredientsFormat entity)
        {
            _dbContext.IngredientsFormats.Remove(entity);
            _dbContext.SaveChanges();
        }

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<IngredientsFormat> Out = new List<IngredientsFormat>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        IngredientsFormat myObject = new IngredientsFormat();

                        long IngredId = _dbContext.Ingredients.FirstOrDefault(x=>x.Name == values[0]).Id;
                        myObject.IngredientId = IngredId;
                        long SupId = _dbContext.Suppliers.FirstOrDefault(x => x.SupplierName == values[1]).Id;
                        myObject.SupplierId = SupId;
                        string Price = values[2].Replace("€ ", "");
                        myObject.Cost = decimal.Parse(Price);
                        myObject.PastCost1 = decimal.Parse(Price);
                        if (values[3] != "")
                        {
                            myObject.SizeKg = decimal.Parse(values[3]);
                        }
                        if (values[4] != "")
                        {
                            myObject.SizeUnit = int.Parse(values[4]);
                        }

                        Out.Add(myObject);
                    }
                    string result = AddAll(Out);
                    return result;
                }
                catch (DbUpdateException e)
                {
                    StringBuilder sb = new StringBuilder();
                    string inner = e.InnerException.Message;
                    if (inner != null)
                    {
                        return e.Message + "\nInner Exeption: " + inner;
                    }
                    else
                    {
                        return e.Message;
                    }
                }
            }
        }
    }
}
