using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class MenusRepository : IRepository<Menu>
    {
        private readonly GestioneMagazzinoContext _dbContext;

        public MenusRepository(GestioneMagazzinoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Menu> GetAll()
        {
            return _dbContext.Menus.ToList();
        }

        public Menu GetById(int id)
        {
            return _dbContext.Menus.Find(id);
        }

        public void Add(Menu entity)
        {
            _dbContext.Menus.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Menu entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(Menu entity)
        {
            _dbContext.Menus.Remove(entity);
            _dbContext.SaveChanges();
        }

    }
}
