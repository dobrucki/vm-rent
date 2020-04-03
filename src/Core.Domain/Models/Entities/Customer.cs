namespace Core.Domain.Models.Entities
{
    public class Customer : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}