using DBManager.Models;
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

        public void InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    DrinkIngredient myObject = new DrinkIngredient();

                    myObject.DrinkName = values[0];
                    myObject.Category = values[1];
                    myObject.IsUsed = int.Parse(values[2]);
                    myObject.SupplierId = int.Parse(values[3]);
                    myObject.Cost = decimal.Parse(values[4]);
                    myObject.SizeLiters = decimal.Parse(values[5]);
                    myObject.SizeUnits = decimal.Parse(values[6]);
                    //myObject.CostLiter= decimal.Parse(values[7]);
                    //myObject.CostUnit= decimal.Parse(values[8]);
                    myObject.QuantityNeeded = int.Parse(values[7]);
                    myObject.Notes = values[8];

                    this.Add(myObject);
                }

            }
        }
    }
}
