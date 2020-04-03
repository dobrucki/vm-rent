using System.Collections.Generic;
using System.Linq;

namespace Core.Domain.Dtos
{
    public class BaseResponseDto<TData> : BaseResponseDto where TData : class
    {
        public TData Data { get; set; }
    }

    public class BaseResponseDto
    {
        public BaseResponseDto()
        {
            Errors = new List<string>();
        }

        public ICollection<string> Errors { get; }
        public bool HasError => Errors.Any();
    }
}