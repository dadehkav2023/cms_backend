using Domain.Attributes;
using Domain.Attributes.ContactUs;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.ContactUs
{
    [ContactUsEntity]
    [Auditable]
    public class ContactUsMessage: BaseEntityWithIdentityKey
    {
        public int? UserId { get; set; }
        public virtual Identity.User.User User { get; set; }

        public string FirstNameAndLastName { get; set; }
        public string Email { get; set; }
        
        public string TextMessage { get; set; }
    }
}