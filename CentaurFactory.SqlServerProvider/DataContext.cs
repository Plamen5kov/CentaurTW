namespace CentaurFactory.SqlServerProvider
{
    using System.Data.Entity;

    using CentaurFactory.Model;
    using CentaurFactory.SqlServerProvider.Mapping;

    internal class DataContext : DbContext
    {
        public DataContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ProductTypeMap());
            modelBuilder.Configurations.Add(new UnitTypeMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
