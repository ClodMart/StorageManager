﻿using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class IsUsedValuesRepository : IRepository<IsUsedValue>
    {
        private readonly StorageManagerDBContext _dbContext;

        public IsUsedValuesRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<IsUsedValue> GetAll()
        {
            return _dbContext.IsUsedValues.ToList();
        }

        public IsUsedValue GetById(long id)
        {
            return _dbContext.IsUsedValues.Find(id);
        }

        public long Add(IsUsedValue entity)
        {
            _dbContext.IsUsedValues.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public string AddAll(List<IsUsedValue> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.IsUsedValues.Add(entity);
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

        public void Update(IsUsedValue entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(IsUsedValue entity)
        {
            _dbContext.IsUsedValues.Remove(entity);
            _dbContext.SaveChanges();
        }

        public string InsertFromCSV(string fileUri)
        {
            
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<IsUsedValue> Out = new List<IsUsedValue>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        IsUsedValue myObject = new IsUsedValue();
                        myObject.Description = values[0];
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

        public List<long> GetUsedId()
        {
            return _dbContext.IsUsedValues.Where(x=>x.CorrespondsToUsed).Select(x=>x.Id).ToList();
        }
    }
}
