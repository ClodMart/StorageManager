using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    internal class SuppliersRepository : IRepository<Supplier>
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
    }
}
