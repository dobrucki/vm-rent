using System.Collections.Generic;
using Application.Core.Models;

namespace Application.WebApp.ViewModels
{
    public class ListUserViewModel
    {
        public IList<User> Users { get; set; }
    }
}