using System;

namespace UserService.Core.Application.SharedKernel.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, Guid id)
            : base($"Did not find any {name} with id ({id}).")
        {
        }
    }
}