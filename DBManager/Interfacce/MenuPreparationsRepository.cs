using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class MenuPreparationsRepository : IRepository<MenuPreparation>
    {
        private readonly GestioneMagazzinoContext _dbContext;

        public MenuPreparationsRepository(GestioneMagazzinoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MenuPreparation> GetAll()
        {
            return _dbContext.MenuPreparations.ToList();
        }

        public MenuPreparation GetById(int id)
        {
            return _dbContext.MenuPreparations.Find(id);
        }

        public void Add(MenuPreparation entity)
        {
            _dbContext.MenuPreparations.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(MenuPreparation entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(MenuPreparation entity)
        {
            _dbContext.MenuPreparations.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
