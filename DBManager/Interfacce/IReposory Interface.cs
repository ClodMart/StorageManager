using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Interfacce
{
    internal interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
