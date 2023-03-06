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

        public string AddAll(List<MenuPreparation> entities)
        {
            using (var dbContextTrans = _dbContext.Database.BeginTransaction())
            {
                try
                {                
                    foreach (var entity in entities)
                    {
                        _dbContext.MenuPreparations.Add(entity);
                    }
                    _dbContext.SaveChanges();
                    dbContextTrans.Commit();
                    return "Succeded";
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

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    List<MenuPreparation> Out = new List<MenuPreparation>();
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        MenuPreparation myObject = new MenuPreparation();

                        myObject.MenuProductId = int.Parse(values[0]);
                        myObject.IngedientId = int.Parse(values[1]);
                        myObject.IngredientQuantity = decimal.Parse(values[2]);
                        int Ut = _dbContext.UnitsOfMesures.FirstOrDefault(x => x.Description == values[3]).Id;
                        myObject.UnitOfMesure= Ut;

                        Out.Add(myObject);
                    }
                    string result = AddAll(Out);
                    return result;
                }
                catch (DbUpdateException e)
                {
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
