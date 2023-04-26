using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBManager.Interfacce
{
    public class ProductCompositionsRepository : IRepository<ProductComposition>
    {
        private readonly StorageManagerDBContext _dbContext;

        public ProductCompositionsRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductComposition> GetAll()
        {
            return _dbContext.ProductCompositions.ToList();
        }

        public ProductComposition GetById(long id)
        {
            return _dbContext.ProductCompositions.Find(id);
        }

        //public ProductComposition GetByProductId (long ProdID)
        //{
        //    return _dbContext.ProductCompositions.FirstOrDefault(x => x.ProductId == ProdID);
        //}

        public List<ProductComposition> GetFormatsFromProductId(long id)
        {
            List<ProductComposition> Out = _dbContext.ProductCompositions.Where(x => x.ProductId == id).ToList();
            return Out;
        }

        public long Add(ProductComposition entity)
        {
            entity.Cost= (double)_dbContext.IngredientsFormats.FirstOrDefault(x => x.IngredientId == entity.IngredientId && x.IsDefault).CostKg * entity.Quantity;
            _dbContext.ProductCompositions.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public string AddAll(List<ProductComposition> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        if(_dbContext.ProductCompositions.Any(x=>x.Id== entity.Id))
                        {
                            _dbContext.ProductCompositions.Update(entity);
                        }
                        else
                        {
                            _dbContext.ProductCompositions.Add(entity);
                        }                        
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

        public void Update(ProductComposition entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(ProductComposition entity)
        {
            _dbContext.ProductCompositions.Remove(entity);
            _dbContext.SaveChanges();
        }

        public bool Exists(long ID)
        {
            return _dbContext.ProductCompositions.Any(x => x.Id == ID);
        }

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<ProductComposition> Out = new List<ProductComposition>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        ProductComposition myObject = new ProductComposition();

                        myObject.ProductId = _dbContext.Products.FirstOrDefault(x => x.ProductName == values[0].ToUpper()).Id;
                        myObject.IngredientId = _dbContext.Ingredients.FirstOrDefault(x => x.Name == values[0].ToUpper()).Id;
                        //long Used = _dbContext.IsUsedValues.FirstOrDefault(x => x.Description == values[2].ToUpper()).Id;
                        //myObject.IsUsedValue = Used;
                        myObject.Quantity = int.Parse(values[2]);
                        //myObject.Cost = int.Parse(values[3]);
                        //myObject.Notes = values[5];

                        Out.Add(myObject);
                    }
                    string result = AddAll(Out);
                    return result;
                }
                catch (DbUpdateException e)
                {
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

        public List<ProductComposition> GetAllById(List<long> Ids)
        {
            List<ProductComposition> OUT = new List<ProductComposition>();
            foreach (long x in Ids)
            {
                OUT.Add(_dbContext.ProductCompositions.FirstOrDefault(y => y.Id == x));
            }
            return OUT;
        }

        public List<ProductComposition> GetAllByProductId(long Id)
        {
            List<ProductComposition> OUT = _dbContext.ProductCompositions.Where(y => y.ProductId == Id).ToList();
            return OUT;
        }
    }
}
