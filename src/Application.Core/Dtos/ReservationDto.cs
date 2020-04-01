using System;

namespace Application.Core.Dtos
{
    public class ReservationDto : ModelBaseDto

    {
    public UserDto User { get; set; }
    public VirtualMachineDto VirtualMachine { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    }
}