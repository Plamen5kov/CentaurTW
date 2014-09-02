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

    using Telerik.OpenAccess;
    using MySqlPRovider;

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
    }
}
