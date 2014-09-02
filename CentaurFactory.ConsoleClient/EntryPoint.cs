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
    using System.Configuration;
    using CentaurFactory.MongoDbProvider;
    using Ionic.Zip;

    using CentaurFactory.ExcelModel;
    using CentaurFactory.ExcelModel.Parsers;
    using CentaurFactory.SqlServerProvider;

    using Newtonsoft.Json;
    using Telerik.OpenAccess;
    using MySqlPRovider;
    using System.IO;

    public class EntryPoint
    {
        public static void Main()
        {
            /* To be able to run this code you need first to download mongodb server local on your computer.
               this is the link for windows 64 
               https://fastdl.mongodb.org/win32/mongodb-win32-x86_64-2008plus-2.6.4.zip .
               Before you start up the bin/mongod.exe file(this is the server) you need to 
               create a new 'data/db' directory in C:/ and than you start the mongod.exe file.
               Then you can run the program successfully. */

            //var mongoClient = new MongoClient("mongodb://192.168.0.100/");
            //var mongoServer = mongoClient.GetServer();
            //var centaurRestaurantDb = mongoServer.GetDatabase("CentaurRestaurantDb");
            var mongoProvider = new MongoProvider(ConfigurationManager.AppSettings["mongoDB"], "centaur_restaurant_db");
            var mongoRepo = new MongoRepository(mongoProvider);

            // uncomment to load data to mongo db
            //mongoRepo.InitData();

            //var allProductsInDb = mongoRepo.GetProducts();
            //var sqlServerRepo = new SqlServerRepository("SQLServer");
            //sqlServerRepo.AddProducts(allProductsInDb);

            // uncomment to clear the data
            //mongoRepo.EreaseData();

            //var allProductsInDb = mongoRepo.GetProducts();
            //var allUnitTypesInDb = mongoRepo.GetUntTypes();
            //var allProductTypesInDb = mongoRepo.GetProductTypes();
            //foreach (var item in allUnitTypesInDb)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //foreach (var item in allProductsInDb)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            //foreach (var item in allProductTypesInDb)
            //{
            //    Console.WriteLine(item.ToString());
            //}

            UpdateDatabase();

            // ExtractZipFiles();

            //DataContext data = new DataContext();
            //ExportReportToJsonFiles(data);

        }

        /// <summary>
        /// Extracts the zip file into a given directory
        /// </summary>
        /// <param name="path"></param>
        private static void ExtractZipFiles()
        {
            string zipToUnpack = "../../Report-Jul-2013.zip";
            string unpackDirectory = "../../";

            using (ZipFile zip = ZipFile.Read(zipToUnpack))
            {
                foreach (ZipEntry entry in zip)
                {
                    entry.Extract(unpackDirectory, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

        private static void UpdateDatabase()
        {
            using (var context = new MySqlPRovider.CentaurFactoryModel())
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);
            }
        }

        private static void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }

        public static void ExportReportToJsonFiles(DataContext data)
        {
            var products = data.Products.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                productType = p.ProductType,
                ingerdients = p.Ingredients,
                unitType = p.UnitType,
                qunatity = p.Quantity
            });

            Array.ForEach(Directory.GetFiles("..\\..\\..\\Reports\\Json-Reports\\"), File.Delete);

            var serializer = new JsonSerializer();
            foreach (var product in products)
            {
                string path = "..\\..\\..\\Reports\\Json-Reports\\" + product.id + ".json";

                using (var fileStream = new FileStream(path, FileMode.CreateNew))
                {
                    using (var sw = new StreamWriter(fileStream))
                    {
                        using (var writer = new JsonTextWriter(sw))
                        {
                            writer.Formatting = Formatting.Indented;
                            serializer.Serialize(writer, product);
                        }
                    }
                }
            }
        }
    }
}
