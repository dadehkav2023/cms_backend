using System.Collections.Generic;
using Domain.Attributes.Identity;
using Domain.Entities.ContactUs;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity.User
{
    [IdentityEntity]
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        //public ICollection<ContactUsMessage> ContactUsMessages { get; set; }

        //public virtual UserLegal UserLegal { get; set; }
        //public int UserLegalId { get; set; }
        //public virtual UserReal UserReal { get; set; }
        //public int UserRealId { get; set; }


    }
}
