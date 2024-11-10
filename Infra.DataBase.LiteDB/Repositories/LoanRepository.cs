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
    public class LoanRepository : RepositoryBase<Loan>, ILoanRepository
    {
        public LoanRepository()
        {
            _collection = "loans";
        }
        public override Task<Loan> Get(string key)
        {
            var task = new Task<Loan>(() =>
            {
                using (var db = new LiteDatabase(_database))
                {
                    // Get a collection (or create, if doesn't exist)
                    var col = db.GetCollection<Loan>(_collection);

                    var results = col.Query()
                    .Where(x => x.User.Name.ToUpper().Contains(key) || x.Game.Name.ToUpper().Contains(key))
                    .FirstOrDefault();

                    return results;

                }
            });

            task.Start();

            return task;
        }

        public Task<Loan> Get(string userName, string gameName)
        {
            var task = new Task<Loan>(() =>
            {
                using (var db = new LiteDatabase(_database))
                {
                    // Get a collection (or create, if doesn't exist)
                    var col = db.GetCollection<Loan>(_collection);

                    var results = col.Query()
                    .Where(x => x.User.Name.ToUpper().Contains(userName) && x.Game.Name.ToUpper().Contains(gameName))
                    .FirstOrDefault();

                    return results;

                }
            });

            task.Start();

            return task;
        }

        public Task<bool> InsertOrUpdate(Loan loan)
        {
            var task = new Task<bool>(() =>
            {
                using (var db = new LiteDatabase(_database))
                {
                    var collection = db.GetCollection<Loan>(_collection);
                    return collection.Upsert(loan);
                }
            });

            task.Start();

            return task;
        }
    }
}
