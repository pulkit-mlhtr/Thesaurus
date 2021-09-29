using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Thesaurus.Api.Test.Support
{
    public static class AppConfiguration
    {
        public static  IConfiguration Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
                .Build();        
               
        public static IMongoClient mongoClient = new MongoClient(Config.GetSection("MongoDbSettings").GetSection("MongoUri").Value);
    }
}
