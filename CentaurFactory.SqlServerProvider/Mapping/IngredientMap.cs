using CentaurFactory.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentaurFactory.SqlServerProvider
{
    public class IngredientMap : EntityTypeConfiguration<Ingredient>
    {
        public IngredientMap()
        {
            this.ToTable("Product");

            this.HasKey(x => x.Id);

            this.Property(x => x.Id).HasColumnName("Id");
            this.Property(x => x.Quantity).HasColumnName("Quantity");
        }
    }
}
