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
    }
}
 
