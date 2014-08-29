namespace CentaurFactory.Data
{
    using MongoDB.Bson;

    using MongoDB.Bson.IO;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Bson.Serialization.IdGenerators;
    using MongoDB.Bson.Serialization.Options;
    using MongoDB.Bson.Serialization.Serializers;
    using MongoDB.Driver.Wrappers;

    public class Product
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public int ProductType { get; set; }
       
        public int UnitType { get; set; }
       
        public int Quantity { get; set; }

        public Product(string productName, int quantity, UnitType unitType,  ProductType productType)
        {
            this.Id = ObjectId.GenerateNewId();
            this.UnitType = unitType.Id;
            this.ProductType = productType.Id;
            this.Name = productName;
            this.Quantity = quantity;
        }

        public override string ToString()
        {
            return "Name: " + this.Name;
        }

    }
}
