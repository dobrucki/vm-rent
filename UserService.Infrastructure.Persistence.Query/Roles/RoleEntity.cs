using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Infrastructure.Persistence.Query.Roles
{
    public class RoleEntity
    {
        [BsonId]
        [BsonElement("id")]
        public string Id { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }
    }
}