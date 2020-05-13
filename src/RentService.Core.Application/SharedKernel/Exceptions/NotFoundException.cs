using System;
using System.Collections.Generic;

namespace RentService.Core.Application.SharedKernel.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, Guid id)
            : base($"Did not find any {name} with id ({id}).")
        {
        }
    }
}