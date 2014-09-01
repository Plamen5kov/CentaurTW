using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentaurFactory.MongoDbProvider
{
    internal class MongoDish
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public Decimal Price { get; set; }

        public MongoDish(string name, decimal price)
        {
            this.Id = ObjectId.GenerateNewId();
            this.Name = name;
            this.Price = price;
        }
    }
}
