namespace CentaurFactory.MySqlProvider
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MySqlRepository
    {
        private DataContext context;

        public MySqlRepository(string connectionString)
        {
            context = new DataContext();
        }
    }
}
