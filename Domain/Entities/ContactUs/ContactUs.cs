using Domain.Attributes;
using Domain.Attributes.AboutUs;
using Domain.Attributes.ContactUs;
using Domain.Entities.BaseEntity;

namespace Domain.Entities.ContactUs
{
    [ContactUsEntity]
    [Auditable]
    public class ContactUs : BaseEntityWithIdentityKey
    {
        public string Title { get; set; }
        public string HeaderImage { get; set; }
        public string ContactUsImage { get; set; }
        public string Email { get; set; }
    }
}