using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Query.Customers
{
    public class CustomerEntity
    {
        [BsonId]
        [BsonElement("id")]
        public string Id { get; set; }
        
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        
        [BsonElement("lastName")]
        public string LastName { get; set; }
        
        [BsonElement("emailAddress")]
        public string EmailAddress { get; set; }
    }
}