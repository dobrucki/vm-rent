using System;
using System.Collections.Generic;
using Application.Core.Models;

namespace Application.Infrastructure.InMemoryDataAccess
{
    internal sealed class DataContext
    {
        private DataContext()
        {
            VirtualMachines = new Dictionary<Guid, VirtualMachine>();
            
            
            // Dummy data
            var virtualMachine1 = new VirtualMachine
            {
                Id = Guid.Parse("ad446f4a-c6d3-4557-997e-c7616620d6e6"),
                Name = "Virtual machine"
            };
            
            VirtualMachines.Add(virtualMachine1.Id, virtualMachine1);
        }

        private static DataContext _context;

        public static DataContext Context => _context ??= new DataContext();

        public IDictionary<Guid, VirtualMachine> VirtualMachines { get; }
    }
}