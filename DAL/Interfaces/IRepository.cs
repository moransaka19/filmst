using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    interface IRepository<T> where T : class
    {
        void Create(T item);
        void Update(T item);
        void Remove(T item);
        IEnumerable<T> GetAll();
        T Get(string key);
    }
}
