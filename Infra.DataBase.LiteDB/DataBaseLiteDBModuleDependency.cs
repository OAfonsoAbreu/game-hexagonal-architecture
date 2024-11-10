using Domain.Adapters;
using Domain.Entities;
using Infra.DataBase.LiteDB.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.DataBase.LiteDB
{
    public static class DataBaseLiteDBModuleDependency
    {
        public static void AddDataBaseModule(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IRepository<Game>, GameRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

        }
    }
}
