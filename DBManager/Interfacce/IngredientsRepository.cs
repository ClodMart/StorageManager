using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class IngredientsRepository : IRepository<Ingredient>
    {
        private readonly GestioneMagazzinoContext _dbContext;

        public IngredientsRepository(GestioneMagazzinoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _dbContext.Ingredients.ToList();
        }

        public Ingredient GetById(int id)
        {
            return _dbContext.Ingredients.Find(id);
        }

        public void Add(Ingredient entity)
        {
            _dbContext.Ingredients.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Ingredient entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Ingredient entity)
        {
            _dbContext.Ingredients.Remove(entity);
            _dbContext.SaveChanges();
        }

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        Ingredient myObject = new Ingredient();

                        myObject.Ingredient1 = values[0];
                        myObject.Category = values[1];
                        myObject.IsUsed = int.Parse(values[2]);
                        int SupID = _dbContext.Suppliers.FirstOrDefault(x => x.SupplierName == values[3]).Id;
                        myObject.SupplierId = SupID;
                        string Price = values[4].Replace("€ ", "");
                        myObject.Cost = decimal.Parse(Price);
                        if (values[5] != "")
                        {
                            myObject.SizeKg = decimal.Parse(values[5]);
                        }
                        if (values[6] != "")
                        {
                            myObject.SizeUnits = int.Parse(values[6]);
                        }
                        myObject.QuantityNeeded = int.Parse(values[7]);
                        myObject.Notes = values[8];

                        this.Add(myObject);
                    }
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
