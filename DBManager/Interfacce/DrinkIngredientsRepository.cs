using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
