using Newtonsoft.Json;

namespace RentingService.Api.Requests
{
    public class GetVirtualMachinesRequest : IPagedRequest
    {
        public int Page { get; }
        public int Limit { get; }

        [JsonConstructor]
        public GetVirtualMachinesRequest(int page, int limit)
        {
            Page = page;
            Limit = limit;
        }
    }
}