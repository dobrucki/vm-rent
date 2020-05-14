using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Infrastructure.Persistence.Query.Users
{
    public class UserEntity
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