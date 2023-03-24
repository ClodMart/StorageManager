using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class UnitOfMesuresRepository : IRepository<UnitsOfMeasure>
    {
        private readonly StorageManagerDBContext _dbContext;

        public UnitOfMesuresRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UnitsOfMeasure> GetAll()
        {
            return _dbContext.UnitsOfMeasures.ToList();
        }

        public UnitsOfMeasure GetById(long id)
        {
            return _dbContext.UnitsOfMeasures.Find(id);
        }

        public long Add(UnitsOfMeasure entity)
        {
            _dbContext.UnitsOfMeasures.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public string AddAll(List<UnitsOfMeasure> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.UnitsOfMeasures.Add(entity);
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

        public void Update(UnitsOfMeasure entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(UnitsOfMeasure entity)
        {
            _dbContext.UnitsOfMeasures.Remove(entity);
            _dbContext.SaveChanges();
        }

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<UnitsOfMeasure> Out = new List<UnitsOfMeasure>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        UnitsOfMeasure myObject = new UnitsOfMeasure();
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
    }
}
