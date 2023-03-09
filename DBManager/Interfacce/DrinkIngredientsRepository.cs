using DBManager.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class DrinkIngredientsRepository : IRepository<DrinkIngredient>
    {
        private readonly GestioneMagazzinoContext _dbContext;

        public DrinkIngredientsRepository(GestioneMagazzinoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DrinkIngredient> GetAll()
        {
            return _dbContext.DrinkIngredients.ToList();
        }

        public DrinkIngredient GetById(int id)
        {
            return _dbContext.DrinkIngredients.Find(id);
        }

        public void Add(DrinkIngredient entity)
        {
            _dbContext.DrinkIngredients.Add(entity);
            _dbContext.SaveChanges();
        }

        public string AddAll(List<DrinkIngredient> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.DrinkIngredients.Add(entity);
                    }
                    _dbContext.SaveChanges();
                    dbContextTrans.Commit();
                    return "Succeded";
                }
                catch (DbUpdateException e)
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

        public void Update(DrinkIngredient entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(DrinkIngredient entity)
        {
            _dbContext.DrinkIngredients.Remove(entity);
            _dbContext.SaveChanges();
        }

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<DrinkIngredient> Out = new List<DrinkIngredient>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        DrinkIngredient myObject = new DrinkIngredient();

                        myObject.DrinkName = values[0];
                        myObject.Category = values[1];
                        int Used = _dbContext.IsUsedValues.FirstOrDefault(x => x.Description == values[2]).Id;
                        myObject.IsUsed = Used;
                        int SupID = _dbContext.Suppliers.FirstOrDefault(x => x.SupplierName == values[3]).Id;
                        myObject.SupplierId = SupID;
                        //myObject.SupplierId = int.Parse(values[3]);
                        string Price = values[4].Replace("€ ", "");
                        myObject.Cost = decimal.Parse(Price);
                        myObject.OldCost= decimal.Parse(Price);
                        if (values[5] != "")
                        {
                            myObject.SizeLiters = decimal.Parse(values[5]);
                        }
                        if (values[6] != "")
                        {
                            myObject.SizeUnits = decimal.Parse(values[6]);
                        }
                        myObject.QuantityNeeded = int.Parse(values[7]);
                        myObject.ActualQuantity = int.Parse(values[7]);
                        myObject.Notes = values[8];

                        Out.Add(myObject);
                    }
                    string result = AddAll(Out);
                    return result;
                }
                catch(DbUpdateException e)
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
