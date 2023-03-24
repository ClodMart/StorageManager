using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class IngredientsRepository : IRepository<Ingredient>
    {
        private readonly StorageManagerDBContext _dbContext;

        public IngredientsRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _dbContext.Ingredients.ToList();
        }

        public Ingredient GetById(long id)
        {
            return _dbContext.Ingredients.Find(id);
        }

        public Ingredient GetByName(string Name)
        {
            return _dbContext.Ingredients.FirstOrDefault(x=>x.Name == Name);
        }

        public long Add(Ingredient entity)
        {
            _dbContext.Ingredients.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public string AddAll(List<Ingredient> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.Ingredients.Add(entity);
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

        public void Update(Ingredient entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Ingredient entity)
        {
            _dbContext.Ingredients.Remove(entity);
            _dbContext.SaveChanges();
        }

        public bool Exists(string name)
        {
            return _dbContext.Ingredients.Any(x => x.Name == name);
        }

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<Ingredient> Out = new List<Ingredient>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        Ingredient myObject = new Ingredient();

                        myObject.Name = values[0];
                        myObject.Category = values[1];
                        long Used = _dbContext.IsUsedValues.FirstOrDefault(x => x.Description == values[2].ToUpper()).Id;
                        myObject.IsUsedValue = Used;
                        myObject.QuantityNeeded = int.Parse(values[3]);
                        myObject.ActualQuantity = int.Parse(values[4]);
                        myObject.Notes = values[5];

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
