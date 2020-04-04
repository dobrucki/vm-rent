namespace Core.Application.Dtos
{
    public class CustomerDto : ModelBaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}