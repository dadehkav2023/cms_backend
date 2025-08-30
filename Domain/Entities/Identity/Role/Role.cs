using Microsoft.AspNetCore.Identity;
using Domain.Attributes.Identity;

namespace Domain.Entities.Identity.Role
{
    [IdentityEntity]
    public class Role : IdentityRole<int>
    {
        public string DisplayName { get; set; }
    }
}
