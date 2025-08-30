using System.Collections.Generic;

namespace Application.ViewModels.CMS.Identity.Request
{
    public class RequestRemoveRolesFromUserViewModel
    {
        public int UserId { get; set; }
        public List<string> RolesName { get; set; }
    }
}