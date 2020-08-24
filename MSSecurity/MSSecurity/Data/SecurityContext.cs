using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MSSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSecurity.Data
{
    public class SecurityContext
    {
        private readonly IMongoDatabase _database = null;
        private readonly IConfiguration _configuration;
        public SecurityContext(IConfiguration configuration)
        {
            _configuration = configuration;

            //var client = new MongoClient(_configuration.GetSection("MongoConnection:ConnectionString").Value);
            var client = new MongoClient(_configuration["ConnectionStringMongo"]);
            if (client != null)
            {
                //_database = client.GetDatabase(_configuration.GetSection("MongoConnection:Database").Value);
                _database = client.GetDatabase(_configuration["databasemongo"]);
            }
        }
        public IMongoCollection<User> User
        {
            get {
                return _database.GetCollection<User>("User");
            }
        }
    }
}
