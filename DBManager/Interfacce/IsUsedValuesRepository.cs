using DBManager.Models;
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
        private readonly GestioneMagazzinoContext _dbContext;

        public IsUsedValuesRepository(GestioneMagazzinoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<IsUsedValue> GetAll()
        {
            return _dbContext.IsUsedValues.ToList();
        }

        public IsUsedValue GetById(int id)
        {
            return _dbContext.IsUsedValues.Find(id);
        }

        public void Add(IsUsedValue entity)
        {
            _dbContext.IsUsedValues.Add(entity);
            _dbContext.SaveChanges();
        }

        public void AddAll(List<IsUsedValue> entities)
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
                }
                catch (Exception ex)
                {
                    dbContextTrans.Rollback();
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
                    AddAll(Out);
                    return "Succeded";
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
    }
}
