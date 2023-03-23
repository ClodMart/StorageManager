using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    //public class MenusRepository : IRepository<Menu>
    //{
    //    private readonly GestioneMagazzinoContext _dbContext;

    //    public MenusRepository(GestioneMagazzinoContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }

    //    public IEnumerable<Menu> GetAll()
    //    {
    //        return _dbContext.Menus.ToList();
    //    }

    //    public Menu GetById(int id)
    //    {
    //        return _dbContext.Menus.Find(id);
    //    }

    //    public void Add(Menu entity)
    //    {
    //        _dbContext.Menus.Add(entity);
    //        _dbContext.SaveChanges();
    //    }

    //    public string AddAll(List<Menu> entities)
    //    {
    //        using (var dbContextTrans = _dbContext.Database.BeginTransaction())
    //        {
    //            try
    //            {
    //                foreach (var entity in entities)
    //                {
    //                    _dbContext.Menus.Add(entity);
    //                }
    //                _dbContext.SaveChanges();
    //                dbContextTrans.Commit();
    //                return "Succeded";
    //            }
    //            catch (Exception e)
    //            {
    //                dbContextTrans.Rollback();
    //                string inner = e.InnerException.Message;
    //                if (inner != null)
    //                {
    //                    return e.Message + "\nInner Exeption: " + inner;
    //                }
    //                else
    //                {
    //                    return e.Message;
    //                }
    //            }
    //        }
    //    }

    //    public void Update(Menu entity)
    //    {
    //        _dbContext.Entry(entity).State = EntityState.Modified;
    //        _dbContext.SaveChanges();
    //    }

    //    public void Delete(Menu entity)
    //    {
    //        _dbContext.Menus.Remove(entity);
    //        _dbContext.SaveChanges();
    //    }

    //    public string InsertFromCSV(string fileUri)
    //    {
    //        using (var reader = new StreamReader(fileUri))
    //        {
    //            try
    //            {
    //                List<Menu> Out = new List<Menu>();
    //                reader.ReadLine();
    //                while (!reader.EndOfStream)
    //                {
    //                    var line = reader.ReadLine();
    //                    var values = line.Split(';');
    //                    Menu myObject = new Menu();

    //                    myObject.MenuEntry = values[0];
    //                    myObject.Category = values[1];
    //                    myObject.SellingPrice = decimal.Parse(values[2]);

    //                    Out.Add(myObject);
    //                }
    //                string result = AddAll(Out);
    //                return result;
    //            }
    //            catch (DbUpdateException e)
    //            {
    //                StringBuilder sb = new StringBuilder();
    //                string inner = e.InnerException.Message;
    //                if (inner != null)
    //                {
    //                    return e.Message + "\nInner Exeption: " + inner;
    //                }
    //                else
    //                {
    //                    return e.Message;
    //                }
    //            }
    //        }
    //    }

    //}
}
