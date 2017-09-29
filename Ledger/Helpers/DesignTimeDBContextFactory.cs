using Ledger.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ledger.Helpers
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LedgerDBContext>
    {
        public LedgerDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LedgerDBContext>();

            var connectionString = "Server=LAPTOP-KUC56DD5\\SQLEXPRESS; Database=Ledger;Trusted_Connection=True;MultipleActiveResultSets=true";

            builder.UseSqlServer<LedgerDBContext>(connectionString, options => options.MigrationsAssembly("Ledger.Data"));

            return new LedgerDBContext(builder.Options);
        }
    }
}
