namespace CentaurFactory.ConsoleClient
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.GridFS;
    using MongoDB.Driver.Linq;

    using MongoDB.Bson.IO;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Bson.Serialization.IdGenerators;
    using MongoDB.Bson.Serialization.Options;
    using MongoDB.Bson.Serialization.Serializers;
    using MongoDB.Driver.Wrappers;
    using CentaurFactory.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryPoint
    {
        public static void Main()
        {
            // To be able to run this code you need first to download mongodb server local on your computer.
            // this is the link for windows 64 
            // https://fastdl.mongodb.org/win32/mongodb-win32-x86_64-2008plus-2.6.4.zip .
            // When you download the zip you need to start up the bin/mongod.exe file(this is the server).
            // Then you can run the program successfully.
            var mongoClient = new MongoClient("mongodb://localhost/");
            var mongoServer = mongoClient.GetServer();
            var centaurRestaurantDb = mongoServer.GetDatabase("CentaurRestaurantDb");
           
            var unitTypes = new List<UnitType>()
            {
                new UnitType("Kilogramme"),
                new UnitType("Litre"),
                new UnitType("Number"),
            };

            var productTypes = new List<ProductType>()
            {
                new ProductType("Fruit"),
                new ProductType("Vegetable"),
                new ProductType("Herb"),
                new ProductType("Dairy"),
                new ProductType("Meat")
            };

            var products = new List<Product>()
            { 
               new Product("Tomatoe", 250,unitTypes[0], productTypes[1]),
                new Product("Cucumber", 120, unitTypes[0], productTypes[1]),
                new Product("Onion", 120, unitTypes[0], productTypes[1]),
               new Product("WhiteCheese", 40, unitTypes[0], productTypes[3])

            };
            SaveData<UnitType>(centaurRestaurantDb, unitTypes[0]);
            SaveData<UnitType>(centaurRestaurantDb, unitTypes[1]);
            SaveData<UnitType>(centaurRestaurantDb, unitTypes[2]);

            //These are added every time you run the program, but this is easy to be fixed.
            SaveData<ProductType>(centaurRestaurantDb, productTypes[0]);
            SaveData<ProductType>(centaurRestaurantDb, productTypes[1]);
            SaveData<ProductType>(centaurRestaurantDb, productTypes[2]);
            SaveData<ProductType>(centaurRestaurantDb, productTypes[3]);
            SaveData<ProductType>(centaurRestaurantDb, productTypes[4]);

            SaveData<Product>(centaurRestaurantDb, products[0]);
            SaveData<Product>(centaurRestaurantDb, products[1]);
            SaveData<Product>(centaurRestaurantDb, products[2]);
            SaveData<Product>(centaurRestaurantDb, products[3]);
            
            //DeleteAll<Product>(centaurRestaurantDb);
            var allProductsInDb = LoadData<Product>(centaurRestaurantDb);
            var allUnitTypesInDb = LoadData<UnitType>(centaurRestaurantDb);
            var allProductTypesInDb = LoadData<ProductType>(centaurRestaurantDb);
            foreach (var item in allUnitTypesInDb)
            {
                Console.WriteLine(item.ToString());
            }
            foreach (var item in allProductsInDb)
            {
                Console.WriteLine(item.ToString());
            }
            foreach (var item in allProductTypesInDb)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static void SaveData<T>(MongoDatabase db, T value)
        {
                var result = db.GetCollection<T>(typeof(T).Name).Save(value);
        }

        public static IQueryable<T> LoadData<T>(MongoDatabase db)
        {
            return db.GetCollection<T>(typeof(T).Name).AsQueryable();
        }

        public static void DeleteAll<T>(MongoDatabase db)
        {
            db.GetCollection<T>(typeof(T).Name).RemoveAll();
        }
    }
}
