using System.Collections.Generic;
using Application.Core.Models;

namespace Application.WebApp.ViewModels
{
    public class DetailUserViewModel
    {
        public User User { get; set; }
        
        public IList<Reservation> UserVms { get; set; }
    }
}