using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Castle.Components.DictionaryAdapter;
using Core.Domain.Rentals;
using Core.Domain.SharedKernel;

namespace RentService.Infrastructure.Persistence.Entities
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public IList<RentalEntity> Rentals { get; set; }
    }
}