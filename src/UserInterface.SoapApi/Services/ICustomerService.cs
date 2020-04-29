using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using UserInterface.SoapApi.Models;

namespace UserInterface.SoapApi.Services
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        public CustomerModel GetCustomer(string id);

        [OperationContract]
        public void CreateCustomer(CustomerModel customer);

        [OperationContract]
        public CustomerListModel ListCustomers(int offset, int limit);

        [OperationContract]
        public void EditCustomerDetails(string id, string firstName, string lastName);
    }
}