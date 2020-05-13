using System;

namespace UserInterface.RestApi.Customers.ListCustomerRentals
{
    public class ListCustomerRentalsRequest
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}