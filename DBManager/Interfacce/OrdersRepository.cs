using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class OrdersRepository : IRepository<Order>
    {
        private readonly StorageManagerDBContext _dbContext;

        public OrdersRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Order> GetAll()
        {
            return _dbContext.Orders.ToList();
        }

        public Order GetById(long id)
        {
            return _dbContext.Orders.Find(id);
        }

        public long Add(Order entity)
        {
            _dbContext.Orders.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public string AddAll(List<Order> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.Orders.Add(entity);
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

        public void Update(Order entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Order entity)
        {
            _dbContext.Orders.Remove(entity);
            _dbContext.SaveChanges();
        }

        public string InsertFromCSV(string fileUri)
        {
            return "";
            //using (var reader = new StreamReader(fileUri))
            //{
            //    try
            //    {
            //        List<Order> Out = new List<Order>();
            //        reader.ReadLine();
            //        while (!reader.EndOfStream)
            //        {
            //            var line = reader.ReadLine();
            //            var values = line.Split(';');
            //            Order myObject = new Order();
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
    }
}
