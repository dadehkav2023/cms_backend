using System;
using Domain.Attributes;
using Domain.Attributes.Identity;

namespace Domain.Entities.Identity.User
{
    [EntityType]
    [Auditable]
    public class UserApiToken
    {
        public long Id { get; set; }
        public string TokenHash { get; set; }
        public DateTime TokenExp { get; set; }
        public string RefreshTokenHash { get; set; }
        public DateTime RefreshTokenExp { get; set; }
        public string PhoneNumber { get; set; }

         // public virtual User User { get; set; }
         // public string UserId { get; set; }
    }
}
