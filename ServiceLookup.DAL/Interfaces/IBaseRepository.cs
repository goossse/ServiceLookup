﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLookup.DAL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
/*        Task<bool> Create(T entity);
        Task<T> Get(int id);
        Task<List<T>> GetAll();
        Task<bool> Delete(T entity);*/

        void Create(T entity);
        T FindById(int id);
        /*Task<T> FindByIdAsync(int id);*/
        IEnumerable<T> Get();
        void Remove(T item);
        void Update(T item);

    }
}
