﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using North.DAL;

namespace North.BLL.Repository
{
    public class RepositoryBase<T, TId> : IDisposable where T : class
    {
        protected static MyContext db;

        public RepositoryBase()
        {
            db = db ?? new MyContext();
        }

        public virtual List<T> GetAll()
        {
            try
            {
                return db.Set<T>().ToList();
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await db.Set<T>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public virtual T GetById(TId id)
        {
            try
            {
                return db.Set<T>().Find(id);
            }
            catch
            {
                throw;
            }
        }

        public virtual int Insert(T entity)
        {
            try
            {
                db.Set<T>().Add(entity);
                return db.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public virtual int Delete(T entity)
        {
            try
            {
                db.Set<T>().Remove(entity);
                return db.SaveChanges();
            }
            catch
            { 
                throw;
            }
        }

        public virtual int Update()
        {
            return db.SaveChanges();
        }

        public virtual async Task<int> UpdateAsync()
        {
            return await db.SaveChangesAsync();
        }

        public virtual IQueryable<T> Queryable()
        {
            try
            {
                return db.Set<T>().AsQueryable();
            }
            catch
            {
                throw;
            }
        }

        public virtual List<T> GetAll(Func<T,bool> predicate)
        {
            try
            {
                return db.Set<T>().Where(predicate).ToList();
            }
            catch
            { 
                throw;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            db = new MyContext();
        }
    }
}