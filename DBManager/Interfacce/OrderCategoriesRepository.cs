using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class OrderCategoriesRepository : IRepository<OrderCategory>
    {
        private readonly StorageManagerDBContext _dbContext;

        public OrderCategoriesRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrderCategory> GetAll()
        {
            return _dbContext.OrderCategories.ToList();
        }

        public OrderCategory GetById(long id)
        {
            return _dbContext.OrderCategories.Find(id);
        }

        public long Add(OrderCategory entity)
        {
            _dbContext.OrderCategories.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public string AddAll(List<OrderCategory> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.OrderCategories.Add(entity);
                    }
                    _dbContext.SaveChanges();
                    dbContextTrans.Commit();
                    return "Succeded";
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

        public void Update(OrderCategory entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(OrderCategory entity)
        {
            _dbContext.OrderCategories.Remove(entity);
            _dbContext.SaveChanges();
        }

        public long GetNextId()
        {
            long OUT = 0;
            if (_dbContext.OrderCategories.Count() > 0)
            {
                OUT = _dbContext.OrderCategories.Max(x => x.Id);
            }            
            return OUT+1;
        }

        public string InsertFromCSV(string fileUri)
        {
            return "";
            //using (var reader = new StreamReader(fileUri))
            //{
            //    try
            //    {
            //        List<OrderCategory> Out = new List<OrderCategory>();
            //        reader.ReadLine();
            //        while (!reader.EndOfStream)
            //        {
            //            var line = reader.ReadLine();
            //            var values = line.Split(';');
            //            OrderCategory myObject = new OrderCategory();
            //            myObject.Description = values[0];
            //            Out.Add(myObject);
            //        }
            //        string result = AddAll(Out);
            //        return result;
            //    }
            //    catch (DbUpdateException e)
            //    {
            //        string inner = e.InnerException.Message;
            //        if (inner != null)
            //        {
            //            return e.Message + "\nInner Exeption: " + inner;
            //        }
            //        else
            //        {
            //            return e.Message;
            //        }
            //    }
            //}
        }

        public bool DeleteCategoryCascade(OrderCategory entity)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    List<CategoryIngredientList> ingredients = _dbContext.CategoryIngredientLists.Where(x => x.CategoryId == entity.Id).ToList();
                    foreach(CategoryIngredientList x in ingredients)
                    {
                        _dbContext.CategoryIngredientLists.Remove(x);
                    }
                    _dbContext.OrderCategories.Remove(entity);
                    _dbContext.SaveChanges();
                    dbContextTrans.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    dbContextTrans.Rollback();
                    string inner = e.InnerException.Message;
                    if (inner != null)
                    {
                        string err = e.Message + "\nInner Exeption: " + inner;
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
