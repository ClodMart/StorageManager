using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class UseMaterialsRepository : IRepository<UseMaterial>
    {
        private readonly GestioneMagazzinoContext _dbContext;

        public UseMaterialsRepository(GestioneMagazzinoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UseMaterial> GetAll()
        {
            return _dbContext.UseMaterials.ToList();
        }

        public UseMaterial GetById(int id)
        {
            return _dbContext.UseMaterials.Find(id);
        }

        public void Add(UseMaterial entity)
        {
            _dbContext.UseMaterials.Add(entity);
            _dbContext.SaveChanges();
        }

        public void AddAll(List<UseMaterial> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.UseMaterials.Add(entity);
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

        public void Update(UseMaterial entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(UseMaterial entity)
        {
            _dbContext.UseMaterials.Remove(entity);
            _dbContext.SaveChanges();
        }

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<UseMaterial> Out = new List<UseMaterial>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        UseMaterial myObject = new UseMaterial();

                        myObject.MaterialName = values[0];
                        myObject.Category = values[1];
                        int Used = _dbContext.IsUsedValues.FirstOrDefault(x => x.Description == values[2]).Id;
                        myObject.IsUsed = Used;
                        int SupID = _dbContext.Suppliers.FirstOrDefault(x => x.SupplierName == values[3]).Id;
                        myObject.SupplierId = SupID;
                        string Price = values[4].Replace("€ ", "");
                        myObject.Cost = decimal.Parse(Price);
                        if (values[5] != "")
                        {
                            myObject.SizeUnits = decimal.Parse(values[5]);
                        }
                        myObject.QuantityNeeded = int.Parse(values[6]);
                        myObject.Notes = values[7];

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
 
