namespace RentingService.Api.Requests
{
    public interface IPagedRequest : IRequest
    {
        int Page { get; }
        int Limit { get; }
    }
}