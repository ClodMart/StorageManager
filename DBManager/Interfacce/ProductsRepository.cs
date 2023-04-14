using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class ProductsRepository : IRepository<Product>
    {
        private readonly StorageManagerDBContext _dbContext;

        public ProductsRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetById(long id)
        {
            return _dbContext.Products.Find(id);
        }

        public Product GetByProductName(string ProductName)
        {
            return _dbContext.Products.FirstOrDefault(x => x.ProductName == ProductName);
        }

        public long Add(Product entity)
        {
            _dbContext.Products.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public string AddAll(List<Product> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.Products.Add(entity);
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

        public void Update(Product entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Product entity)
        {
            _dbContext.Products.Remove(entity);
            _dbContext.SaveChanges();
        }

        public bool Exists(string ProductName)
        {
            return _dbContext.Products.Any(x => x.ProductName == ProductName);
        }

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<Product> Out = new List<Product>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        Product myObject = new Product();

                        myObject.ProductName = values[0];
                        myObject.ProductPrice = double.Parse(values[1]);
                        //long Used = _dbContext.IsUsedValues.FirstOrDefault(x => x.Description == values[2].ToUpper()).Id;
                        //myObject.IsUsedValue = Used;
                        myObject.ProductPrice = int.Parse(values[2]);
                        myObject.ProductCost = int.Parse(values[3]);
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

        public List<Product> GetAllById(List<long> Ids)
        {
            List<Product> OUT = new List<Product>();
            foreach (long x in Ids)
            {
                OUT.Add(_dbContext.Products.FirstOrDefault(y => y.Id == x));
            }
            return OUT;
        }
    }
}
