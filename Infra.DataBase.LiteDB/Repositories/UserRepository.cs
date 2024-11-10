using Domain.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.DataBase.LiteDB.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository()
        {
            _collection = "users";
        }
        public override Task<User> Get(string key)
        {
            var task = new Task<User>(() =>
            {
                using (var db = new LiteDatabase(_database))
                {
                    try
                    {
                        // Get a collection (or create, if doesn't exist)
                        var col = db.GetCollection<User>(_collection);

                        var results = col.Query()
                        .Where(x => x.Name.ToUpper().Contains(key.ToUpper()))
                        .FirstOrDefault();

                        return results;
                    }
                    catch
                    {
                        return new User();
                    }
                    

                }
            });

            task.Start();

            return task;
        }
    }
}
