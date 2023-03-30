using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    public class CategoryIngredientListsRepository : IRepository<CategoryIngredientList>
    {
        private readonly StorageManagerDBContext _dbContext;

        public CategoryIngredientListsRepository(StorageManagerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CategoryIngredientList> GetAll()
        {
            return _dbContext.CategoryIngredientLists.ToList();
        }

        public CategoryIngredientList GetById(long id)
        {
            return _dbContext.CategoryIngredientLists.Find(id);
        }

        public CategoryIngredientList GetByName(string Name)
        {
            return _dbContext.CategoryIngredientLists.FirstOrDefault(x => x.Category.Name == Name);
        }

        public long Add(CategoryIngredientList entity)
        {
            _dbContext.CategoryIngredientLists.Add(entity);
            _dbContext.SaveChanges();
            return entity.EntryId;
        }

        public string AddAll(List<CategoryIngredientList> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var entity in entities)
                    {
                        _dbContext.CategoryIngredientLists.Add(entity);
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

        public void Update(CategoryIngredientList entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(CategoryIngredientList entity)
        {
            _dbContext.CategoryIngredientLists.Remove(entity);
            _dbContext.SaveChanges();
        }

        public bool Exists(long CategoryId)
        {
            return _dbContext.CategoryIngredientLists.Any(x => x.CategoryId == CategoryId);
        }

        public List<CategoryIngredientList> GetFromCategory_Id(long CategoryId)
        {
            return _dbContext.CategoryIngredientLists.Where(X=>X.CategoryId == CategoryId).ToList();
        }

        public List<long> GetIngredientIdFromCategory_Id(long CategoryId)
        {
            List<CategoryIngredientList> CatIngredients = _dbContext.CategoryIngredientLists.Where(X => X.CategoryId == CategoryId).ToList();
            List<long> Ids = new List<long>();
            foreach (CategoryIngredientList x in CatIngredients)
            {
                Ids.Add(x.IngredientId);
            }
            return Ids;
        }

        public string InsertFromCSV(string fileUri)
        {
            return "";
        }
    }
}
