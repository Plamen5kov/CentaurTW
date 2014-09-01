using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentaurFactory.MongoDbProvider
{
    internal class MongoIngredient
    {
        public MongoDish Dish { get; set; }

        public MongoProduct Product { get; set; }

        public double Price { get; set; }

        public MongoIngredient(MongoDish dish, MongoProduct product, double price)
        {
            this.Dish = dish;
            this.Product = product;
            this.Price = price;
        }
    }
}
