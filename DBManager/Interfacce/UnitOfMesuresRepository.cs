using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class UnitOfMesuresRepository : IRepository<UnitsOfMesure>
    {
        private readonly GestioneMagazzinoContext _dbContext;

        public UnitOfMesuresRepository(GestioneMagazzinoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UnitsOfMesure> GetAll()
        {
            return _dbContext.UnitsOfMesures.ToList();
        }

        public UnitsOfMesure GetById(int id)
        {
            return _dbContext.UnitsOfMesures.Find(id);
        }

        public void Add(UnitsOfMesure entity)
        {
            _dbContext.UnitsOfMesures.Add(entity);
            _dbContext.SaveChanges();
        }

        public string AddAll(List<UnitsOfMesure> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.UnitsOfMesures.Add(entity);
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

        public void Update(UnitsOfMesure entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(UnitsOfMesure entity)
        {
            _dbContext.UnitsOfMesures.Remove(entity);
            _dbContext.SaveChanges();
        }

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<UnitsOfMesure> Out = new List<UnitsOfMesure>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        UnitsOfMesure myObject = new UnitsOfMesure();
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
