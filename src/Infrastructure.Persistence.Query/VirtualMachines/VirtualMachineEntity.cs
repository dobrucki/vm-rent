using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Query.VirtualMachines
{
    public class VirtualMachineEntity
    {
        [BsonId]
        [BsonElement("id")]
        public string Id { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }
    }
}