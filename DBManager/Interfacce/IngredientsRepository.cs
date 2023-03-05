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
    }
}
