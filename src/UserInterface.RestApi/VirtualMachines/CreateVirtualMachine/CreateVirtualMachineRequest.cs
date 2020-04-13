using System;

namespace UserInterface.RestApi.VirtualMachines.CreateVirtualMachine
{
    public class CreateVirtualMachineRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}