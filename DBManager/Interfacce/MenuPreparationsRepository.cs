﻿using DBManager.Models;
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

        public string InsertFromCSV(string fileUri)
        {
            using (var reader = new StreamReader(fileUri))
            {
                try
                {
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');
                        MenuPreparation myObject = new MenuPreparation();

                        myObject.MenuProductId = int.Parse(values[0]);
                        myObject.IngedientId = int.Parse(values[1]);
                        myObject.IngredientQuantity = decimal.Parse(values[2]);
                        myObject.UnitOfMesure= int.Parse(values[3]);

                        this.Add(myObject);
                    }
                    return "Succeded";
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
