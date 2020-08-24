using MongoDB.Bson.Serialization.Attributes;
using System;


namespace MSSecurity.Models
{
    public class User
    {
        [BsonId]
        public Object Id { get; set; }
        public string fullname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public bool status { get; set; }
    }
}
