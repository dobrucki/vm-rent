using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Persistence.Query.Rentals
{
    public class RentalEntity
    {
        [BsonId]
        [BsonElement("id")]
        public string Id { get; set; }
        
        [BsonElement("customerId")]
        public string CustomerId { get; set; }
        
        [BsonElement("virtualMachineId")]
        public string VirtualMachineId { get; set; }
        
        [BsonElement("startTime")]
        public string StartTime { get; set; }
        
        [BsonElement("endTime")]
        public string EndTime { get; set; }
    }
}