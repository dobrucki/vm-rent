using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Dtos
{
    public class BaseResponseDto<TData>
    {
        public BaseResponseDto()
        {
            
        }

        public bool HasError => Errors.Any();
        public List<string> Errors { get; set; }
        public int Total { get; set; }
        public TData Data { get; set; }
    }
}