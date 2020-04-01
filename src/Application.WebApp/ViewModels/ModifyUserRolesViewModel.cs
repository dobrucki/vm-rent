using System.Collections.Generic;

namespace Application.WebApp.ViewModels
{
    public class ModifyUserRolesViewModel
    {
        public string UserId { get; set; }

        public IList<string> UserRoles { get; set; }
        
        public string Role { get; set; }
    }
}