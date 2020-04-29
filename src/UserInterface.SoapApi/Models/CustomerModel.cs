using System.Runtime.Serialization;

namespace UserInterface.SoapApi.Models
{
    [DataContract]
    public class CustomerModel
    {
        [DataMember]
        public string Id { get; set; }
        
        [DataMember]
        public string EmailAddress { get; set; }
        
        [DataMember]
        public string FirstName { get; set; }
        
        [DataMember]
        public string LastName { get; set; }
    }
}