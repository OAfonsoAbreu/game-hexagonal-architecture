using Domain.Adapters;
using Domain.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.DataBase.LiteDB.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        protected string _database = "filename=Database.db;Connection=shared";

        protected string _collection;
        public Task<bool> Delete(int id)
        {
            var task = new Task<bool>(() =>
            {
                try
                {
                    using (var db = new LiteDatabase(_database))
                    {
                        var collection = db.GetCollection<T>(_collection);
                        collection.Delete(id);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
                
            });

            task.Start();

            return task;
        }

        public Task<T> Get(int id)
        {
            var task = new Task<T>(() =>
            {
                using (var db = new LiteDatabase(_database))
                {
                    var collection = db.GetCollection<T>(_collection);
                    return collection.FindById(id);
                }
            });

            task.Start();

            return task;
        }

        public abstract Task<T> Get(string key);

        public Task<IEnumerable<T>> GetAll()
        {
            var task = new Task<IEnumerable<T>>(() =>
            {
                using (var db = new LiteDatabase(_database))
                {
                    var collection = db.GetCollection<T>(_collection);
                    return collection.FindAll();
                }
            });

            task.Start();

            return task;
        }

        public Task<bool> Insert(T item)
        {
            var task = new Task<bool>(() =>
            {
                try
                {
                    using (var db = new LiteDatabase(_database))
                    {
                        var collection = db.GetCollection<T>(_collection);
                        collection.Upsert(item);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
                
            });

            task.Start();

            return task;
        }

        public Task<bool> Update(T item)
        {
            var task = new Task<bool>(() =>
            {
                try
                {
                    using (var db = new LiteDatabase(_database))
                    {
                        var collection = db.GetCollection<T>(_collection);
                        collection.Upsert(item);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
                
            });

            task.Start();

            return task;
        }
    }
}
