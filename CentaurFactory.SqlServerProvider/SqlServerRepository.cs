using CentaurFactory.Model;
using CentaurFactory.SqlServerProvider.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentaurFactory.SqlServerProvider
{
    public class SqlServerRepository
    {
        private DataContext context;

        public SqlServerRepository(string connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Config>());

            context = new DataContext(connectionString);
        }

        public void AddProducts(IEnumerable<Product> products)
        {
            foreach (Product product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
        }
    }
}
