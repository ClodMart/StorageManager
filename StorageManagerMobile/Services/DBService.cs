using DBManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.Services
{
    public class DBService
    {
        private static DBService instance;
        private static readonly object padlock = new object();
        private readonly GestioneMagazzinoContext dbContext;

        private DBService()
        {
            dbContext = new GestioneMagazzinoContext();
        }

        public static DBService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DBService();
                    }
                    return instance;
                }
            }
        }

        public GestioneMagazzinoContext DbContext
        {
            get
            {
                return dbContext;
            }
        }
    }
}
