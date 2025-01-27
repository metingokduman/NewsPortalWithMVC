﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MHA.Data.Repository.Contract
{
    public interface IRepository<T> where T :class
    {
        //tüm datayı çekecek
        IEnumerable<T> GetAll();
        //tek bir nesne döncek
        T GetById(int id);
        //tek bir nesne fakat expressiona göre
        T Get(Expression<Func<T, bool>> expression);
        IQueryable<T> GetMany(Expression<Func<T, bool>> expression);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int id);
        int Count();
        void Save();

    }
}
