using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class UsersRepository : IRepository<User>
    {
        private readonly StorageManagerDBContext _dbContext;

        public UsersRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User GetById(long id)
        {
            return _dbContext.Users.Find(id);
        }

        public User GetByName(string Name)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Username == Name);
        }

        public long Add(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
            return entity.Id;
        }

        public string AddAll(List<User> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.Users.Add(entity);
                    }
                    _dbContext.SaveChanges();
                    dbContextTrans.Commit();
                    return "Succesful";
                }
                catch (Exception e)
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

        public void Update(User entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
            _dbContext.SaveChanges();
        }

        public string InsertFromCSV(string fileUri)
        {
            return "";
        }
    }
}
