﻿using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class OrdersListsRepository : IRepository<OrdersList>
    {
        private readonly StorageManagerDBContext _dbContext;

        public OrdersListsRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrdersList> GetAll()
        {
            return _dbContext.OrdersLists.ToList();
        }

        public OrdersList GetById(long id)
        {
            return _dbContext.OrdersLists.Find(id);
        }

        public long Add(OrdersList entity)
        {
            _dbContext.OrdersLists.Add(entity);
            _dbContext.SaveChanges();
            return entity.EntryId;
        }

        public string AddAll(List<OrdersList> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.OrdersLists.Add(entity);
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

        public void Update(OrdersList entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(OrdersList entity)
        {
            _dbContext.OrdersLists.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<OrdersList> GetListByCategory(long categoryId)
        {
            return _dbContext.OrdersLists.Where(x=>x.OrderId == categoryId).ToList();
        }

        public string InsertFromCSV(string fileUri)
        {
            return "";
        }
    }
}
