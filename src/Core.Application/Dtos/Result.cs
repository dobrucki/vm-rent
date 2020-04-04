using System.Collections.Generic;
using System.Linq;

namespace Core.Application.Dtos
{
    public class Result<TData> : Result where TData : class
    {
        public Result(TData data)
        {
            Data = data;
        }
        public TData Data { get; }
    }

    public class Result
    {
        public Result()
        {
            Errors = new List<Error>();
        }

        public ICollection<Error> Errors { get; }
        public bool Success => !Errors.Any();
    }
}