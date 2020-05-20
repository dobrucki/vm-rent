using System.Collections.Generic;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.RentalAggregate
{
    public class RentalStatus : ValueObject
    {
        public string StatusName { get; }

        protected RentalStatus(string statusName)
        {
            StatusName = statusName;
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StatusName;
        }
        
        public static RentalStatus Started => new RentalStatus("Started");
        public static RentalStatus Cancelled => new RentalStatus("Cancelled");
        public static RentalStatus Finished => new RentalStatus("Finished");
    }
}        