using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UserInterface.SoapApi.Models
{
    [DataContract]
    public class CustomerListModel
    {
        [DataMember]
        public List<CustomerModel> Customers { get; set; }
    }
}