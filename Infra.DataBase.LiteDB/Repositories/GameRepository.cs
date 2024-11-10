using Domain.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.DataBase.LiteDB.Repositories
{
    public class GameRepository : RepositoryBase<Game>
    {
        public GameRepository()
        {
            _collection = "games";
        }
        public override Task<Game> Get(string key)
        {
            var task = new Task<Game>(() =>
            {
                using (var db = new LiteDatabase(_database))
                {
                    // Get a collection (or create, if doesn't exist)
                    var col = db.GetCollection<Game>(_collection);

                    var results = col.Query()
                    .Where(x => x.Name.ToUpper().Contains(key.ToUpper()))
                    .FirstOrDefault();

                    return results;

                }
            });

            task.Start();

            return task;
        }
    }
}
