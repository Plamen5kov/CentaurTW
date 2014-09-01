using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentaurFactory.Model
{
    public class Dish
    {
        public Dish()
        {
            this.Ingredients = new HashSet<Ingredient>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Decimal Price { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
