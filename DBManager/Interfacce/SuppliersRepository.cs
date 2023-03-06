using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class SuppliersRepository : IRepository<Supplier>
    {
        private readonly GestioneMagazzinoContext _dbContext;

        public SuppliersRepository(GestioneMagazzinoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _dbContext.Suppliers.ToList();
        }

        public Supplier GetById(int id)
        {
            return _dbContext.Suppliers.Find(id);
        }

        public void Add(Supplier entity)
        {
            _dbContext.Suppliers.Add(entity);
            _dbContext.SaveChanges();
        }

        public void AddAll(List<Supplier> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.Suppliers.Add(entity);
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

        public void Update(Supplier entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Supplier entity)
        {
            _dbContext.Suppliers.Remove(entity);
            _dbContext.SaveChanges();
        }

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<Supplier> Out = new List<Supplier>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        Supplier myObject = new Supplier();

                        myObject.SupplierName = values[0];
                        myObject.PtIva = values[1];
                        myObject.Telefono = values[2];
                        myObject.Email = values[3];
                        myObject.Note = values[4];

                        Out.Add(myObject);
                    }
                    AddAll(Out);
                    return "Succeded";
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
