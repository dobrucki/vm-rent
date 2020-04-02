using System.Collections.Generic;
using System.Linq;

namespace Application.Domain.Dtos
{
    using Models;
    
    public class BaseResponseDto<TData> where TData : ModelBase
    {
        public BaseResponseDto(TData data)
        {
            Errors = new List<string>();
            Data = data;
        }

        public ICollection<string> Errors { get; }
        public bool HasErrors => Errors.Any();
        public TData Data { get; }
    }
}