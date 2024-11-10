using Domain.Adapters;
using Domain.Entities;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.DataBase.LiteDB.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository()
        {
            using (var db = new LiteDatabase(@"Database.db"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Account>("accounts");

                if(col.Count() == 0)
                {
                    var account = new Account
                    {
                        Username = "admin",
                        Password = "admin",
                        Role = "admin"
                    };

                    col.Insert(account);

                    account = new Account
                    {
                        Username = "user",
                        Password = "user",
                        Role = "view"
                    };

                    col.Insert(account);
                }
                
            }
        }
        public async Task<Account> Get(string userName, string password)
        {
            var accountAsync = new Task<Account>(() =>
            {
                using (var db = new LiteDatabase(@"Database.db"))
                {
                    // Get a collection (or create, if doesn't exist)
                    var col = db.GetCollection<Account>("accounts");

                    var results = col.Query()
                    .Where(x => x.Username == userName && x.Password == password)
                    .FirstOrDefault();

                    return results;

                }
            });

            accountAsync.Start();

            return await accountAsync;


        }
    }
}
